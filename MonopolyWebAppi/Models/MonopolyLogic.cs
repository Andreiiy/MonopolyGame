using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MonopolyWebAppi4.Models
{
    public class MonopolyLogic
    {
        private  Model1 db;
        private Player player;
        private MsgToUser Msg;
        public MonopolyLogic()
        {
            db = new Model1();

        }

        public User ChackEntry(string userName, string password)
        {
            User user = db.Users.Where(u => (u.Name == userName && u.Password == password)).FirstOrDefault();
            if (user == null)
            {
                User user2 = new User();
                user2.ID = -1;
                user2.Points = 1;
                user2.Name = "dsfsdf";
                user2.Password = "43333434";
                user2.Email = "dfgsdfhgsdhg";
                user2.Status = "sdf";
                user2.Rank = "sdgdgs";
                user2.NumberLosses = 1;
                user2.NumberWins = 1;
                return user2;
            }
            else
            {
                user.StatusInSystem = "online";
                //db.Users.Add(user);
                db.SaveChanges();
            }
            return user;
        }

        public Player GetPlayerbyID(int playerID)
        {
            return db.Players.Where(u => u.ID == playerID).FirstOrDefault();
        }

        public void PlayerLost( int playerID)
        {
            int count = 0;
            var player = db.Players.Where(p => ( p.ID == playerID)).FirstOrDefault();
            player.Status = "Lost";
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            if(player.Move == "true")
            MovePlayer(player.GameID, player.Color, player.PlaceInTable);
            var game = db.Games.Where(g => (g.ID == player.GameID)).FirstOrDefault();
            var players = db.Players.Where(p => (p.GameID == game.ID)).ToList();
            foreach(var pl in players)
                if (pl.Status == "Lost") count++;
            if (count >= 2)
            {
                game.Status = "GameOver";
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                foreach (var pl in players)
                    if (pl.Status != "Lost") PlayerMyGameWinner(pl.UserID);
            }
                

        }

        public User GetUserByName(string userName)
        {
          return db.Users.Where(u => u.Name == userName).FirstOrDefault();
        }

        public List<User> GetUsersWithoutFriends(int userID)
        {
            List<User> usersWithoutFriends = new List<User>();
            List<UserFriend> friends = db.UserFriends.Where(u => (u.UserId == userID)).ToList();
            List <User> usersAll = db.Users.ToList();

            foreach(User user in usersAll)
            {
                UserFriend friend = friends.Where(f => (f.FreindId == user.ID)).FirstOrDefault();
                if (friend == null && user.ID != userID)
                    usersWithoutFriends.Add(user);
            }
            return usersWithoutFriends;
        }

        public void PlayerMyGameWinner(int userID)
        {
            var user = db.Users.Where(p => (p.ID == userID)).FirstOrDefault();
            user.NumberWins = user.NumberWins + 1;
            user.Points = user.Points + 100;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public void PlayerAddMoney(int playerID, int sumMoney)
        {
            var player = db.Players.Where(p => p.ID == playerID).FirstOrDefault();
            player.Money += sumMoney;
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.SaveChangesAsync();
        }
        public void PlayerReduceMoney(int playerID, int sumMoney)
        {
            var player = db.Players.Where(p => p.ID == playerID).FirstOrDefault();
            player.Money -= sumMoney;
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.SaveChangesAsync();
        }

        public Player GetPlayer(int userID, int gameID)
        {
            var player = db.Players.Where(p => (p.UserID == userID && p.GameID == gameID)).FirstOrDefault();

            return player;
        }

        public void ExitFromSystem(int userID)
        {
            User user = db.Users.Where(u => (u.ID == userID)).FirstOrDefault();
           
                user.StatusInSystem = "ofline";
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesAsync();
                       
        }

        
        /// <summary>
        /// Create new Game in the Data Base
        /// </summary>
        /// <param name="statusGame">Status Game</param>
        /// <param name="userID"> User ID</param>
        /// <returns>new player information</returns>
        public NewPlayerInGame CreateGame(string statusGame,int userID)
        {
            NewPlayerInGame newPlayer;
            Game game = CreateNewGame(statusGame);     //create new game in table Games

            CreateNewPlayer(game, userID);             //create new player in table Players

            newPlayer = GetPlayerInGame(game, player); //get new player information
             
            return newPlayer;
        }


        public void DellGame(int userID, int gameID)
        {
            Game game = db.Games.Find(gameID);
            var player = db.Players.Where(p => p.UserID == userID);
            var fillds = db.Fillds.Where(f => f.GameID == gameID);
            db.Games.Remove(game);
            db.Players.RemoveRange(player);
            db.Fillds.RemoveRange(fillds);
            db.SaveChanges();
        }

        public NewPlayerInGame AddPlayerToGame(int userID, int gameID)
        {
            Game game = db.Games.Find(gameID);
            CreateNewPlayer(game, userID);
            NewPlayerInGame newPlayer = GetPlayerInGame(game, player);
            return newPlayer;
        }


        public void DellPlayerFromGame(int userID, int gameID)
        {
            Game game = db.Games.Find(gameID);
            game.PlayerGreen = null;
            db.Games.Add(game);
            db.SaveChanges();
        }


        private NewPlayerInGame GetPlayerInGame(Game game, Player player)
        {
            NewPlayerInGame newPlayer = new NewPlayerInGame();
            newPlayer.gameID = game.ID;
            newPlayer.playerID = player.ID;
            var user = db.Users.Find(player.UserID);
            newPlayer.Player_Name = user.Name;
            newPlayer.Player_Color = player.Color;
            newPlayer.Player_Place = player.PlaceInTable;
            newPlayer.Player_Status = player.Status;
            newPlayer.Player_Money = player.Money;
            newPlayer.Player_Move = player.Move;

            return newPlayer;
        }

        /// <summary>
        /// Create new game in table Games
        /// </summary>
        /// <param name="status"> Status Game</param>
        /// <returns>Object type Game</returns>
        private Game CreateNewGame(string status)
        {
            Game game = new Game();
            game.Status = status;
            db.Games.Add(game);        //add game to table Games
            db.SaveChanges();          // save chenges in the Data Base

            CreateFillds(game.ID);     // create fields for game

            return game;
        }

        /// <summary>
        /// Create new player in table Players fore game
        /// </summary>
        /// <param name="game">Object game type Game</param>
        /// <param name="userID"> User ID</param>
        public void CreateNewPlayer(Game game, int userID)
        {
            player = new Player();
            player.GameID = game.ID;
            player.UserID = userID;
            player.PlaceInTable = 0;
            player.Status = "play";
            player.Money = 250000;
            if (game.PlayerRed == null)      //if the red player does not exist, the red player is created
            {
                player.Color = "Red";
                player.Move = "true";
                db.Players.Add(player);      //add new player to table Players
                db.SaveChanges();            //save changes in the Data Base
                game.PlayerRed = player.ID;
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            else if (game.PlayerRed != null && game.PlayerGreen == null)
            {
                player.Color = "Green";
                player.Move = "false";
                db.Players.Add(player);
                db.SaveChanges();
                game.PlayerGreen = player.ID;
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else if (game.PlayerGreen != null && game.PlayerYellow == null)
            {
                player.Color = "Yellow";
                player.Move = "false";
                db.Players.Add(player);
                db.SaveChanges();
                game.PlayerYellow = player.ID;
                game.Status = "play";
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Create new fields for game in table Fields
        /// </summary>
        /// <param name="gameID">Game ID</param>
        private void CreateFillds(int gameID)
        {
            List<Filld> fillds = new List<Filld>() { 
            new Filld{GameID= gameID,NumberFilld= 0,Status= "start",Price= 0,Rent= 0 },
            new Filld{GameID=gameID,NumberFilld= 1,Status= "free",Price= 50000,Rent= 20000 },
            new Filld{GameID=gameID, NumberFilld = 2, Status = "free", Price = 25000, Rent = 10000},
            new Filld{GameID=gameID, NumberFilld = 3, Status = "free", Price = 35000, Rent = 15000},
            new Filld { GameID = gameID, NumberFilld = 4, Status = "chance", Price = 0, Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 5, Status = "free", Price = 45000, Rent = 25000},
            new Filld{GameID=gameID, NumberFilld = 6, Status = "free", Price = 30000, Rent = 15000},
            new Filld{GameID=gameID, NumberFilld = 7, Status = "free", Price = 25000, Rent = 10000},
            new Filld { GameID = gameID, NumberFilld = 8, Status = "jail", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 9, Status = "free", Price = 65000,Rent = 25000},
            new Filld{GameID=gameID, NumberFilld = 10, Status = "free", Price = 80000,Rent = 30000},
            new Filld { GameID = gameID, NumberFilld = 11, Status = "chance", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 12, Status = "free", Price = 40000,Rent = 20000},
            new Filld{GameID=gameID, NumberFilld = 13, Status = "free", Price = 50000,Rent = 25000},
            new Filld{GameID=gameID, NumberFilld = 14, Status = "free", Price = 30000,Rent = 15000},
            new Filld { GameID = gameID, NumberFilld = 15, Status = "parking", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 16, Status = "free", Price = 60000,Rent = 25000},
            new Filld{GameID=gameID, NumberFilld = 17, Status = "free", Price = 40000,Rent = 20000},
            new Filld{GameID=gameID, NumberFilld = 18, Status = "free", Price = 35000,Rent = 15000},
            new Filld { GameID = gameID, NumberFilld = 19, Status = "chance", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 20, Status = "free", Price = 15000,Rent = 10000},
            new Filld{GameID=gameID, NumberFilld = 21, Status = "free", Price = 25000,Rent = 10000},
            new Filld{GameID=gameID, NumberFilld = 22, Status = "free", Price = 10000,Rent = 3000},
            new Filld { GameID = gameID, NumberFilld = 23, Status = "gotojaice", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 24, Status = "free", Price = 15000,Rent = 5000},
            new Filld{GameID=gameID, NumberFilld = 25, Status = "free", Price = 25000,Rent = 10000},
            new Filld{GameID=gameID, NumberFilld = 26, Status = "free", Price = 20000,Rent = 7000},
            new Filld { GameID =gameID, NumberFilld = 27, Status = "chance", Price = 0,Rent = 0},

            new Filld{GameID=gameID, NumberFilld = 28, Status = "free", Price = 10000,Rent = 3000 },
            new Filld{GameID=gameID, NumberFilld = 29, Status = "free", Price = 90000, Rent = 40000 },
        };
            db.Fillds.AddRange(fillds);
            db.SaveChanges();
        }

        
        public List<GameInfo> GetGamesWait(int userID)
            {
                List<GameInfo> gamesInfo = new List<GameInfo>();
            List<Game> gameMyFriends = GetMyFriendsGames(userID);
            List<Game> gamesForMe = GetGamesOfMyLevel(userID);

            foreach (Game game in gameMyFriends)
            {
                GameInfo info = GetGameInfo(game.ID);
                gamesInfo.Add(info);
            }
            foreach (Game game in gamesForMe)
                {
                    GameInfo info = GetGameInfo(game.ID);
                    gamesInfo.Add(info);
                }
                return gamesInfo;
          
        }

        private List<Game> GetMyFriendsGames(int userID)
        {
            List<Game> gameMyFriends = new List<Game>();
            List<UserFriend> friends = db.UserFriends.Where(u => (u.UserId == userID)).ToList();

            List<Game> gamesWaitFriends = db.Games.Where(g => (g.Status == "waitfriend")).ToList();
            foreach (Game game in gamesWaitFriends)
            {
                var playerRed = db.Players.Where(u => u.ID == game.PlayerRed).FirstOrDefault();
                if (playerRed.UserID == userID)
                    gameMyFriends.Add(game);

                var friend = friends.Where(f => (f.FreindId == playerRed.UserID)).FirstOrDefault();
                if (friend != null)
                    gameMyFriends.Add(game);
            }

            return gameMyFriends;

        }
        private List<Game> GetGamesOfMyLevel(int userID)
        {
            List<Game> games = new List<Game>();
            List<Game> gamesAll = db.Games.Where(g => (g.Status == "wait")).ToList();
            User myUser = db.Users.Where(u => (u.ID == userID)).FirstOrDefault();
            
            foreach (Game game in gamesAll)
            {
                Player player = db.Players.Where(u => u.ID == game.PlayerRed).FirstOrDefault();
                User userInGame = db.Users.Where(u => (u.ID == player.UserID)).FirstOrDefault();

                if (myUser.Rank == userInGame.Rank)
                    games.Add(game);
            }
            return games;
        }
            public GameInfo GetGameInfo(int gameID)
        {
            GameInfo info = new GameInfo();
            
                Game game = db.Games.Find(gameID);
            if (game != null)
            {
                info.ID_Game = game.ID;
                info.Game_Status = game.Status;
                Player playerRed = db.Players.Find(game.PlayerRed);
                User userRedPlayer = db.Users.Find(playerRed.UserID);
                info.PlayerRed_ID = playerRed.ID;
                info.PlayerRed_Name = userRedPlayer.Name;
                info.PlayerRed_Color = "Red";
                info.PlayerRed_Place = playerRed.PlaceInTable.ToString();
                info.PlayerRed_Money = playerRed.Money;
                info.PlayerRed_Move = playerRed.Move;
                info.PlayerRed_Status = playerRed.Status;
                info.PlayerRed_Gender = userRedPlayer.Gender;
                info.PlayerRed_Rank = userRedPlayer.Rank;


                ///////////////////////////////////////////////////////
                try
                {
                    Player playerGreen = db.Players.Find(game.PlayerGreen);
                    User userGreenPlayer = db.Users.Find(playerGreen.UserID);
                    info.PlayerGreen_ID = playerGreen.ID;
                    info.PlayerGreen_Name = userGreenPlayer.Name;
                    info.PlayerGreen_Color = "Green";
                    info.PlayerGreen_Place = playerGreen.PlaceInTable.ToString();
                    info.PlayerGreen_Money = playerGreen.Money;
                    info.PlayerGreen_Move = playerGreen.Move;
                    info.PlayerGreen_Status = playerGreen.Status;
                    info.PlayerGreen_Gender = userGreenPlayer.Gender;
                    info.PlayerGreen_Rank = userGreenPlayer.Rank;
                }
                catch { }
                ////////////////////////////////////////////////////////
                try
                {
                    Player playerYellow = db.Players.Find(game.PlayerYellow);
                    User userYellowPlayer = db.Users.Find(playerYellow.UserID);
                    info.PlayerYellow_ID = playerYellow.ID;
                    info.PlayerYellow_Name = userYellowPlayer.Name;
                    info.PlayerYellow_Color = "Yellow";
                    info.PlayerYellow_Place = playerYellow.PlaceInTable.ToString();
                    info.PlayerYellow_Money = playerYellow.Money;
                    info.PlayerYellow_Move = playerYellow.Move;
                    info.PlayerYellow_Status = playerYellow.Status;
                    info.PlayerYellow_Gender = userYellowPlayer.Gender;
                    info.PlayerYellow_Rank = userYellowPlayer.Rank;
                }
                catch { }
            }
                return info;
        }

        public string CheckReadyPlay(int gameID)
        {
            var game = db.Games.Where(g => g.ID == gameID).FirstOrDefault();
            if (game.Status == "play")
                return "true";
            return "false";
        }

        public List<MsgToUser> GetMsgToUser(int info1, int info2)
        {
            var msgToUser = db.MsgToUsers.Where(m => (m.UserIdFrom == info1 && m.UserIdTo == info2)).ToList();

            return msgToUser;
        }


        public void SendMsg(int userIdFrom, int userIdTo, string msg)
        {
            Msg = new MsgToUser();
            Msg.UserIdFrom = userIdFrom;
            Msg.UserIdTo = userIdTo;
            Msg.Message = msg + "~f~";
            Msg.Date = DateTime.Now.ToString();
            db.MsgToUsers.Add(Msg);
            db.SaveChanges();
        }


        public string CreateNewUser(string userName, string email, string password,string gender)
        {
            User user1 = db.Users.Where(u => (u.Name == userName)).FirstOrDefault();
            User user2 = db.Users.Where(u => ( u.Password == password)).FirstOrDefault();
            if (user1 != null )return "falseName";
            else if( user2 != null) return "falsePassword";

            User  user = new User();
            user.Name = userName;
            user.Password = password;
            user.Email = email;
            user.StatusInSystem = "ofline";
            user.Status = "notplay";
            user.Rank = "Newbie";
            user.Gender = gender;
            user.NumberLosses = 0;
            user.NumberWins = 0;
            user.Points = 0;
            
            db.Users.Add(user);
            db.SaveChanges();
            return "true";
        }


        public IQueryable<UserFriend> GetUserFriends(int userID)
        {
            return db.UserFriends.Where(g => g.UserId == userID);
        }

        
        public async Task AddUserToFrendsAsync(int userID, int friendID)
        {

            UserFriend friend = db.UserFriends.Where(u=> u.UserId == userID && u.FreindId == friendID).FirstOrDefault();
            if(friend == null)
            {
            UserFriend userFriend = new UserFriend();
            userFriend.UserId = userID;
            userFriend.FreindId = friendID;
            db.UserFriends.Add(userFriend);
            await db.SaveChangesAsync();
            }
            

        }             
        

        public async Task FilldDepositAsync(int gameId, string status, int filldNumber)
        {
            Filld filld = db.Fillds.Where(f => (f.GameID == gameId && f.NumberFilld == filldNumber)).FirstOrDefault();
            filld.Status = status;
            db.Entry(filld).State = System.Data.Entity.EntityState.Modified;
          await  db.SaveChangesAsync();
        }

        public Filld GetField(int gameId, int filldNumber)
        {
            Filld field = db.Fillds.Where(f => (f.GameID == gameId && f.NumberFilld == filldNumber)).FirstOrDefault();
            return field;
        }

        public List<Filld> GetFields(int gameId)
        {
            var fields = db.Fillds.Where(f => (f.GameID == gameId )).ToList();
            return fields;
        }

        public void FilldBuy(int gameId, int playerID, int filldNumber)
        {
            Filld filld = db.Fillds.Where(f => (f.GameID == gameId && f.NumberFilld == filldNumber)).FirstOrDefault();
            Player player = db.Players.Where(p => p.ID == playerID).FirstOrDefault();
            player.Money = player.Money - filld.Price;
            filld.Status = "buy";
            filld.Owner = player.Color;
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.Entry(filld).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void FilldSell(int gameId, int playerID, int filldNumber)
        {
            Filld filld = db.Fillds.Where(f => (f.GameID == gameId && f.NumberFilld == filldNumber)).FirstOrDefault();
            Player player = db.Players.Where(p => p.ID == playerID).FirstOrDefault();
            player.Money = player.Money + filld.Price/2;
            filld.Status = "free";
            filld.Owner = null;
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.Entry(filld).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void PayRent(int gameId, int playerID, int filldNumber)
        {
            Filld filld = db.Fillds.Where(f => (f.GameID == gameId && f.NumberFilld == filldNumber)).FirstOrDefault();
            Player player = db.Players.Where(p => p.ID == playerID).FirstOrDefault();
            Player player2 = db.Players.Where(p => p.Color == filld.Owner && p.GameID == gameId).FirstOrDefault();
            player.Money = player.Money - filld.Rent;
            player2.Money = player2.Money + filld.Rent;
            db.Entry(player).State = System.Data.Entity.EntityState.Modified;
            db.Entry(player2).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public  void MovePlayer(int gameID, string playerColor, int toPlace)
        {
            Game game = db.Games.Find(gameID);
            Player playerNextMove;
            Player player;
            switch (playerColor)
            {

                case "Red":
                    player = db.Players.Find(game.PlayerRed);
                    player.PlaceInTable = toPlace;
                    player.Move = "false";
                    db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                   
                    playerNextMove = db.Players.Find(game.PlayerGreen);
                    if (playerNextMove.Status=="Lost")
                        playerNextMove = db.Players.Find(game.PlayerYellow);
                        playerNextMove.Move = "true";
                    if(playerNextMove.Status != "Lost")
                    db.Entry(playerNextMove).State = System.Data.Entity.EntityState.Modified;
                    else
                    {
                        game.Status = "GameOver";
                        db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    break;

                case "Green":
                    player = db.Players.Find(game.PlayerGreen);
                    player.PlaceInTable = toPlace;
                    player.Move = "false";
                    db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                    playerNextMove = db.Players.Find(game.PlayerYellow);
                    if (playerNextMove.Status == "Lost")
                        playerNextMove = db.Players.Find(game.PlayerRed);
                    playerNextMove.Move = "true";
                    if (playerNextMove.Status != "Lost")
                        db.Entry(playerNextMove).State = System.Data.Entity.EntityState.Modified;
                    else
                    {
                        game.Status = "GameOver";
                        db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    break;
                case "Yellow":
                    player = db.Players.Find(game.PlayerYellow);
                    player.PlaceInTable = toPlace;
                    player.Move = "false";
                    db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                    playerNextMove = db.Players.Find(game.PlayerRed);
                    if (playerNextMove.Status == "Lost")
                        playerNextMove = db.Players.Find(game.PlayerGreen);
                    playerNextMove.Move = "true";
                    if (playerNextMove.Status != "Lost")
                        db.Entry(playerNextMove).State = System.Data.Entity.EntityState.Modified;
                    else
                    {
                        game.Status = "GameOver";
                        db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    break;
            }



        }

    }
}