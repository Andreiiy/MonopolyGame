using ClientMonopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Monopoly_WPF
{
  public  class MyGameMonopoly
    {
        MainWindow mainWindow;
        Canvas canvas;
       
        Client clientGame;
        private int userID;
        private int priceFilld;
        public string nextPlayerMove = "Red";
        private Player myPlayer;
        private Player playerToJail;
        private Player playerToStart;
        private Player computerGreen;
        private Player computerYellow;
        private List<Player> lPlayers;
        private Board board;
        public string payNow;
        private bool difficult;
        private DispatcherTimer timerGoPlayer;
        private DispatcherTimer timerGoToJail;
        private DispatcherTimer timerPay;
        private DispatcherTimer timerGoToStart;

        public MyGameMonopoly(MainWindow mainWindow, Canvas canvas, Client client, int userID, bool difficult)
        {

            this.mainWindow = mainWindow;
            this.canvas = canvas;
            this.clientGame = client;
            this.userID = userID;
            this.difficult = difficult;
            mainWindow.lGameOver.Height = 0;
            board = new Board(mainWindow, clientGame);
            Canvas.SetLeft(mainWindow.red, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.red, board.GetFields()[0].top);

            Canvas.SetLeft(mainWindow.green, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.green, board.GetFields()[0].top + 30);

            Canvas.SetLeft(mainWindow.yellow, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.yellow, board.GetFields()[0].top + 60);

            timerGoPlayer = new DispatcherTimer();
            timerGoPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoPlayer.Tick += timerGoPlayer_Tick;

            timerGoToJail = new DispatcherTimer();
            timerGoToJail.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoToJail.Tick += timerGoToJail_Tick;

            timerGoToStart = new DispatcherTimer();
            timerGoToStart.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoToStart.Tick += timerGoToStart_Tick;

            timerPay = new DispatcherTimer();
            timerPay.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerPay.Tick += timerPay_Tick;

            CreateMyPlayers();
            ShowPlayers();
            setForegroundColor();
        }

        private void timerGoToStart_Tick(object sender, EventArgs e)
        {
           // Image image3 = null;
            Player player = null;

            if (playerToStart.Color == "Red")
            {
                //image3 = mainWindow.red;
                player = myPlayer;

            }
            if (playerToStart.Color == "Green")
            {
               
                player = computerGreen;
            }
            if (playerToStart.Color == "Yellow")
            {
                
                player = computerYellow;
            }

            if (!player.playerGoToPlace(mainWindow, lPlayers, board)) { }
            else
            {
                timerGoToStart.Stop();
                mainWindow.btnStart.IsEnabled = true;
            }
                
        }

        private void timerGoToJail_Tick(object sender, EventArgs e)
        {
            //Image image3 = null;
            Player player = null;

            if (playerToJail.Color == "Red")
            {
               // image3 = mainWindow.red;
                player = myPlayer;

            }
            if (playerToJail.Color == "Green")
            {
               player = computerGreen;
            }
            if (playerToJail.Color == "Yellow")
            {
                player = computerYellow;
            }

            if (!player.playerGoToPlace(mainWindow, lPlayers, board)) { }
            else
            {
                timerGoToJail.Stop();
                mainWindow.btnStart.IsEnabled = true;
            }
                

        }


        /// <summary>
        /// the timer moves the player on the table and starts the function of processing actions on the field
        /// </summary>
        private void timerGoPlayer_Tick(object sender, EventArgs e)
        {

            if (timerGoToJail.IsEnabled == false && timerGoToStart.IsEnabled == false)
            {
                Player player = lPlayers.Where(p => (p.Color == nextPlayerMove)).FirstOrDefault();          //player search what makes the move

                    if (player.NumberSkips == 0)                                                            //check whether the player can make a move or not
                    { 
                        if (player.Status != "Lost" && player.playerGoToPlace(mainWindow, lPlayers, board)) //check completed player movement or not
                        {
                            actionOnField(player);                                                         //starts the function of processing actions on the field
                    }
                    }
                    else
                    {
                    player.NumberSkips -= 1;                                                                //reduce the number of skipped moves
                    nextPlayerMove = GetNextPlaerMove(nextPlayerMove);                                      //get the color of the next player in turn

                    if (nextPlayerMove == "Red")
                        {
                                CheckGameOver();
                                mainWindow.btnStart.IsEnabled = true;
                                timerGoPlayer.Stop();
                        }
                        else
                        {
                         CheckGameOver();
                         MoveMyPlayer();
                   
                        }
                    }
               }
        }

        /// <summary>
        /// the function of processing actions on the field
        /// </summary>
        /// <param name="player">Object type Player</param>
        private void actionOnField(Player player)
        {
            if (board.GetFields()[player.Place].status == "")
                actionOnPlayingField(board.GetFields()[player.Place].name, player);

            if (board.GetFields()[player.Place].status == "free")                                     //if the field can be bought
            {
                if (nextPlayerMove == "Red" && myPlayer.Money > board.GetFields()[player.Place].price)//if the player is not a computer 
                                                                                                      //and he has enough money to buy a field
                {
                    if (myPlayer.buyField(board, null, 0) == true)                                    //player buy field
                    {
                        payNow = nextPlayerMove;
                        priceFilld = board.GetFields()[player.Place].price;                          
                        timerPay.Start();
                    }
                }
                else if (nextPlayerMove == "Green" || nextPlayerMove == "Yellow")                    //if the player is a computer
                {
                    ComputerBuyField(player);                                                        //start the function of buying a computer field
                }
            }
            else if (player.Color != board.GetFields()[player.Place].owner)                          //if the field is bought by another player
                PayRent(player);                                                                     //start function for paying field rent

            nextPlayerMove = GetNextPlaerMove(nextPlayerMove);
            if (nextPlayerMove == "Red")                                                             //if the player is not a computer
            {
                if (timerGoToJail.IsEnabled == false && timerGoToStart.IsEnabled == false)
                    mainWindow.btnStart.IsEnabled = true;
                timerGoPlayer.Stop();                                                                //timer stop
                ShowPlayers();
            }
            else
            {
                ShowPlayers();
                MoveMyPlayer();                                  //if the player is  a computer started functions start of the die roll function
                CheckGameOver();
            }
        }

        private void actionOnPlayingField(string name, Player player)
        {
           switch (name)
            {
                case "start":
                    player.Money += 10000;
                    break;
                case "jail":
                    player.NumberSkips += 3;
                    break;
                case "parking":
                    player.NumberSkips +=1;
                    break;
                case "gotojail":
                    {
                        player.NumberSkips = 3;
                        player.Place = 8;
                        playerToJail = player;
                        timerGoToJail.Start();
                    }
                    break;
                case "gotostart":
                    {
                        player.Place = 0;
                        playerToStart = player;
                        timerGoToStart.Start();
                        actionOnPlayingField("start", player);
                    }
                    break;
                case "chance":
                    actionOnChanceField(player);
                    break;
            }
        }

        private void actionOnChanceField(Player player)
        {
            Images images = new Images();
            string chance = board.GetChance();
         mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance(chance));
           
            switch (chance)
            {
                case "get10":
                    {
                        player.Money += 10000;
                        ShowPlayers();
                    }
                    break;
                case "get15":
                    {
                        player.Money += 15000;
                        ShowPlayers();
                    }
                    break;
                case "pay10":
                    {
                        player.Money -= 10000;
                        payNow = player.Color;
                        priceFilld = 10000;
                        timerPay.Start();
                    }
                    break;
                case "pay15":
                    {
                        player.Money -= 15000;
                        payNow = player.Color;
                        priceFilld = 15000;
                        timerPay.Start();
                    }
                    break;
                case "gotojail":
                    actionOnPlayingField("gotojail", player);
                    break;
                case "gotostart":
                    actionOnPlayingField("gotostart", player);
                    break;
            }
            
        }

        private void timerPay_Tick(object sender, EventArgs e)
        {
            if (priceFilld != 0)
            {
                
                switch (payNow)
                {
                    case "Red":
                        {
                           // myPlayer.Money -= 100;
                            mainWindow.lMoney.Content = myPlayer.Money+ priceFilld;
                            
                        }
                        break;
                    case "Green":
                        {
                            //computerGreen.Money -= 100;
                            mainWindow.lMoney2.Content = computerGreen.Money + priceFilld;
                           
                        }
                        break;
                    case "Yellow":
                        {
                            //computerYellow.Money -= 100;
                            mainWindow.lMoney3.Content = computerYellow.Money + priceFilld;
                        }
                        break;
                }
                priceFilld -= 1000;
            }
            else
            {
               timerPay.Stop();
            }
        }

        /// <summary>
        /// the function processes the actions of buying a field by a computer
        /// </summary>
        /// <param name="computer"> Object type Computer</param>
        private void ComputerBuyField(Player computer)
        {
                if (computer.buyField(board, null, 0))                        //if the computer can buy a field
            {
                computer.Money -= board.GetField(computer.Place).price;
                board.BuyField(computer.Place, computer);                     //replacement of playing field data

                payNow = nextPlayerMove;
                priceFilld = board.GetFields()[computer.Place].price;
                timerPay.Start();
            }
        }

        private void PayRent(Player player)
        {
            if (player.Money < board.GetFields()[player.Place].rent)
            {
                player.PayRent(board, mainWindow);
               
            }
            if (player.Status != "Lost")
            {
                player.Money -= board.GetFields()[player.Place].rent;
                if (board.GetFields()[player.Place].owner == "Green")
                    computerGreen.Money += board.GetFields()[player.Place].rent;
                else if (board.GetFields()[player.Place].owner == "Yellow")
                    computerYellow.Money += board.GetFields()[player.Place].rent;
                else
                    myPlayer.Money+= board.GetFields()[player.Place].rent;
            }
        }

        private void CreateMyPlayers()
        {
            myPlayer = new Player();
            myPlayer.ID = 1;
            myPlayer.Name = clientGame.GetUser(userID).Name;
            myPlayer.Color = "Red";
            myPlayer.Place = 0;
            myPlayer.Money = 250000;
            myPlayer.Move = "true";
            myPlayer.Status = "play";

            computerGreen = new Computer();
            computerGreen.difficult = difficult;
            computerGreen.ID = 2;
            computerGreen.Name = "ComputerGreen";
            computerGreen.Color = "Green";
            computerGreen.Place = 0;
            computerGreen.Money = 250000;
            computerGreen.Move = "false";
            computerGreen.Status = "play";
            

            computerYellow = new Computer();
            computerYellow.difficult = difficult;
            computerYellow.ID = 3;
            computerYellow.Name = "ComputerYellow";
            computerYellow.Color = "Yellow";
            computerYellow.Place = 0;
            computerYellow.Money = 250000;
            computerYellow.Move = "false";
            computerYellow.Status = "play";

            lPlayers = new List<Player>();
            lPlayers.Add( myPlayer);
            lPlayers.Add(computerGreen);
            lPlayers.Add(computerYellow);

        }

        private void ShowPlayers()
        {
            if (myPlayer.Status.Equals("Lost"))
            {
                mainWindow.lStatus.Content = myPlayer.Status;
                mainWindow.lName.Content = "";
                mainWindow.lMoney.Content ="";
                mainWindow.lMove.Content ="";
                mainWindow.lColor.Content = "";
            }
            else
            {
                mainWindow.lName.Content = myPlayer.Name;
                mainWindow.lMoney.Content = myPlayer.Money;
                mainWindow.lStatus.Content = myPlayer.Status;
                mainWindow.lMove.Content = myPlayer.Move;
                mainWindow.lColor.Content = myPlayer.Color;
            }
            if (computerGreen.Status.Equals("Lost"))
            {
                mainWindow.lStatus2.Content = computerGreen.Status;
                mainWindow.lName2.Content = "";
                mainWindow.lMoney2.Content = "";
                mainWindow.lMove2.Content = "";
                mainWindow.lColor2.Content = "";
            }
            else
            {
                mainWindow.lName2.Content = computerGreen.Name;
                mainWindow.lMoney2.Content = computerGreen.Money;
                mainWindow.lStatus2.Content = computerGreen.Status;
                mainWindow.lMove2.Content = computerGreen.Move;
                mainWindow.lColor2.Content = computerGreen.Color;
            }
            if (computerYellow.Status.Equals("Lost"))
            {
                mainWindow.lStatus3.Content = computerYellow.Status;
                mainWindow.lName3.Content = "";
                mainWindow.lMoney3.Content = "";
                mainWindow.lMove3.Content = "";
                mainWindow.lColor3.Content = "";
            }
            else
            {
                mainWindow.lName3.Content = computerYellow.Name;
                mainWindow.lMoney3.Content = computerYellow.Money;
                mainWindow.lStatus3.Content = computerYellow.Status;
                mainWindow.lMove3.Content = computerYellow.Move;
                mainWindow.lColor3.Content = computerYellow.Color;
            }
            
                    
        }

        private string GetNextPlaerMove(string player)
        {
            if (player == "Red")
            {
                if (computerGreen.Status != "Lost")
                {
                    myPlayer.Move = "False";
                    computerGreen.Move = "True";
                    return "Green";
                }
                else return "Yellow";
                
               
            }
            if (player == "Green")
            {
                if (computerYellow.Status != "Lost")
                {
                    computerGreen.Move = "False";
                    computerYellow.Move = "True";
                    return "Yellow";
                }
                else return "Red";

            }
            if (player == "Yellow")
            {
                computerYellow.Move = "False";
                myPlayer.Move = "True";
                return "Red";
            }
            else return "Green";

            return player;

        }

        public void SetImageKub(int num1, int num2)
        {
            mainWindow.kub1.Source = new BitmapImage(new Uri(@"/Image/Kub/" + num1 + ".png", UriKind.Relative));
            mainWindow.kub2.Source = new BitmapImage(new Uri(@"/Image/Kub/" + num2 + ".png", UriKind.Relative));
            
        }

        /// <summary>
        /// game dice throw function
        /// </summary>
        public void MoveMyPlayer()
        {
            
            Player player = null;

            //player search what makes the move
            if (nextPlayerMove == "Red") player = myPlayer;
            else if (nextPlayerMove == "Green") player = computerGreen;
            else if (nextPlayerMove == "Yellow") player = computerYellow;

            Random rand = new Random();
            //two random number generation
            int num1 = rand.Next(1, 7);
            int num2 = rand.Next(1, 7);

            SetImageKub(num1, num2);                                 //show the player two cubes with generated numbers

            if (player.NumberSkips == 0)
            {
                if (player.Place + num1 + num2 > 29)
                {
                    player.Place = player.Place + num1 + num2 - 30;  //setting a new place for the player on the gaming table
                    player.Money = player.Money + 10000;
                }
                else
                    player.Place += num1 + num2;
            }
            timerGoPlayer.IsEnabled = true;                         //starting a timer that moves the player to a new field
        }

        public void setForegroundColor()
        {
            mainWindow.lName.Foreground = Brushes.Red;
            mainWindow.lMoney.Foreground = Brushes.Red;
            mainWindow.lStatus.Foreground = Brushes.Red;
            mainWindow.lMove.Foreground = Brushes.Red;
            mainWindow.lColor.Foreground = Brushes.Red;

            mainWindow.lName2.Foreground = Brushes.Green;
            mainWindow.lMoney2.Foreground = Brushes.Green;
            mainWindow.lStatus2.Foreground = Brushes.Green;
            mainWindow.lMove2.Foreground = Brushes.Green;
            mainWindow.lColor2.Foreground = Brushes.Green;

            mainWindow.lName3.Foreground = Brushes.Yellow;
            mainWindow.lMoney3.Foreground = Brushes.Yellow;
            mainWindow.lStatus3.Foreground = Brushes.Yellow;
            mainWindow.lMove3.Foreground = Brushes.Yellow;
            mainWindow.lColor3.Foreground = Brushes.Yellow;
        }

        private int shiftPlayer()
        {
            if (myPlayer.Place == computerGreen.Place && myPlayer.Place == computerYellow.Place)
                return 60;
                switch (nextPlayerMove)
            {
                case "Red":
                    if (myPlayer.Place == computerGreen.Place || myPlayer.Place == computerYellow.Place)
                        return 30;
                    break;
                case "Green":
                    if (myPlayer.Place == computerGreen.Place || computerGreen.Place == computerYellow.Place)
                        return 30;
                    break;
                case "Yellow":
                    if (myPlayer.Place == computerYellow.Place || computerGreen.Place == computerYellow.Place)
                        return 30;
                    break;
            }
            
            return 0;
        }

        private void CheckGameOver()
        {
            int count = 0;
            if (myPlayer.Status == "Lost")
            {
                mainWindow.red.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.lLost.Content = "You Lost";
                mainWindow.lGameOver.Height = 227;
                mainWindow.btnStart.IsEnabled = false;
            }
            else
            {
                            
            if (computerGreen.Status == "Lost")
                {
                    mainWindow.green.Visibility = System.Windows.Visibility.Hidden;
                    count++;
                }
           
                if (computerYellow.Status == "Lost")
                {
                    mainWindow.yellow.Visibility = System.Windows.Visibility.Hidden;
                    count++;
                }
            }
            if(count >= 2)
            {
                ShowPlayers();
                mainWindow.lGameOver.Height = 227;
                    mainWindow.lLost.Content = "You Winner";
                    clientGame.PlayerMyGameWinner(userID);

                mainWindow.btnStart.IsEnabled = false;
            }
        }
    }
}