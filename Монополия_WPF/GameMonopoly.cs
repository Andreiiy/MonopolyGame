using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClientMonopoly;

namespace Monopoly_WPF
{
    public class GameMonopoly 
    {
        #region Fields

        MainWindow mainWindow;
        Canvas canvas;
       
        Client clientGame;
        GameInfo gameInfo;
        private int gameID;
        private int userID;
        public int priceFilld;
        public string nextPlaerMove = "Red";
        private ModalWindow winMod;
        private Player myPlayer;
        private Player playerRed;
        private Player playerGreen;
        private Player playerYellow;
        private List<Player> lPlayers;
        private Board board;
        public string payNow;
        private int moneyPlayer;
        private DispatcherTimer timerGoPlayer;
        private DispatcherTimer timerPay;
        private DispatcherTimer timerCheckReadyMovePlayer;
        private DispatcherTimer timerGoToJail;
        private DispatcherTimer timerGoToStart;
        private Player playerToJail;
        private Player playerToStart;
        #endregion


        public GameMonopoly(MainWindow mainWindow, Canvas canvas, Client client, GameInfo gameInfo,int userID)
        {
            this.mainWindow = mainWindow;
            this.canvas = canvas;
            this.clientGame = client;
            this.gameInfo = gameInfo;
            this.userID = userID;
            this.gameID = gameInfo.ID_Game;
            board = new Board(mainWindow,clientGame);
            myPlayer = CreateMyPlayer();
            playerRed = new Player();
            playerGreen = new Player();
            playerYellow = new Player();
            lPlayers = new List<Player>();
            lPlayers.AddRange(new List<Player>{playerRed,
                                               playerGreen,
                                               playerYellow });
            CreateMyPlayers();

            #region Timers
            timerGoPlayer = new DispatcherTimer();
            timerGoPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoPlayer.Tick += timerGoPlayer_Tick;

            timerCheckReadyMovePlayer = new DispatcherTimer();
            timerCheckReadyMovePlayer.Interval = new TimeSpan(0, 0, 3); //in Hour, Minutes, Second.
            timerCheckReadyMovePlayer.Tick += timerCheckReadyMovePlayer_Tick;

            timerPay = new DispatcherTimer();
            timerPay.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerPay.Tick += timerPay_Tick;

            timerGoToJail = new DispatcherTimer();
            timerGoToJail.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoToJail.Tick += timerGoToJail_Tick;

            timerGoToStart = new DispatcherTimer();
            timerGoToStart.Interval = new TimeSpan(0, 0, 0, 0, 1); //in Hour, Minutes, Second.
            timerGoToStart.Tick += timerGoToStart_Tick;

            #endregion


            mainWindow.btnStart.IsEnabled = false;


            Canvas.SetLeft(mainWindow.red, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.red, board.GetFields()[0].top);

            Canvas.SetLeft(mainWindow.green, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.green, board.GetFields()[0].top + 30);

            Canvas.SetLeft(mainWindow.yellow, board.GetFields()[0].left);
            Canvas.SetTop(mainWindow.yellow, board.GetFields()[0].top + 60);

            // win.Dispatcher.Invoke(new Action(() => win.Show()));

            //timerCheckReadyGame.Start();
            ShowPlayers();
            timerCheckReadyMovePlayer.Start();
        }

        private void timerGoToStart_Tick(object sender, EventArgs e)
        {
            
            Player player = null;

            switch (playerToStart.Color)
            {
                case "Red":
                    player = lPlayers.Where(b => (b.Color == "Red")).FirstOrDefault();
                    break;
                case "Green":
                    player = lPlayers.Where(b => (b.Color == "Green")).FirstOrDefault();
                    break;
                case "Yellow":
                    player = lPlayers.Where(b => (b.Color == "Yellow")).FirstOrDefault();
                    break;
            }
            if (player.playerGoToPlace(mainWindow, lPlayers, board)) 
            {
                timerGoToStart.Stop();
                mainWindow.btnStart.IsEnabled = true;
            }

        }

