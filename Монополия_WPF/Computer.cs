using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_WPF
{
    public class Computer : Player
    {

        /// <summary>
        /// a method that checks the conditions of the purchase of a field by computer
        /// </summary>
        /// <param name="board"> Object type Board</param>
        /// <param name="clientGame"> Object type ClientMonopoly.Client</param>
        /// <param name="gameID"> Game ID</param>
        /// <returns>if the computer can buy a field it returns true if not false </returns>
        public override bool buyField(Board board, ClientMonopoly.Client clientGame, int gameID) //7,6,14,16
        {
            //return true if computer can to buy field and he still has 30,000 thousand
            if (Money - board.GetField(Place).price > 30000)
                return true;
            //return false if the computer does not have enough money
            else if (Money < board.GetField(Place).price)
                return false;

            //returns true if the computer is on a frequently accessed field
            if (Money > board.GetField(Place).price && board.GetField(Place).price == 6 || board.GetField(Place).price == 7 ||
                                      board.GetField(Place).price == 14 || board.GetField(Place).price == 16
                                      && difficult == true)
                return true;
            //return true if the field is next to the computer too
            else if (Money > board.GetField(Place).price && board.GetField(Place - 1).owner == Color ||
                                           board.GetField(Place + 1).owner == Color
                                           && difficult == true)
                return true;

            return false;
        }

       
    }
}
