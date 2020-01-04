using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monopoly_WPF
{
    public class Player
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public int Money { get; set; }


        public string Color { get; set; }

        public int Place { get; set; }


        public string Move { get; set; }

        public int NumberSkips { get; set; }

        public List<Field> myFilds = new List<Field>();

        public bool difficult { get; set; }

        /// <summary>
        /// method for buying a field by a player
        /// </summary>
        /// <param name="board">Object type Board</param>
        /// <param name="clientGame">Object type ClientMonopoly.Client</param>
        /// <param name="gameID">Game ID</param>
        /// <returns>if the player  buy a field he returns true if not false</returns>
        public virtual bool buyField( Board board, ClientMonopoly.Client clientGame , int gameID)
        {
            if (Money > board.GetFields()[Place].price)
            {
                ModalWindow winMod = new ModalWindow(board.GetFields()[Place]);
                //showing a dialog box where the player chooses to buy the field or not
                winMod.ShowDialog(); 
                if (winMod.bay == true)    //player selection check
                {
                    try
                    {
                        //if the network game sends a request to the server
                        clientGame.FilldBuy(gameID, ID, board.GetFields()[Place].number); 
                    }
                    catch { }
                    board.BuyField(board.GetFields()[Place].number, this);//changes the field data on the board
                    myFilds.Add(board.GetFields()[Place]);
                    Money = Money - board.GetFields()[Place].price;       //reduces player money
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// method for paying rent of another field
        /// </summary>
        /// <param name="board">Object type Board</param>
        /// <param name="mainWindow">Object type MainWindow</param>
        public void PayRent(Board board, MainWindow mainWindow)
        {
            if (Money < board.GetFields()[Place].rent)
            {
                //if the player does not have enough money to pay the rent but he has fields
                while (Money < board.GetFields()[Place].rent && Status != "Lost")
                {
                    if (board.GetMyFillds(Color).Count != 0)//if he has fields
                    {
                        if (Color == "Red")   //if the player is not a computer
                        {
                            WindowSellField windowSellField = new WindowSellField(board.GetMyFillds(Color));
                            windowSellField.ShowDialog();                      //shows a window for choosing a field for sale
                            Money += windowSellField.fieldSell.price / 2;
                            board.SellField(windowSellField.fieldSell.number); //changes the field data on the board
                        }
                        else   //if the player is  a computer
                        {
                            Money += board.GetMyFillds(Color)[0].price / 2;
                            //the computer sells the field first in the list of its fields
                            board.SellField(board.GetMyFillds(Color)[0].number); 
                        }
                    }
                    else
                    {
                        Status = "Lost";
                        if (Color == "Red") mainWindow.myPlayer.Visibility = System.Windows.Visibility.Hidden;
                        else if (Color == "Green") mainWindow.player2.Visibility = System.Windows.Visibility.Hidden;
                        else mainWindow.player3.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
            }
        }

        public bool playerGoToPlace(MainWindow mainWindow,List<Player> lPlayers, Board board)
        {
            Image image3 = null;
            switch (Color)
            {
                case "Red":
                    image3 = mainWindow.red;
                    break;
                case "Green":
                    image3 = mainWindow.green;
                    break;
                case "Yellow":
                    image3 = mainWindow.yellow;
                    break;
            }
            int shiftLeft = 0;
            int shiftTop = 0;
            if (Place <= 8 || Place >= 15 && Place <= 23)
                shiftTop = shiftPlayer(this,  lPlayers);
            else shiftLeft = shiftPlayer(this,lPlayers);

            double iTop = (double)image3.GetValue(Canvas.TopProperty);
            double iLeft = (double)image3.GetValue(Canvas.LeftProperty);

            if (iLeft != board.GetFields()[Place].left + shiftLeft || iTop != board.GetFields()[Place].top + shiftTop)
            {
                if (iLeft < board.GetFields()[Place].left + shiftLeft)
                {
                    Canvas.SetLeft(image3, iLeft + 1);
                }
                else if (iLeft > board.GetFields()[Place].left)
                    Canvas.SetLeft(image3, iLeft - 1);
                if (iTop < board.GetFields()[Place].top + shiftTop)
                    Canvas.SetTop(image3, iTop + 1);
                else if (iTop > board.GetFields()[Place].top)
                    Canvas.SetTop(image3, iTop - 1);

                return false;
            }
            else return true;
        }



        private int shiftPlayer(Player player, List<Player> lPlayers)
        {
            int shift = 0;
            foreach (Player item in lPlayers)
            {
                if (item.Place == player.Place && item.Color != player.Color)
                    shift += 30;
            }

            return shift;
        }
    }
}
