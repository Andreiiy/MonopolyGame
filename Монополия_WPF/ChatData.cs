using ClientMonopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Monopoly_WPF
{
   public class ChatData
    {
        string HOST = "http://localhost:63569/api/";
        Client client;
        List<User> frends;



        Images images = new Images();

        public ChatData(Client client)
        {
         this.client = client;
        }

      public  List<User> GetFrends(int userID)
        {
                               
             frends = new List<User>();
            User frend = new User();
            frends.Add(new User { Name = "Friends", Status = "", Rank = "", ImgUrl = new Uri(@"/Monopoly_WPF;component/Image/Plaers/mens.png" , UriKind.Relative) });
            try
            {
            var users = client.GetFrends(userID); 
            foreach (var user in users)
            {
                frends.Add(new User {ID = user.ID, Name = user.Name, StatusInSystem = user.StatusInSystem, Rank = user.Rank, ImgUrl = images.GetRandomImage(user.Gender) });
            }
            }
            catch { }
            
                                        
            return frends;
        }
        public List<User> GetUsers(int userID)
        {

         List<User> usersTosystem = new List<User>();
           
           
            try
            {
                var users = client.GetUsersWithoutFriends(userID);
                foreach (var user in users)
                {
                    usersTosystem.Add(new User { ID = user.ID, Name = user.Name,Gender=user.Gender, StatusInSystem = user.StatusInSystem, Rank = user.Rank, ImgUrl = images.GetRandomImage(user.Gender) });
                }
            }
            catch { }


            return usersTosystem;
        }

        public List<User> GetUserbyName(string userName)
        {

            List<User> userTosystem = new List<User>();


            try
            {
                var user = client.GetUserbyName(userName);
                userTosystem.Add(new User { ID = user.ID, Name = user.Name, Gender = user.Gender, StatusInSystem = user.StatusInSystem, Rank = user.Rank, ImgUrl = images.GetRandomImage(user.Gender) });
                
            }
            catch { }


            return userTosystem;
        }

        public List<GameToList> GetGames(int userID)
        {
            User userGreen;
            
            List<GameToList> games = new List<GameToList>();
            List<GameInfo> gamesWeit = new List<GameInfo>();
            try
            {
                gamesWeit = client.GetGameWeit(userID);
            }
            catch { }
           
            if(gamesWeit.Count!=0)
                foreach (GameInfo gameinfo in gamesWeit)
                {

                   if(gameinfo.PlayerGreen_ID !=0)
                    {

                        userGreen = new User {ID=gameinfo.PlayerGreen_ID, Name = gameinfo.PlayerGreen_Name, Status = "online",
                            Rank = gameinfo.PlayerGreen_Rank, ImgUrl = images.GetRandomImage(gameinfo.PlayerGreen_Gender) };
                    }
                    else
                    {
                        userGreen = new User { Name = "", Status = "", Rank = "",
                            ImgUrl = new Uri(@"/Monopoly_WPF;component/Image/pl2.png" , UriKind.Relative) };
                    }

                    games.Add(new GameToList(gameinfo.ID_Game, gameinfo.Game_Status, new User {ID=gameinfo.PlayerRed_ID, Name = gameinfo.PlayerRed_Name, Status = "online",
                        Rank = gameinfo.PlayerRed_Rank, ImgUrl = images.GetRandomImage(gameinfo.PlayerRed_Gender) },
                                    userGreen,
                                    new User { Name = "", Status = "", Rank = "",
                            ImgUrl = new Uri(@"/Monopoly_WPF;component/Image/pl2.png", UriKind.Relative)}));
                }


            return games;
        }

        public List<MsgToUser> GetMessages(int userID,int frendID)
        {
            List<MsgToUser> messages = client.GetMessages(userID, frendID);
            return messages;
        }
    }
}
