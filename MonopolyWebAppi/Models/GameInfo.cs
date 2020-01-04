using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopolyWebAppi4.Models
{
    public class GameInfo
    {
        public int ID_Game { get; set; }

        public string Game_Status { get; set; }
        public int PlayerRed_ID { get; set; }
        public string PlayerRed_Name { get; set; }
        public string PlayerRed_Color { get; set; }
        public string PlayerRed_Place { get; set; }
        public int PlayerRed_Money { get; set; }
        public string PlayerRed_Move { get; set; }
        public string PlayerRed_Status { get; set; }
        public string PlayerRed_Gender { get; set; }
        public string PlayerRed_Rank { get; set; }

        public int PlayerGreen_ID{ get; set; }
        public string PlayerGreen_Name { get; set; }
        public string PlayerGreen_Color { get; set; }
        public string PlayerGreen_Place { get; set; }
        public int PlayerGreen_Money { get; set; }
        public string PlayerGreen_Move { get; set; }
        public string PlayerGreen_Status { get; set; }
        public string PlayerGreen_Gender { get; set; }
        public string PlayerGreen_Rank { get; set; }

        public int PlayerYellow_ID { get; set; }
        public string PlayerYellow_Name { get; set; }
        public string PlayerYellow_Color { get; set; }
        public string PlayerYellow_Place { get; set; }
        public int PlayerYellow_Money { get; set; }
        public string PlayerYellow_Move { get; set; }
        public string PlayerYellow_Status { get; set; }
        public string PlayerYellow_Gender { get; set; }
        public string PlayerYellow_Rank { get; set; }
    }
}