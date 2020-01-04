using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClientMonopoly;

namespace ClientMonopoly
{
   public class Client
    {
       public string host { get; private set; }
        

        public Client(string host)
        {
           this.host = host;
           
        }


        ///Functions ///////////////////////////////////////////////////////////////////////
        
        public string Entry(string userName, string password)
        {
            User user = new User();
            user =user.GetUser(ParseJason( CallServer("Entry/"+ "GetChackEntryAsync/" + userName+"/"+ password)));
            if(user.ID == -1)
            return "false";

            return user.ID.ToString();
        }


        public string Registration(string userName,string email,string password,string gender)
        {
            var respons = CallServer("Registration/"+ "GetRegistrationAsync/" + userName + "/" + email + "/" + password + "/" + gender);
            string res = (string)respons;
            return res;
        }


        public void AddUserToFrends(int userId, int frendId)
        {
            UserFriend userFrends = new UserFriend();
                
          CallServer("UserFriends/"+ "GetAddUserToFriends/" + userId + "/" + frendId);
            CallServer("UserFriends/" + "GetAddUserToFriends/" + frendId + "/" + userId);
        }

        public void DellGame(int userId, int gameId)
        {
         CallServer("Games/"+ "GetDellGame/" + userId + "/" + gameId );
        }

        public void DellPlayerFromGame(int userId, int gameId)
        {
            CallServer("Games/"+ "GetDellPlayerFromGame/" + userId + "/" + gameId);
        }

        public void  ExitFromSystemAsync(int userID)
        {
            CallServer("Entry/"+ "GetExitFromSystemAsync/" + userID);
        }

        public List<User> GetFrends(int userId)
        {

            List<User> users = new List<User>();
            NameValueCollection list = ParseJasonToList(CallServer("UserFriends/"+ "GetUserFriendAsync/" + userId));
            List <UserFriend> frends = GetUserFriends(list);
            foreach (UserFriend f in frends)
            {
                users.Add(GetUser(f.FreindId));
            }
            return users;
        }

        public List<User> GetUsers()
        {
            User user = new User();
            NameValueCollection list = ParseJasonToList(CallServer("Users/"+ "GetUsers"));
            List <User> users = user.GetUsers(list);
           
            return users;
        }

        public List<User> GetUsersWithoutFriends(int userID)
        {
            User user = new User();
            NameValueCollection list = ParseJasonToList(CallServer("Users/" + "GetUsersWithoutFriends/"+ userID));
            List<User> users = user.GetUsers(list);

            return users;
        }

        private List<UserFriend> GetUserFriends(NameValueCollection list)
        {

            List<UserFriend> friends = new List<UserFriend>();

            string[] id = null;
            string[] userid = null;
            string[] friendid = null;
            foreach (string key in list)
            {


                if (key == "ID")
                {
                    String ID = list[key];
                    id = ID.Split(',');
                }


                if (key == "UserId")
                {
                    String Userid = list[key];
                    userid = Userid.Split(',');
                }


                if (key == "FreindId")
                {
                    String FreindId = list[key];
                    friendid = FreindId.Split(',');
                }
            }
            int i = 0;
            while (i < id.Length)
            {
                ClientMonopoly.UserFriend friend = new UserFriend();
                if (id[i] != "null")
                    friend.ID = Int32.Parse(id[i]);
                if (userid[i] != "null")
                    friend.UserId = Int32.Parse(userid[i]);
                if (friendid[i] != "null")
                    friend.FreindId = Int32.Parse(friendid[i]);

                friends.Add(friend);
                i++;
            }
            return friends;
        }


        public List<MsgToUser> GetMessages(int userIdFrom, int userIdTo)
        {
           
            List<MsgToUser> messages = new List<MsgToUser>();
            MsgToUser message = new MsgToUser();
            List<MsgToUser> listfrom = message.GetMessages(ParseJasonToList(CallServer("MsgToUsers/"+ "GetMsgToUserAsync/" + userIdFrom + "/" + userIdTo)));
            List<MsgToUser> listto = message.GetMessages(ParseJasonToList(CallServer("MsgToUsers/" + "GetMsgToUserAsync/" + userIdTo + "/" + userIdFrom)));

            if(listfrom.Count!=0)
            listfrom[listfrom.Count - 1].Message = listfrom[listfrom.Count - 1].Message.Remove(listfrom[listfrom.Count - 1].Message.Length - 3);
            if (listto.Count != 0)
                listto[listto.Count - 1].Message = listto[listto.Count - 1].Message.Remove(listto[listto.Count - 1].Message.Length - 3);
            listfrom.Concat(listto);
            var resalt = listfrom.Concat(listto);
            resalt.OrderBy(p => p.ID).ToList<MsgToUser>();

            foreach (MsgToUser item in resalt)
            {
                messages.Add(item);
            }
            messages.Sort((a, b) => a.ID.CompareTo(b.ID));
            return messages;
        }

        public async Task SendMessageToUserAsync(string msg, int userIdFrom, int userIdTo)
        {
            await Task.Run(() => SendMessageToUser(msg, userIdFrom, userIdTo));
        }

        public void SendMessageToUser(string msg,int userIdFrom, int userIdTo)
        {
            MsgToUser message = new MsgToUser();
            CallServer("MsgToUsers/"+ "GetSendtMsgToUserAsync/" + msg + "/" + userIdFrom + "/"+userIdTo );
        }


