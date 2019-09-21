using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;

namespace TicTacToe
{
    class Player
    {
        #region Class members and properties

        public char PlayerSign { get; set; }
        public int NumberOfMoves { get; set; }
        
        public int Xpoints = 0;
        public int Opoints = 0;

        Random random = new Random();

        #endregion

        #region Constuctor

        public Player()
        {

        }
        #endregion

        public bool IsInvalidInput(char? sign)
        {
            if (sign != 'X' && sign != 'x' && sign != 'O' && sign != 'o' || sign == null || !sign.HasValue)

            {
                Console.Write("Like it says in parantheses: \"Must be X or O\"!");
                return true;
            }
            return false;
        }

        public char SetPlayerSign()
        {
            char sign = ' ';
            do
            {
                Console.Write("\r\nSet Player 1 sign(Must be X or O): ");
                sign = Convert.ToChar(Console.ReadLine());
            } while (char.IsDigit(sign) || IsInvalidInput(sign) ||
            char.IsPunctuation(sign) || char.IsWhiteSpace(sign));

            return sign;
        }

        public void ChangeTurn()
        {
            if (PlayerSign == 'X')
            {
                PlayerSign = 'O';
            }
            else
                PlayerSign = 'X';
        }
    }
}
