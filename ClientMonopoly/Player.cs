using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
   public class Player
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int GameID { get; set; }

        public int Money { get; set; }

        public int PlaceInTable { get; set; }

        public string Color { get; set; }

        public string Status { get; set; }

        public string Move { get; set; }

        public Player GetPlayer(NameValueCollection list)
        {
            Player player = new Player();
            player.ID = Int32.Parse(list["ID"]);
            
            player.UserID = Int32.Parse(list["UserID"]);
            
            player.Money = Int32.Parse(list["Money"]);
           
            player.PlaceInTable = Int32.Parse(list["PlaceInTable"]);
          
            player.Color = list["Color"];
            
            player.Status = list["Status"];
          
            player.Move = list["Move"];
            
            return player;
        }
    }

}
