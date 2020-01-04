using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_WPF
{
   public class GameToList
    {
       public int gameID { get; set; }
        public string status { get; set; }
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public User Player3 { get; set; }

        public GameToList(int gameid,string status,User player1, User player2=null, User player3=null)
        {
            this.gameID = gameid;
            this.status = status;
            this. Player1 = player1;
            this. Player2 = player2;
            this.Player3 = player3;
        }
    }
}