        private void timerGoToJail_Tick(object sender, EventArgs e)
        {
            
            Player player = null;

            switch (playerToJail.Color)
            {
                case "Red":
                    player = lPlayers.Where(b => (b.Color == "Red")).FirstOrDefault();
                    break;
                case "Green":
                    player = lPlayers.Where(b => (b.Color == "Green")).FirstOrDefault();
                    break;
                case "Yellow":
                    player = lPlayers.Where(b => (b.Color == "Yellow")).FirstOrDefault();
                    break;
            }
            
            if (player.playerGoToPlace(mainWindow, lPlayers, board)) 
            {
                timerGoToJail.Stop();
                mainWindow.btnStart.IsEnabled = true;
            }


        }

        private void timerPay_Tick(object sender, EventArgs e)
        {
            if (priceFilld != 0)
            {
                lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money -= 1000;
                switch (lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Color)
                {
                    case "Red":
                        {
                            if(myPlayer.Color=="Red") mainWindow.lMoney.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                            else mainWindow.lMoney2.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                        }
                        break;
                    case "Green":
                        {
                            if (myPlayer.Color == "Green") mainWindow.lMoney.Content = mainWindow.lMoney.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money; 
                            else if(myPlayer.Color == "Red") mainWindow.lMoney2.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                            else mainWindow.lMoney3.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                        }
                        break;
                    case "Yellow":
                        {
                            if (myPlayer.Color == "Yellow") mainWindow.lMoney.Content = mainWindow.lMoney.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                            else if (myPlayer.Color == "Red") mainWindow.lMoney3.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                            else mainWindow.lMoney3.Content = lPlayers.Where(p => (p.Color == payNow)).FirstOrDefault().Money;
                        }
                        break;
                }
                      
                priceFilld -= 100;
            }
            else
            {
              
                timerPay.Stop();
                CreateMyPlayers();
                ShowPlayers();
                timerCheckReadyMovePlayer.Start();
            }
        }

        
        private void timerGoPlayer_Tick(object sender, EventArgs e)
        {
           
            
            Player player = null;
            foreach (Player pl in lPlayers)
                if (pl.Color == nextPlaerMove)
                    player = pl;

            if (player.NumberSkips == 0)
            {
                //Move Player to field
                if (player.Status != "Lost" && !player.playerGoToPlace(mainWindow, lPlayers, board)) { }

                //Player actions on the field
                else
                {
                    mainWindow.btnStart.IsEnabled = false;

                    //Actions my player on the field                            

                    //Player buy fielld
                    if (board.GetFields()[player.Place].status == "free" && nextPlaerMove == myPlayer.Color)
                    {

                        if (player.buyField(board, clientGame, gameInfo.ID_Game) == true)
                        {
                                                      
                            payNow = nextPlaerMove;
                            priceFilld = board.GetFields()[player.Place].price;
                            timerPay.Start();
                        }
                    }

                    //Player pay rent
                    else if (board.GetFields()[player.Place].status == "buy" && board.GetFields()[player.Place].owner != myPlayer.Color)
                    {

                        if (lPlayers.Where(b => (b.Color == myPlayer.Color)).FirstOrDefault().Money < board.GetFields()[player.Place].rent)
                        {
                            while (myPlayer.Money < board.GetFields()[player.Place].rent && myPlayer.Status != "Lost")
                            {
                                if (board.GetMyFillds(myPlayer.Color) != null)
                                {
                                    WindowSellField windowSellField = new WindowSellField(board.GetMyFillds(myPlayer.Color));
                                    windowSellField.ShowDialog();
                                                                    
                                    board.SellField(windowSellField.fieldSell.number);
                                    clientGame.FilldSell(gameInfo.ID_Game, myPlayer.ID, windowSellField.fieldSell.number);
                                }
                                else
                                {
                                    myPlayer.Status = "Lost";
                                    clientGame.PlayerLost(player.ID);
                                }
                                   
                            }
                        }

                        if (myPlayer.Status != "Lost")
                        {
                            clientGame.PayRent(this.gameInfo.ID_Game, myPlayer.ID, board.GetFields()[player.Place].number);
                           
                        }

                    }

                    else if (board.GetFields()[player.Place].status != "buy" && board.GetFields()[player.Place].status != "free" &&  player.Color == myPlayer.Color)
                    {
                        actionOnPlayingField(board.GetFields()[player.Place].name, player);
                    }

                    //Actions of other players     
                    if (nextPlaerMove != myPlayer.Color && board.GetFields()[player.Place].status == "free" && player.Status != "Lost")
                    {
                        ClientMonopoly.Field field = clientGame.GetField(gameInfo.ID_Game, player.Place);

                        if (field.Status == "buy" && board.GetFields()[player.Place].owner != player.Color)
                        {
                            payNow = nextPlaerMove;
                            board.BuyField(board.GetFields()[player.Place].number, player);
                            priceFilld = board.GetFields()[player.Place].price;
                            timerPay.Start();
                        }
                        board.ChengeFilld(field);
                        Task.Run(() => board.UpdataFields(gameInfo.ID_Game));
                    }
                   else if (nextPlaerMove != myPlayer.Color && board.GetFields()[player.Place].status == "chance" && player.Status != "Lost")
                    {
                    checkChancesOtherPlayer(player);
                    }
                        finishAction(player);
                }
            }
            else
            {
                player.NumberSkips -= 1;
                finishAction(player);
            }
        }

