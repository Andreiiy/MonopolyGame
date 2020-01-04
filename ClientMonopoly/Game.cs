using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
    public class Game
    {
        public int ID { get; set; }

        public int PlayerRed { get; set; }

        public int PlayerGreen { get; set; }

        public int PlayerYellow { get; set; }


        public string Status { get; set; }



        public List<Game> GetGames(NameValueCollection list)
        {

            List<Game> games = new List<Game>();

            string[] id = null;
            string[] playerRed = null;
            string[] playerGreen = null;
            string[] playerYellow = null;
            string[] status = null;

            foreach (string key in list)
            {

                if (key == "ID")
                {
                    String ID = list[key];
                    id = ID.Split(',');
                }
                if (key == "PlayerRed")
                {
                    String PlayerRed = list[key];
                    playerRed = PlayerRed.Split(',');
                }
                if (key == "PlayerGreen")
                {
                    String PlayerGreen = list[key];
                    playerGreen = PlayerGreen.Split(',');
                }
                if (key == "PlayerYellow")
                {
                    String PlayerYellow = list[key];
                    playerYellow = PlayerYellow.Split(',');
                }
                if (key == "Status")
                {
                    String Status = list[key];
                    status = Status.Split(',');
                }
            }
            int i = 0;
            while (i < id.Length)
            {
                Game game = new Game();
                if (id[i] != "null")
                    game.ID = Int32.Parse(id[i]);

                if (playerRed[i] != "null")
                    game.PlayerRed = Int32.Parse(playerRed[i]);

                if (playerGreen[i] != "null")
                    game.PlayerGreen = Int32.Parse(playerGreen[i]);

                if (playerYellow[i] != "null")
                    game.PlayerYellow = Int32.Parse(playerYellow[i]);

                game.Status = status[i];
                games.Add(game);
                i++;
            }
            return games;
        }
    }              
}