        public List<GameInfo> GetGameWeit(int userID)
        {
            GameInfo game = new GameInfo();
            List<GameInfo> games = new List<GameInfo>();
            games = game.GetListGameInfo(ParseJasonToList(CallServer("Games/"+ "GetGames/"+userID)));
            return games;
        }

        //Create new game
        /// <summary>
        /// the function sends a request to the server to create a new game and receives a response with the data of the created player
        /// </summary>
        /// <param name="userID"> User ID</param>
        /// <param name="statusGame">initial status for the created game</param>
        /// <returns>new player information</returns>
        public NewPlayerInGame CreateGame(int userID, string statusGame)
        {
            return new NewPlayerInGame(ParseJason(CallServer("Games/"+ "GetCreateGame/"+statusGame+"/" + userID)));
          
        }


       public NewPlayerInGame  AddPlayerToGame(int userID, int gameID)
        {
            return new NewPlayerInGame(ParseJason(CallServer("Games/"+ "GetAddPlayerToGame/" + userID+ "/"+gameID)));
           
        }


        public GameInfo GetGameInfo(int gameId)
        {
            NameValueCollection list= ParseJason(CallServer("Games/"+ "GetGameInfo/" + gameId ));
            GameInfo info = new GameInfo();
            info = info.GetGameInfo(list);
            return info;
            
        }


        public User GetUser(int userID)
        {
            User user = new User();
            return user.GetUser(ParseJason(CallServer("Users/"+ "GetUser/" + userID)));
        }

        public User GetUserbyName(string userName)
        {
            User user = new User();
            return user.GetUser(ParseJason(CallServer("Users/" + "GetUserbyName/" + userName)));
        }

        public Player GetPlayer(int userID, int gameID)
        {
            Player player = new Player();

            return player.GetPlayer(ParseJason(CallServer("Players/" + "GetPlayer/" + userID +"/"+ gameID)));
        }

        public Player GetPlayerbyID(int playerID)
        {
            Player player = new Player();

            return player.GetPlayer(ParseJason(CallServer("Players/" + "GetPlayerbyID/" + playerID)));
        }

        public GameInfo PlayerMove(int gameId,string playerColor,int toPlace)
        {
            GameInfo info = new GameInfo();
            return  info.GetGameInfo(ParseJason(CallServer("Players/"+ "GetMovePlayer/" + gameId + "/" + playerColor+"/"+toPlace)));
            
        }


        public void FilldBuy(int gameId, int playerID, int numberFild)
        {
           CallServer("Fillds/"+ "GetFilldBuy/" + gameId + "/" + playerID + "/" + numberFild);

        }
        
            public void FilldSell(int gameId, int playerID, int numberFild)
        {
            CallServer("Fillds/" + "GetFilldSell/" + gameId + "/" + playerID + "/" + numberFild);

        }
        public void PayRent(int gameId, int playerID, int numberFild)
        {
            CallServer("Fillds/" + "GetPayRent/" + gameId + "/" + playerID + "/" + numberFild);

        }
        public void PlayerLost( int playerID)
        {
            CallServer("Players/" + "GetPlayerLost/" + playerID);

        }
        public void PlayerMyGameWinner(int userID)
        {
            CallServer("Players/" + "GetPlayerMyGameWinner/" + userID);

        }
        public void PlayerAddMoney(int playerID, int sum)
        {
            CallServer("Players/" + "GetPlayerAddMoney/" + playerID+"/"+sum);

        }
        public void PlayerReduceMoney(int playerID, int sum)
        {
            CallServer("Players/" + "GetPlayerReduceMoney/" + playerID + "/" + sum);

        }
        public Field GetField(int gameId, int numberFild)
        {
            Field field = new Field();
            
            return  field.GetField(ParseJason(CallServer("Fillds/" + "GetField/" + gameId + "/" + numberFild)));

        }

        public List<Field> GetFields(int gameId)
        {
            Field field = new Field();

            return field.GetFields(ParseJasonToList(CallServer("Fillds/" + "GetFields/" + gameId)));

        }

        public void FilldDeposit(int gameId, string status, int numberFild)
        {
            CallServer("Fillds/"+ "GetFilldDepositAsync/" + gameId + "/" + status + "/" + numberFild );

        }


        public string CheckReadyPlay(int gameId)
        {
         var respons=  CallServer("Games/"+ "GetCheckReadyPlay/" + gameId);
            string res = (string)respons;
            return res;
        }

               
        private string CallServer(string param = "")
        {
            WebRequest request = WebRequest.Create(host + param);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
                     

        private NameValueCollection ParseJason(string json)
        {
            NameValueCollection list = new NameValueCollection();
            string pattern = @"""(\w+)\"":""?([^,""}]*)""?";

            foreach(Match m in Regex.Matches(json,pattern))
            {
                if (m.Groups.Count == 3)
                    list[m.Groups[1].Value] = m.Groups[2].Value;
            }
            return list;
        }


        private NameValueCollection ParseJasonToList(string json)
        {
            NameValueCollection list = new NameValueCollection();
            string pattern = @"""(\w+)\"":""?([^,""}]*)""?";

            foreach (Match m in Regex.Matches(json, pattern))
            {
                list.Add(m.Groups[1].Value, m.Groups[2].Value);
            }
            return list;
        }
    }
}
