using ClientMonopoly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Monopoly_WPF
{
    /// <summary>
    /// Interaction logic for wSystemGame.xaml
    /// </summary>
    public partial class wSystemGame : Window, Monopoly_WPF.ServiceChatWCFMonopoly.IServiceChatCallback
    {
        #region Fields
        Client client;
        Monopoly_WPF.ServiceChatWCFMonopoly.ServiceChatClient clientWCF;
        string HOST = "http://localhost:63569/api/";
       // string HOST = "http://192.168.2.101:63569/api/";
        private DispatcherTimer timerCheckReadyGame;
        int userID=0;
        int friendID;
        int gameID;
        GameInfo gameInfo;
        ClientMonopoly.User user;
        bool checkCreateGame;
        MainWindow windowGame;
        ChatData chatData;
        List<User> listFriends;
        List<GameToList> games;
        List<User> listUsers;
        #endregion


        public wSystemGame(int userID)
        {
            InitializeComponent();
            clientWCF = new ServiceChatWCFMonopoly.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            client = new Client(HOST);

                                 
            chatData = new ChatData(client);
            this.userID = userID;
            user = client.GetUser(userID);
            string resalt=  clientWCF.Connect(userID,user.Name);
            lUserName.Content = user.Name;
            lRank.Content = user.Rank;
            lPoints.Content = user.Points;
            pbPoints.Value = user.Points;

            
            listUsers = chatData.GetUsers(userID);
            lbUsers.DataContext = listUsers;
            try
            {
                games = chatData.GetGames(userID);
                if (games != null)
                    lbGames.DataContext = games;
            }
            catch { }
            timerCheckReadyGame = new DispatcherTimer();
            timerCheckReadyGame.Interval = new TimeSpan(0, 0, 4); //in Hour, Minutes, Second.
            timerCheckReadyGame.Tick += timerCheckReadyGame_Tick;
        }

        private void Key_Loaded(object sender, RoutedEventArgs e)
        {

            lbFriends.DataContext = listFriends = Task.Run(() => chatData.GetFrends(userID)).Result;
            lbUsers.DataContext = listUsers = Task.Run(() => chatData.GetUsers(userID)).Result;
                      

        }

        public void timerCheckReadyGame_Tick(object sender, EventArgs e)
        {
            string check = client.CheckReadyPlay(gameID);

            if (check == "\"true\"")
            {
                timerCheckReadyGame.Stop();
                gameInfo = client.GetGameInfo(gameID);
                windowGame = new MainWindow(client, clientWCF, gameInfo, userID,false);
                windowGame.Owner = this;
                windowGame.Show();
               
            }
        }
        //Button Get friends
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(lbFriends.Width == 0)
            {
                lbFriends.Width = 252;
                btnUpdateFriends.Width = 30;
            }
            
            else
            {
                lbFriends.Width = 0;
                lbChatFriends.Width = 0;
                tbSendMsgFrend.Width = 0;
                btnUpdateFriends.Width = 0;
            }
        }
        //Button Get Users
        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.Width == 0)
            {
                lbUsers.Width = 250.6;
                tbFinde.Width = 198;
                btnFinde.Width = 51.2;
                lbUsers.DataContext = listUsers =  chatData.GetUsers(userID);
            }
            else
            {
                lbUsers.Width = 0;
                tbFinde.Width = 0;
                btnFinde.Width = 0;
            }
            
        }

        // Create game
        /// <summary>
        /// button handler function to create a new network game
        /// </summary>
        private void btnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            if (!checkCreateGame)                                             //checking user creation of games
            {
                WindowCreateGame windowCreateGame = new WindowCreateGame();

                if (windowCreateGame.ShowDialog() == true)                    //showing a dialog box and checking whether the user made a choice or not
                {
                    if (windowCreateGame.playwithAll == true)
                        client.CreateGame(user.ID, "wait");                  //sending a request to the server to create a game for all users :
                                                                             //parameters: user id and game status   
                    else if (windowCreateGame.playwithFrends == true)
                    {
                        client.CreateGame(user.ID, "waitfriend");            //sending a request to the server to create a game for friends only
                                                                             //parameters: user id and game status   
                    }
                    checkCreateGame = true;                                  //change the meaning that the game was created

                    games = chatData.GetGames(userID);                       //receiving from the server a list of games of waiting players

                    lbGames.DataContext = games;                             //show received games in ListBox
                    gameID = games[games.Count - 1].gameID;

                    timerCheckReadyGame.Start();                             //running a timer which, when receiving data from the server, 
                                                                             //checks that all players have joined the game
                }
            }
        }

        private void btnWorldChat_Click(object sender, RoutedEventArgs e)
        {
            if(lbWorldChat.Width == 0)
            {
                lbWorldChat.Width = 286.6;
                tbSendMsgAll.Width = 286.6;
            }
            
            else
            {
                lbWorldChat.Width = 0;
                tbSendMsgAll.Width = 0;
            }
               
        }

        private void lbFrends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            lbChatFriends.Items.Clear();
            lbUsers.Width = 0;
            lbChatFriends.Width = 250.6;
            tbSendMsgFrend.Width= 250.6;
            int selected =int.Parse( lbFriends.SelectedIndex.ToString());
                           
            friendID = listFriends[selected].ID;
            List<MsgToUser> messages = chatData.GetMessages(userID, friendID);
            foreach(MsgToUser item in messages)
            lbChatFriends.Items.Add(item.Message);
                //desineListBox.SplitMassegesListboxItemStringForeground(lbChatFriends, Colors.Black);
               // ListboxDesine();
            lbChatFriends.ScrollIntoView(lbChatFriends.Items[lbChatFriends.Items.Count - 1]);
            }
              catch { }
                       
        }

        private void Key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && tbSendMsgFrend.IsKeyboardFocused == true)
            {
                clientWCF.SendMsgtoFriend(tbSendMsgFrend.Text.ToString(),userID, friendID);
                client.SendMessageToUser(user.Name+"   "+ tbSendMsgFrend.Text.ToString(),userID, friendID);
                lbChatFriends.Items.Add(user.Name+"   "+ tbSendMsgFrend.Text.ToString());
                lbChatFriends.ScrollIntoView(lbChatFriends.Items[lbChatFriends.Items.Count - 1]);
                tbSendMsgFrend.Text = "Send Message";
            }
             else if(e.Key == System.Windows.Input.Key.Enter && tbSendMsgAll.IsKeyboardFocused == true)
            {
                clientWCF.SendMsgtoAll(tbSendMsgAll.Text.ToString(),user.ID, 0);
                tbSendMsgAll.Text = "Send Message";
            } 
        }
        
        public void MsgCallback(string msg, int userIdFrom, int userIdTo)
        {
            if (windowGame == null)
            {
                if (userIdTo == 0)
                {
                    // ClientMonopoly.User friend = client.GetUser(userIdTo);
                    lbWorldChat.Items.Add(msg);
                    lbWorldChat.ScrollIntoView(lbWorldChat.Items[lbWorldChat.Items.Count - 1]);
                }
                else
                {
                    if (lbChatFriends.Width != 0)
                    {

                        client.SendMessageToUserAsync(msg, userIdFrom, userIdTo);
                        lbChatFriends.Items.Add(msg);
                        lbChatFriends.ScrollIntoView(lbChatFriends.Items[lbChatFriends.Items.Count - 1]);
                    }
                    else
                    {
                        client.SendMessageToUser(msg, userIdFrom, userIdTo);
                    }

                }
            }
            else
            {
              windowGame.lbChat.Items.Add(msg);
                windowGame.lbChat.ScrollIntoView(windowGame.lbChat.Items[windowGame.lbChat.Items.Count - 1]);
            }
        }

        private void LbGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = int.Parse(lbGames.SelectedIndex.ToString());
            if(selected>=0)
            { 
            GameToList  game = games[selected];
            int  gameID = game.gameID;
            if (game.status == "waitfriends")
            {
                var user = listFriends.Find(i => i.ID == userID);
                if (user != null)
                    AddOrDellUserInGame(game);
            }
            else
            {
                AddOrDellUserInGame(game);
            }
            }
        }
        
        public void AddOrDellUserInGame(GameToList game)
        {
            ClientMonopoly.Player myplaerInGame = null;
            try
            {
                 myplaerInGame = client.GetPlayer(userID, game.gameID);

                if (game.Player1.ID == myplaerInGame.ID)
                {
                    //window do you want dellete game 
                    WindowAddOrDellUserinGame windowQuestion = new WindowAddOrDellUserinGame("Do you want to dellete the game?");
                    if (windowQuestion.ShowDialog() == true)
                    {
                        if (windowQuestion.chek == true)
                        {
                            timerCheckReadyGame.Stop();
                            client.DellGame(user.ID, game.gameID);
                            checkCreateGame = false;
                            Thread.Sleep(1000);
                            games = chatData.GetGames(userID);
                            lbGames.DataContext = games;
                        }
                    }
                }
                else if (game.Player2.ID == myplaerInGame.ID)
                {
                    WindowAddOrDellUserinGame windowQuestion = new WindowAddOrDellUserinGame("Do you want to leave the game?");
                    if (windowQuestion.ShowDialog() == true)
                    {
                        if (windowQuestion.chek == true)
                        {
                            client.DellPlayerFromGame(user.ID, game.gameID);
                            Thread.Sleep(1000);
                            games = chatData.GetGames(userID);
                            lbGames.DataContext = games;
                        }
                    }
                }
                }
            catch
            {
                client.AddPlayerToGame(userID, game.gameID);
                Thread.Sleep(1000);
                gameInfo = client.GetGameInfo(game.gameID);
                if (gameInfo.Game_Status == "play")
                {
                    MainWindow windowGame = new MainWindow(client, clientWCF, gameInfo, userID,false);
                    windowGame.Show();
                }
                else
                {
                    games = chatData.GetGames(userID);
                    lbGames.DataContext = games;
                    gameID = game.gameID;
                    timerCheckReadyGame.Start();
                }

            }
        }
          
        
        private void LbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = int.Parse(lbUsers.SelectedIndex.ToString());
            if (selected != -1)
            {
                User friend = listUsers[selected];
                WindowAddFriend windowAddFriend = new WindowAddFriend(friend);
                if (windowAddFriend.ShowDialog() == true)
                {
                    if (windowAddFriend.chek == true)
                    {
                        client.AddUserToFrends(userID, friend.ID);
                        listFriends = chatData.GetFrends(userID);
                        lbFriends.DataContext = listFriends;
                    }

                }
            }
        }

        private void Key_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.ExitFromSystemAsync(userID);
            clientWCF.Disconnect(user.ID);

        }

        private void Key_Closed(object sender, EventArgs e)
        {
          
        }

        private void TbSendMsgFrend_MouseEnter(object sender, MouseEventArgs e)
        {
            tbSendMsgFrend.Text = "";
            
        }

        private void BtnUpdateGames_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                games = chatData.GetGames(userID);
                if (games != null)
                    lbGames.DataContext = games;
            }
            catch { }
        }

        private void BtnUpdateFriends_Click(object sender, RoutedEventArgs e)
        {
            lbFriends.DataContext = listFriends = Task.Run(() => chatData.GetFrends(userID)).Result;
        }

        private void BtnMyGame_Click(object sender, RoutedEventArgs e)
        {

            WindowDifficult winMod = new WindowDifficult();
            winMod.ShowDialog();
            if (winMod.DialogResult == true)
            {
                MainWindow mainWindow = new MainWindow(client, clientWCF, null, userID, winMod.difficult);
                mainWindow.Show();
            }
        }

        private void btnFinde_Click(object sender, RoutedEventArgs e)
        {
            if (tbFinde.Text == "")
                lbUsers.DataContext = listUsers = chatData.GetUsers(userID);
            else
            {
                lbUsers.DataContext = listUsers = chatData.GetUserbyName(tbFinde.Text.ToString());
                tbFinde.Text = "";
            }
        }
    }
}