        private void checkChancesOtherPlayer(Player player)
        {
            ClientMonopoly.Player myplaerInGame = clientGame.GetPlayerbyID(player.ID);
            Images images = new Images();
            if (myplaerInGame.PlaceInTable == 0)
            {

                mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("gotostart"));
                player.Place = 0;
                playerToStart = player;
                timerGoToStart.Start();
            }
            else if (myplaerInGame.PlaceInTable == 8)
            {
                mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("gotojail"));
                player.Place = 8;
                playerToJail = player;
                timerGoToJail.Start();
            }
            else
            {
                int chackMoney =  player.Money - moneyPlayer;

                switch (chackMoney)
                {
                    case 10000:
                        {
                            mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("get10"));
                        }
                        break;
                    case 15000:
                        {
                            mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("get15"));
                        }
                        break;
                    case -10000:
                        {
                            mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("pay10"));
                        }
                        break;
                    case -15000:
                        {
                            mainWindow.Imagechance.Source = new BitmapImage(images.GetImageChance("pay15"));
                        }
                        break;
                }
            }
        }

        private void finishAction(Player player)
        {
            clientGame.PlayerMove(gameInfo.ID_Game, player.Color, player.Place);
            nextPlaerMove = GetNextPlaerMove(nextPlaerMove);
            this.gameInfo = clientGame.GetGameInfo(this.gameInfo.ID_Game);
                       
            if (!timerPay.IsEnabled)
            {
                CreateMyPlayers();
                ShowPlayers();
                timerCheckReadyMovePlayer.Start();
            }
            
            timerGoPlayer.Stop();
        }

        private async void actionOnPlayingField(string nameField, Player player)
        {
            switch (nameField)
            {
                case "start":
                    {
                        player.Money += 10000;
                        clientGame.PlayerAddMoney(player.ID, 10000);
                    }
                    break;
                case "jail":
                    player.NumberSkips += 3;
                    break;
                case "parking":
                    player.NumberSkips += 1;
                    break;
                case "gotojail":
                    {
                        player.NumberSkips = 3;
                        player.Place = 8;
                       await Task.Run(() => clientGame.PlayerMove(gameInfo.ID_Game, player.Color, player.Place));
                        playerToJail = player;
                        timerGoToJail.Start();
                    }
                    break;
                case "gotostart":
                    {
                        player.Place = 0;
                        playerToStart = player;
                        await Task.Run(() => clientGame.PlayerMove(gameInfo.ID_Game, player.Color, player.Place));
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
                        clientGame.PlayerAddMoney(player.ID, 10000);
                        ShowPlayers();
                    }
                    break;
                case "get15":
                    {
                        player.Money += 15000;
                        clientGame.PlayerAddMoney(player.ID, 15000);
                        ShowPlayers();
                    }
                    break;
                case "pay10":
                    {
                        player.Money -= 10000;
                        clientGame.PlayerReduceMoney(player.ID, 10000);
                        payNow = player.Color;
                        priceFilld = 10000;
                        timerPay.Start();
                    }
                    break;
                case "pay15":
                    {
                        player.Money -= 15000;
                        clientGame.PlayerReduceMoney(player.ID, 15000);
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

        
        private void timerCheckReadyMovePlayer_Tick(object sender, EventArgs e)
        {
           if(lPlayers.Where(p=>p.Color == myPlayer.Color).FirstOrDefault().Status != "Lost")
            {
                if (!timerGoPlayer.IsEnabled)
                {
                    Player player = null;
                    player = lPlayers.Where(p => p.Color == myPlayer.Color).FirstOrDefault();
                    this.gameInfo = clientGame.GetGameInfo(this.gameInfo.ID_Game);
                    if (player.Move == "false")
                    {
                        checkMoveOtherPlayer();
                    }
                    else
                    {
                        mainWindow.btnStart.IsEnabled = true;
                        timerCheckReadyMovePlayer.Stop();
                    }
                }
                else
                    timerCheckReadyMovePlayer.Stop();
            }
            else
            {
                timerCheckReadyMovePlayer.Stop();
                mainWindow.lLost.Content = "You Lost";
                //mainWindow.lGameOver.Height = 227;
            }
              
        }

        private void checkMoveOtherPlayer()
        {
            Player player;
            if (nextPlaerMove == "Red" && this.gameInfo.PlayerRed_Move == "false")
            {
                player = lPlayers.Where(p => p.Color == "Red").FirstOrDefault();
                if ((int.Parse(this.gameInfo.PlayerRed_Place) - player.Place) % 2 == 0)
                    SetImageKub((int.Parse(this.gameInfo.PlayerRed_Place) - player.Place) / 2, (int.Parse(this.gameInfo.PlayerRed_Place) - player.Place) / 2);
                else
                    SetImageKub((int.Parse(this.gameInfo.PlayerRed_Place) - player.Place) / 2, ((int.Parse(this.gameInfo.PlayerRed_Place) - player.Place) / 2) + 1);
                moneyPlayer = player.Money;
                timerCheckReadyMovePlayer.Stop();
                timerGoPlayer.Start();
                CreateMyPlayers();
                ShowPlayers();
            }
            else if (nextPlaerMove == "Green" && gameInfo.PlayerGreen_Move == "false")
            {
                player = lPlayers.Where(p => p.Color == "Green").FirstOrDefault();
                if ((int.Parse(this.gameInfo.PlayerGreen_Place) - player.Place) % 2 == 0)
                    SetImageKub((int.Parse(this.gameInfo.PlayerGreen_Place) - player.Place) / 2, (int.Parse(this.gameInfo.PlayerGreen_Place) - player.Place) / 2);
                else
                    SetImageKub((int.Parse(this.gameInfo.PlayerGreen_Place) - player.Place) / 2, ((int.Parse(this.gameInfo.PlayerGreen_Place) - player.Place) / 2) + 1);
                moneyPlayer = player.Money;
                timerCheckReadyMovePlayer.Stop();
                timerGoPlayer.Start();
                CreateMyPlayers();
                ShowPlayers();
            }
            else if (nextPlaerMove == "Yellow" && gameInfo.PlayerYellow_Move == "false")
            {
                player = lPlayers.Where(p => p.Color == "Yellow").FirstOrDefault();
                if ((int.Parse(this.gameInfo.PlayerYellow_Place) - player.Place) % 2 == 0)
                    SetImageKub((int.Parse(this.gameInfo.PlayerYellow_Place) - player.Place) / 2, (int.Parse(this.gameInfo.PlayerYellow_Place) - player.Place) / 2);
                else
                    SetImageKub((int.Parse(this.gameInfo.PlayerYellow_Place) - player.Place) / 2, ((int.Parse(this.gameInfo.PlayerYellow_Place) - player.Place) / 2) + 1);
                moneyPlayer = player.Money;
                timerCheckReadyMovePlayer.Stop();
                timerGoPlayer.Start();
                CreateMyPlayers();
                ShowPlayers();
            }

        }
        private Player CreateMyPlayer()
        {
            Player player = new Player();
           
            var plaerInGame = clientGame.GetPlayer(userID,gameInfo.ID_Game);
            gameID = plaerInGame.GameID;
            player.ID = plaerInGame.ID;
            if(plaerInGame.Color=="Red")player.Name = gameInfo.PlayerRed_Name;
            else if (plaerInGame.Color == "Green") player.Name = gameInfo.PlayerGreen_Name;
            else  player.Name = gameInfo.PlayerYellow_Name;
            player.Color = plaerInGame.Color;
            player.Place = plaerInGame.PlaceInTable;
            player.Money = plaerInGame.Money;
            player.Move = plaerInGame.Move;
            player.Status = plaerInGame.Status;
            
            return player;

        }

        private void CreateMyPlayers()
        {
            playerRed.ID = gameInfo.PlayerRed_ID;
            playerRed.Name = gameInfo.PlayerRed_Name;
            playerRed.Color = "Red";
            playerRed.Place = int.Parse(gameInfo.PlayerRed_Place);
            playerRed.Money = gameInfo.PlayerRed_Money;
            playerRed.Move = gameInfo.PlayerRed_Move;
            playerRed.Status = gameInfo.PlayerRed_Status;

            playerGreen.ID = gameInfo.PlayerGreen_ID;
            playerGreen.Name = gameInfo.PlayerGreen_Name;
            playerGreen.Color = "Green";
            playerGreen.Place = int.Parse(gameInfo.PlayerGreen_Place);
            playerGreen.Money = gameInfo.PlayerGreen_Money;
            playerGreen.Move = gameInfo.PlayerGreen_Move;
            playerGreen.Status = gameInfo.PlayerGreen_Status;

            playerYellow.ID = gameInfo.PlayerYellow_ID;
            playerYellow.Name = gameInfo.PlayerYellow_Name;
            playerYellow.Color = "Yellow";
            playerYellow.Place = int.Parse(gameInfo.PlayerYellow_Place);
            playerYellow.Money = gameInfo.PlayerYellow_Money;
            playerYellow.Move = gameInfo.PlayerYellow_Move;
            playerYellow.Status = gameInfo.PlayerYellow_Status;

            lPlayers[0]=playerRed;
            lPlayers[1]=playerGreen;
            lPlayers[2]=playerYellow;
            CheckFinishGame();
        }
        
        private void CheckFinishGame()
        {
            int count = 0;
            bool myPlayerLost = false;
            foreach (var pl in lPlayers)
                if (pl.Status == "Lost")
                {
                    count++;
                    if (pl.Color == myPlayer.Color)
                    {
                        myPlayerLost = true;
                        mainWindow.lLost.Content = "You Lost";
                    }
                }
            if (count >= 2)
            {
               if(myPlayerLost == false)
                {
                    mainWindow.lLost.Content = "You Winner";
                }
                else
                {
                    mainWindow.lLost.Content = "You Lost";
                }
                mainWindow.lGameOver.Height = 227;
                mainWindow.btnStart.IsEnabled = false;
            }
        }

        private string GetNextPlaerMove(string player)
        {
            if (player == "Red")
            {
                if (gameInfo.PlayerGreen_Status != "Lost")
                    return "Green";
                else
                    return "Yellow";
            }
            if (player == "Green")
            {
                if (gameInfo.PlayerYellow_Status != "Lost")
                    return "Yellow";
                else
                    return "Red";
               
            }

            if (player == "Yellow")
            {
                if (gameInfo.PlayerRed_Status != "Lost")
                    return "Red";
                else
                    return "Green";
            }
            return player;

        }

        private void SetImageKub(int num1, int num2)
        {
            mainWindow.kub1.Source = new BitmapImage(new Uri(@"/Image/Kub/" + num1 + ".png", UriKind.Relative));
            mainWindow.kub2.Source = new BitmapImage(new Uri(@"/Image/Kub/" + num2 + ".png", UriKind.Relative));

        }
        
        public void MoveMyPlayer()
        {
            Player player = null;
            Random rand = new Random();
            
            int num1 = rand.Next(1, 7);
            int num2 = rand.Next(1, 7);
            SetImageKub(num1, num2);

            foreach (Player pl in lPlayers)
                if (pl.Color == myPlayer.Color)
                    player = pl;

            if(player.NumberSkips==0)
            {
                if (player.Place + num1 + num2 > 29)
                {
                    player.Place = player.Place + num1 + num2 - 30;
                    clientGame.PlayerAddMoney(player.ID, 10000);
                }
                else
                    player.Place += num1 + num2;
            }

            timerGoPlayer.IsEnabled = true;
        }

        private void ShowPlayers()
        { 
            switch (myPlayer.Color)
            {
                case "Red":
                    if (playerRed.Status == "Lost")
                    {
                        mainWindow.lName.Content = "";
                        mainWindow.lMoney.Content = "";
                        mainWindow.lStatus.Content = "Lost";
                        mainWindow.lMove.Content = "";
                        mainWindow.lColor.Content = "";
                        mainWindow.red.Width = 0;

                    }
                    else
                    {
                        mainWindow.lName.Content = playerRed.Name;
                        mainWindow.lMoney.Content = playerRed.Money;
                        mainWindow.lStatus.Content = playerRed.Status;
                        mainWindow.lMove.Content = playerRed.Move;
                        mainWindow.lColor.Content = playerRed.Color;
                    }
                    if (playerGreen.Status == "Lost")
                    {
                        mainWindow.lName2.Content = "";
                        mainWindow.lMoney2.Content = "";
                        mainWindow.lStatus2.Content = "Lost";
                        mainWindow.lMove2.Content = "";
                        mainWindow.lColor2.Content = "";
                        mainWindow.green.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName2.Content = playerGreen.Name;
                        mainWindow.lMoney2.Content = playerGreen.Money;
                        mainWindow.lStatus2.Content = playerGreen.Status;
                        mainWindow.lMove2.Content = playerGreen.Move;
                        mainWindow.lColor2.Content = playerGreen.Color;
                    }
                    if (playerYellow.Status == "Lost")
                    {
                        mainWindow.lName3.Content = "";
                        mainWindow.lMoney3.Content = "";
                        mainWindow.lStatus3.Content = "Lost";
                        mainWindow.lMove3.Content = "";
                        mainWindow.lColor3.Content = "";
                        mainWindow.yellow.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName3.Content = playerYellow.Name;
                        mainWindow.lMoney3.Content = playerYellow.Money;
                        mainWindow.lStatus3.Content = playerYellow.Status;
                        mainWindow.lMove3.Content = playerYellow.Move;
                        mainWindow.lColor3.Content = playerYellow.Color;
                    }
                    ///////////////////////////
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
                    break;




                case "Green":
                    if (playerGreen.Status == "Lost")
                    {
                        mainWindow.lName.Content = "";
                        mainWindow.lMoney.Content = "";
                        mainWindow.lStatus.Content = "Lost";
                        mainWindow.lMove.Content = "";
                        mainWindow.lColor.Content = "";
                        mainWindow.green.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName.Content = playerGreen.Name;
                        mainWindow.lMoney.Content = playerGreen.Money;
                        mainWindow.lStatus.Content = playerGreen.Status;
                        mainWindow.lMove.Content = playerGreen.Move;
                        mainWindow.lColor.Content = playerGreen.Color;
                    }
                    if (playerRed.Status == "Lost")
                    {
                        mainWindow.lName2.Content = "";
                        mainWindow.lMoney2.Content = "";
                        mainWindow.lStatus2.Content = "Lost";
                        mainWindow.lMove2.Content = "";
                        mainWindow.lColor2.Content = "";
                        mainWindow.red.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName2.Content = playerRed.Name;
                        mainWindow.lMoney2.Content = playerRed.Money;
                        mainWindow.lStatus2.Content = playerRed.Status;
                        mainWindow.lMove2.Content = playerRed.Move;
                        mainWindow.lColor.Content = playerRed.Color;
                    }
                    if (playerYellow.Status == "Lost")
                    {
                        mainWindow.lName3.Content = "";
                        mainWindow.lMoney3.Content = "";
                        mainWindow.lStatus3.Content = "Lost";
                        mainWindow.lMove3.Content = "";
                        mainWindow.lColor3.Content = "";
                        mainWindow.yellow.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName3.Content = playerYellow.Name;
                        mainWindow.lMoney3.Content = playerYellow.Money;
                        mainWindow.lStatus3.Content = playerYellow.Status;
                        mainWindow.lMove3.Content = playerYellow.Move;
                        mainWindow.lColor3.Content = playerYellow.Color;
                    }
                    /////////////////////////////////////////////

                    mainWindow.lName2.Foreground = Brushes.Red;
                    mainWindow.lMoney2.Foreground = Brushes.Red;
                    mainWindow.lStatus2.Foreground = Brushes.Red;
                    mainWindow.lMove2.Foreground = Brushes.Red;
                    mainWindow.lColor2.Foreground = Brushes.Red;

                    mainWindow.lName.Foreground = Brushes.Green;
                    mainWindow.lMoney.Foreground = Brushes.Green;
                    mainWindow.lStatus.Foreground = Brushes.Green;
                    mainWindow.lMove.Foreground = Brushes.Green;
                    mainWindow.lColor.Foreground = Brushes.Green;

                    mainWindow.lName3.Foreground = Brushes.Yellow;
                    mainWindow.lMoney3.Foreground = Brushes.Yellow;
                    mainWindow.lStatus3.Foreground = Brushes.Yellow;
                    mainWindow.lMove3.Foreground = Brushes.Yellow;
                    mainWindow.lColor3.Foreground = Brushes.Yellow;

                    break;
                case "Yellow":
                    if (playerYellow.Status == "Lost")
                    {
                        mainWindow.lName.Content = "";
                        mainWindow.lMoney.Content = "";
                        mainWindow.lStatus.Content = "Lost";
                        mainWindow.lMove.Content = "";
                        mainWindow.lColor.Content = "";
                        mainWindow.yellow.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName.Content = playerYellow.Name;
                        mainWindow.lMoney.Content = playerYellow.Money;
                        mainWindow.lStatus.Content = playerYellow.Status;
                        mainWindow.lMove.Content = playerYellow.Move;
                        mainWindow.lColor.Content = playerYellow.Color;
                    }
                    if (playerRed.Status == "Lost")
                    {
                        mainWindow.lName2.Content = "";
                        mainWindow.lMoney2.Content = "";
                        mainWindow.lStatus2.Content = "Lost";
                        mainWindow.lMove2.Content = "";
                        mainWindow.lColor2.Content = "";
                        mainWindow.red.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName2.Content = playerRed.Name;
                        mainWindow.lMoney2.Content = playerRed.Money;
                        mainWindow.lStatus2.Content = playerRed.Status;
                        mainWindow.lMove2.Content = playerRed.Move;
                        mainWindow.lColor2.Content = playerRed.Color;
                    }
                    if (playerGreen.Status == "Lost")
                    {
                        mainWindow.lName3.Content = "";
                        mainWindow.lMoney3.Content = "";
                        mainWindow.lStatus3.Content = "Lost";
                        mainWindow.lMove3.Content = "";
                        mainWindow.lColor3.Content = "";
                        mainWindow.green.Width = 0;
                    }
                    else
                    {
                        mainWindow.lName3.Content = playerGreen.Name;
                        mainWindow.lMoney3.Content = playerGreen.Money;
                        mainWindow.lStatus3.Content = playerGreen.Status;
                        mainWindow.lMove3.Content = playerGreen.Move;
                        mainWindow.lColor3.Content = playerGreen.Color;
                    }
                    ///////////////////////////////////////////////

                    mainWindow.lName2.Foreground = Brushes.Red;
                    mainWindow.lMoney2.Foreground = Brushes.Red;
                    mainWindow.lStatus2.Foreground = Brushes.Red;
                    mainWindow.lMove2.Foreground = Brushes.Red;
                    mainWindow.lColor2.Foreground = Brushes.Red;

                    mainWindow.lName3.Foreground = Brushes.Green;
                    mainWindow.lMoney3.Foreground = Brushes.Green;
                    mainWindow.lStatus3.Foreground = Brushes.Green;
                    mainWindow.lMove3.Foreground = Brushes.Green;
                    mainWindow.lColor3.Foreground = Brushes.Green;

                    mainWindow.lName.Foreground = Brushes.Yellow;
                    mainWindow.lMoney.Foreground = Brushes.Yellow;
                    mainWindow.lStatus.Foreground = Brushes.Yellow;
                    mainWindow.lMove.Foreground = Brushes.Yellow;
                    mainWindow.lColor.Foreground = Brushes.Yellow;
                    break;

            }


        }

       
    }
}
