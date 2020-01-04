using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClientMonopoly;

namespace Wcf_Chat
{
   

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        
        List<ServerUser> users = new List<ServerUser>();
        List<ServerUserInGame> usersInGame = new List<ServerUserInGame>();
        //int userID;
        // int nextid = 1;


        public string Connect(int clientID ,string clientName)
        {
            
           
            ServerUser serverUser = new ServerUser() {
                ID= clientID,
                Name= clientName,
                operationContext=OperationContext.Current
            };
            //nextid++;
            Console.WriteLine(clientName + " Connect in chat",0);

            users.Add(serverUser);

            return "true";
        }


        //public string ConnectPlayers(int clientID, int gameID, string clientName)
        //{
        //    ServerUserInGame serverUserInGame = new ServerUserInGame()
        //    {
        //        ID = clientID,
        //        GameID = gameID,
        //        Name = clientName,
        //        operationContext = OperationContext.Current
        //    };

        //    Console.WriteLine("Player " + serverUserInGame.ID + " Connect in Gamechat - gameID = " + serverUserInGame.GameID);

        //    usersInGame.Add(serverUserInGame);
        //    Console.WriteLine(usersInGame.Count);

        //    return "true";
        //}

        public string ConnectPlayers(int clientID, int gameID)
        {
            ServerUser user = users.Where(u => (u.ID == clientID)).FirstOrDefault();
            user.GameID = gameID;

            Console.WriteLine("Player " + user.ID + " Connect in Gamechat - gameID = " + user.GameID);

           

            return "true";
        }
        public void Disconnect(int id)
        {
            var user = users.Find(i => i.ID == id);
            if (user != null)
            {
             
               // SendMsgtoAll( " Disconnect",id,0);
                Console.WriteLine(user.Name + " Disconnect in chat", 0);
                users.Remove(user);
            }
                
        }

        public void DisconnectUserFromGame(int userID)
        {
            ServerUser user = users.Where(u => (u.ID == userID)).FirstOrDefault();
            user.GameID = 0;

            // SendMsgtoAll( " Disconnect",id,0);
            Console.WriteLine("Player " + user.ID + " Disconnect in chat", 0);
                
            
        }

        public void SendMsgtoAll(string msg,int userIdFrom,int id)
        {
                
                var user = users.FirstOrDefault(i => i.ID == userIdFrom);
            foreach(var item in users)
            {
                string answer = "";
                answer += user.Name + ">  ";
                answer += msg;
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer, userIdFrom, 0);
                
            }
        }

        public void SendMsgtoFriend(string msg, int userIdFrom, int userIdTo)
        {

                string answer = "";
            var userfrom = users.FirstOrDefault(i => i.ID == userIdFrom);
            var user = users.FirstOrDefault(i => i.ID == userIdTo);
                if (user != null)
                {
                    answer +=  userfrom.Name + ">   ";
                    answer += msg;
                Console.WriteLine(answer);
                user.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer, userIdFrom, userIdTo);

                }
                
            
        }

        public void SendMsgtoPlayers(string msg, int userIdFrom, int gameID)
        {           
            
            var userfrom = users.Where(i => i.ID == userIdFrom).FirstOrDefault();
           
            
            List<ServerUser> usersInGame = users.Where(i => i.GameID == gameID).ToList();
            
            foreach (var item in usersInGame)
            {
               
                if (item.ID != userIdFrom)
                {
                    string answer = "";
                    answer += userfrom.Name + ">  ";
                    answer += msg;
                    Console.WriteLine("Player " + userfrom.Name + "  send msg to Player "+ item.Name+" ID = "+ item.ID);
                    item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer, userIdFrom, 0);

                }

            }
        }
    }
}
