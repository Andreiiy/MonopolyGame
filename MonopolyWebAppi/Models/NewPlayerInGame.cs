using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopolyWebAppi4.Models
{
    public class NewPlayerInGame
    {

        public int gameID { get; set; }
        public int playerID { get; set; }
        public string Player_Name { get; set; }
        public string Player_Color { get; set; }
        public int Player_Place { get; set; }
        public int Player_Money { get; set; }
        public string Player_Move { get; set; }
        public string Player_Status { get; set; }
    }
}