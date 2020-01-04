using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_WPF
{
    public class Images
    {
        List<string> imagesMans = new List<string>();
        List<string> imagesGirls = new List<string>();
        List<string> imagesChance = new List<string>();

        Random rand;
        public Images()
        {
            rand = new Random();
            imagesMans.Add("Image\\Plaers\\player1.jpg");
            imagesMans.Add("Image\\Plaers\\player2.jpg");
            imagesMans.Add("Image\\Plaers\\player3.jpg");
            imagesMans.Add("Image\\Plaers\\player4.png");
            imagesMans.Add("Image\\Plaers\\player5.jpg");
            imagesMans.Add("Image\\Plaers\\player6.jpg");
            imagesMans.Add("Image\\Plaers\\player7.jpg");
            imagesMans.Add("Image\\Plaers\\player8.jpg");
            imagesMans.Add("Image\\Plaers\\player9.jpg");
            imagesMans.Add("Image\\Plaers\\player1.jpg");

            imagesGirls.Add("Image\\Plaers\\playergirl1.jpg");
            imagesGirls.Add("Image\\Plaers\\playergirl2.jpg");
            imagesGirls.Add("Image\\Plaers\\playergirl1.jpg");

            imagesChance.Add("Image\\Chance\\get10.png");
            imagesChance.Add("Image\\Chance\\get15.png");
            imagesChance.Add("Image\\Chance\\pay10.png");
            imagesChance.Add("Image\\Chance\\pay15.png");
            imagesChance.Add("Image\\Chance\\gotojoil.png");
            imagesChance.Add("Image\\Chance\\gotostart.jpg");
        }


        public Uri GetRandomImage(string gender)
        {
            if (gender.Equals("male"))
            {
                Uri ImgUrl = new Uri(@"/Monopoly_WPF;component/" + imagesMans[rand.Next(0, imagesMans.Count - 1)], UriKind.Relative);
                return ImgUrl;
            }
            else
            {
                Uri ImgUrl = new Uri(@"/Monopoly_WPF;component/" + imagesGirls[rand.Next(0, imagesGirls.Count - 1)], UriKind.Relative);

                return ImgUrl;
            }

        }

        public Uri GetImageChance(string chance)
        {
            switch (chance)
            {
                case "get10":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\get10.png", UriKind.Relative);
                case "get15":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\get15.png", UriKind.Relative);
                case "pay15":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\pay15.png", UriKind.Relative);
                case "pay10":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\pay10.png", UriKind.Relative);
                case "gotostart":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\gotostart.png", UriKind.Relative);
                case "gotojoil":
                    return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\gotojoil.png", UriKind.Relative);
                default:
                    break;

            }
            return new Uri(@"/Monopoly_WPF;component/" + "Image\\Chance\\Chance.png", UriKind.Relative);
        }
    }
}
