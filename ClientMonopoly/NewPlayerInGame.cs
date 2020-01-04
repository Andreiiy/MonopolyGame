using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
   public struct NewPlayerInGame
    {
        public int gameID;
        public int playerID;
        public string Player_Name;
        public string Player_Color;
        public int Player_Place;
        public int Player_Money;
        public string Player_Move;
        public string Player_Status;

       public NewPlayerInGame(NameValueCollection list)
    {
        gameID = int.Parse(list["gameID"]);
        playerID = int.Parse(list["playerID"]);
        Player_Name = list["Player_Name"];
        Player_Color = list["Player_Color"];
        Player_Place = int.Parse(list["Player_Place"]);
        Player_Money = int.Parse(list["Player_Money"]);
        Player_Move = list["Player_Move"];
        Player_Status = list["Player_Status"];

      }
    }

    
}
