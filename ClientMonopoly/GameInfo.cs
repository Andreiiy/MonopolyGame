using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
    public class GameInfo
    {
        public int ID_Game;
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

        public int PlayerGreen_ID { get; set; }
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

        public GameInfo GetGameInfo(NameValueCollection list)
        {
            GameInfo info = new GameInfo();
            info.ID_Game = int.Parse(list["ID_Game"]);
            info.Game_Status = list["Game_Status"];
            info.PlayerRed_ID = int.Parse(list["PlayerRed_ID"]);
            info.PlayerRed_Name = list["PlayerRed_Name"];
            info.PlayerRed_Color = list["PlayerRed_Color"];
            info.PlayerRed_Place = list["PlayerRed_Place"];
            info.PlayerRed_Money = int.Parse(list["PlayerRed_Money"]);
            info.PlayerRed_Move = list["PlayerRed_Move"];
            info.PlayerRed_Status = list["PlayerRed_Status"];
            info.PlayerRed_Gender = list["PlayerRed_Gender"];
            info.PlayerRed_Rank = list["PlayerRed_Rank"];

            info.PlayerGreen_ID = int.Parse(list["PlayerGreen_ID"]);
            info.PlayerGreen_Name = list["PlayerGreen_Name"];
            info.PlayerGreen_Color = list["PlayerGreen_Color"];
            info.PlayerGreen_Place = list["PlayerGreen_Place"];
            info.PlayerGreen_Money = int.Parse(list["PlayerGreen_Money"]);
            info.PlayerGreen_Move = list["PlayerGreen_Move"];
            info.PlayerGreen_Status = list["PlayerGreen_Status"];
            info.PlayerGreen_Gender = list["PlayerGreen_Gender"];
            info.PlayerGreen_Rank = list["PlayerGreen_Rank"];

            info.PlayerYellow_ID = int.Parse(list["PlayerYellow_ID"]);
            info.PlayerYellow_Name = list["PlayerYellow_Name"];
            info.PlayerYellow_Color = list["PlayerYellow_Color"];
            info.PlayerYellow_Place = list["PlayerYellow_Place"];
            info.PlayerYellow_Money = int.Parse(list["PlayerYellow_Money"]);
            info.PlayerYellow_Move = list["PlayerYellow_Move"];
            info.PlayerYellow_Status = list["PlayerYellow_Status"];
            info.PlayerYellow_Gender = list["PlayerYellow_Gender"];
            info.PlayerYellow_Rank = list["PlayerYellow_Rank"];

            return info;
        }

        public List<GameInfo> GetListGameInfo(NameValueCollection list)
        {
            List<GameInfo> infos = new List<GameInfo>();
            #region arrays
            GameInfo info = new GameInfo();
            string[] Game_ID = null;
            string[] Game_Status = null;
            string[] PlayerRed_ID = null;
            string[] PlayerRed_Name = null;
            string[] PlayerRed_Color = null;
            string[] PlayerRed_Place = null;
            string[] PlayerRed_Money = null;
            string[] PlayerRed_Move = null;
            string[] PlayerRed_Status = null;
            string[] PlayerRed_Gender = null;
            string[] PlayerRed_Rank = null;

            string[] PlayerGreen_ID = null;
            string[] PlayerGreen_Name = null;
            string[] PlayerGreen_Color = null;
            string[] PlayerGreen_Place = null;
            string[] PlayerGreen_Money = null;
            string[] PlayerGreen_Move = null;
            string[] PlayerGreen_Status = null;
            string[] PlayerGreen_Gender = null;
            string[] PlayerGreen_Rank = null;

            string[] PlayerYellow_ID = null;
            string[] PlayerYellow_Name = null;
            string[] PlayerYellow_Color = null;
            string[] PlayerYellow_Place = null;
            string[] PlayerYellow_Money = null;
            string[] PlayerYellow_Move = null;
            string[] PlayerYellow_Status = null;
            string[] PlayerYellow_Gender = null;
            string[] PlayerYellow_Rank = null;
            #endregion

            foreach (string key in list)
            {

                if (key == "ID_Game")
                {
                    String ID = list[key];
                    Game_ID = ID.Split(',');
                }
                if (key == "Game_Status")
                {
                    String game_Status = list[key];
                    Game_Status = game_Status.Split(',');
                }
                #region PlayerRed
                if (key == "PlayerRed_ID")
                {
                    String playerRed_ID = list[key];
                    PlayerRed_ID = playerRed_ID.Split(',');
                }
                if (key == "PlayerRed_Name")
                {
                    String playerRed_Name = list[key];
                    PlayerRed_Name = playerRed_Name.Split(',');
                }
                if (key == "PlayerRed_Color")
                {
                    String playerRed_Color = list[key];
                    PlayerRed_Color = playerRed_Color.Split(',');
                }
                if (key == "PlayerRed_Place")
                {
                    String playerRed_Place = list[key];
                    PlayerRed_Place = playerRed_Place.Split(',');
                }

                if (key == "PlayerRed_Money")
                {
                    String playerRed_Money = list[key];
                    PlayerRed_Money = playerRed_Money.Split(',');
                }
                if (key == "PlayerRed_Move")
                {
                    String playerRed_Move = list[key];
                    PlayerRed_Move = playerRed_Move.Split(',');
                }
                if (key == "PlayerRed_Status")
                {
                    String Status = list[key];
                    PlayerRed_Status = Status.Split(',');
                }
                if (key == "PlayerRed_Gender")
                {
                    String playerRed_Gender = list[key];
                    PlayerRed_Gender = playerRed_Gender.Split(',');
                }
                if (key == "PlayerRed_Rank")
                {
                    String playerRed_Rank = list[key];
                    PlayerRed_Rank = playerRed_Rank.Split(',');
                }
                #endregion

                #region PlayerGreen
                if (key == "PlayerGreen_ID")
                {
                    String playerGreen_ID = list[key];
                    PlayerGreen_ID = playerGreen_ID.Split(',');
                }
                if (key == "PlayerGreen_Name")
                {
                    String playerGreen_Name = list[key];
                    PlayerGreen_Name = playerGreen_Name.Split(',');
                }
                if (key == "PlayerGreen_Color")
                {
                    String playerGreen_Color = list[key];
                    PlayerGreen_Color = playerGreen_Color.Split(',');
                }
                if (key == "PlayerGreen_Place")
                {
                    String playerGreen_Place = list[key];
                    PlayerGreen_Place = playerGreen_Place.Split(',');
                }

                if (key == "PlayerGreen_Money")
                {
                    String playerGreen_Money = list[key];
                    PlayerGreen_Money = playerGreen_Money.Split(',');
                }
                if (key == "PlayerGreen_Move")
                {
                    String playerGreen_Move = list[key];
                    PlayerGreen_Move = playerGreen_Move.Split(',');
                }
                if (key == "PlayerGreen_Status")
                {
                    String Status = list[key];
                    PlayerGreen_Status = Status.Split(',');
                }
                if (key == "PlayerGreen_Gender")
                {
                    String playerGreen_Gender = list[key];
                    PlayerGreen_Gender = playerGreen_Gender.Split(',');
                }
                if (key == "PlayerGreen_Rank")
                {
                    String playerGreen_Rank = list[key];
                    PlayerGreen_Rank = playerGreen_Rank.Split(',');
                }
                #endregion

                #region PlayerYellow
                if (key == "PlayerYellow_ID")
                {
                    String playerYellow_ID = list[key];
                    PlayerYellow_ID = playerYellow_ID.Split(',');
                }
                if (key == "PlayerYellow_Name")
                {
                    String playerYellow_Name = list[key];
                    PlayerYellow_Name = playerYellow_Name.Split(',');
                }
                if (key == "PlayerYellow_Color")
                {
                    String playerYellow_Color = list[key];
                    PlayerYellow_Color = playerYellow_Color.Split(',');
                }
                if (key == "PlayerYellow_Place")
                {
                    String playerYellow_Place = list[key];
                    PlayerYellow_Place = playerYellow_Place.Split(',');
                }

                if (key == "PlayerYellow_Money")
                {
                    String playerYellow_Money = list[key];
                    PlayerYellow_Money = playerYellow_Money.Split(',');
                }
                if (key == "PlayerYellow_Move")
                {
                    String playerYellow_Move = list[key];
                    PlayerYellow_Move = playerYellow_Move.Split(',');
                }
                if (key == "PlayerYellow_Status")
                {
                    String Status = list[key];
                    PlayerYellow_Status = Status.Split(',');
                }
                if (key == "PlayerYellow_Gender")
                {
                    String playerYellow_Gender = list[key];
                    PlayerYellow_Gender = playerYellow_Gender.Split(',');
                }
                if (key == "PlayerYellow_Rank")
                {
                    String playerYellow_Rank = list[key];
                    PlayerYellow_Rank = playerYellow_Rank.Split(',');
                }
                #endregion
            }
            int i = 0;
            while (i < Game_ID.Length)
            {
                GameInfo gameinfo = new GameInfo();
                if (Game_ID[i] != "null")
                    gameinfo.ID_Game = Int32.Parse(Game_ID[i]);
                if (Game_Status[i] != "null")
                    gameinfo.Game_Status = Game_Status[i];
                #region PlayerRed
                if (PlayerRed_ID[i] != "null")
                    gameinfo.PlayerRed_ID = Int32.Parse(PlayerRed_ID[i]);

                if (PlayerRed_Name[i] != "null")
                    gameinfo.PlayerRed_Name = PlayerRed_Name[i];

                if (PlayerRed_Color[i] != "null")
                    gameinfo.PlayerRed_Color = PlayerRed_Color[i];

                if (PlayerRed_Place[i] != "null")
                    gameinfo.PlayerRed_Place = PlayerRed_Place[i];

                if (PlayerRed_Money[i] != "null")
                    gameinfo.PlayerRed_Money = Int32.Parse(PlayerRed_Money[i]);

                if (PlayerRed_Move[i] != "null")
                    gameinfo.PlayerRed_Move = PlayerRed_Move[i];

                if (PlayerRed_Status[i] != "null")
                    gameinfo.PlayerRed_Status = PlayerRed_Status[i];
                if (PlayerRed_Gender[i] != "null")
                    gameinfo.PlayerRed_Gender = PlayerRed_Gender[i];
                if (PlayerRed_Rank[i] != "null")
                    gameinfo.PlayerRed_Rank = PlayerRed_Rank[i];
                #endregion
                #region PlayerGreen
                if (PlayerGreen_ID[i] != "null")
                    gameinfo.PlayerGreen_ID = Int32.Parse(PlayerGreen_ID[i]);

                if (PlayerGreen_Name[i] != "null")
                    gameinfo.PlayerGreen_Name = PlayerGreen_Name[i];

                if (PlayerGreen_Color[i] != "null")
                    gameinfo.PlayerGreen_Color = PlayerGreen_Color[i];

                if (PlayerGreen_Place[i] != "null")
                    gameinfo.PlayerGreen_Place = PlayerGreen_Place[i];

                if (PlayerGreen_Money[i] != "null")
                    gameinfo.PlayerGreen_Money = Int32.Parse(PlayerGreen_Money[i]);

                if (PlayerGreen_Move[i] != "null")
                    gameinfo.PlayerGreen_Move = PlayerGreen_Move[i];

                if (PlayerGreen_Status[i] != "null")
                    gameinfo.PlayerGreen_Status = PlayerGreen_Status[i];
                if (PlayerGreen_Gender[i] != "null")
                    gameinfo.PlayerGreen_Gender = PlayerGreen_Gender[i];
                if (PlayerGreen_Rank[i] != "null")
                    gameinfo.PlayerGreen_Rank = PlayerGreen_Rank[i];
                #endregion
                #region PlayerYellow
                if (PlayerYellow_ID[i] != "null")
                    gameinfo.PlayerYellow_ID = Int32.Parse(PlayerYellow_ID[i]);

                if (PlayerYellow_Name[i] != "null")
                    gameinfo.PlayerYellow_Name = PlayerYellow_Name[i];

                if (PlayerYellow_Color[i] != "null")
                    gameinfo.PlayerYellow_Color = PlayerYellow_Color[i];

                if (PlayerYellow_Place[i] != "null")
                    gameinfo.PlayerYellow_Place = PlayerYellow_Place[i];

                if (PlayerYellow_Money[i] != "null")
                    gameinfo.PlayerYellow_Money = Int32.Parse(PlayerYellow_Money[i]);

                if (PlayerYellow_Move[i] != "null")
                    gameinfo.PlayerYellow_Move = PlayerYellow_Move[i];

                if (PlayerYellow_Status[i] != "null")
                    gameinfo.PlayerYellow_Status = PlayerYellow_Status[i];
                if (PlayerYellow_Gender[i] != "null")
                    gameinfo.PlayerYellow_Gender = PlayerYellow_Gender[i];
                if (PlayerYellow_Rank[i] != "null")
                    gameinfo.PlayerYellow_Rank = PlayerYellow_Rank[i];
                #endregion
                infos.Add(gameinfo);
                i++;
            }

            return infos;
        }
    }
}
