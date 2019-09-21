using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
namespace ExtensionMethods
{
    /// <summary>
    /// Represents extension methods for Console.Clear method
    /// </summary>
    public static class ClearExtension
    {
        /// <summary>
        /// Clears range of lines in console.
        /// </summary>
        public static void Clear(int startingLine, int endingLine, int cursorPosX)
        {
            for (int possY = startingLine-1; possY >= endingLine; possY--)
            {
                Console.SetCursorPosition(cursorPosX, possY);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            }
        }
    }
}

namespace TicTacToe
{
    class TicTacToeGame
    {
        #region class members

        public char[,] boardGame = new char[3, 3];
        public bool diagonalWinner;
        public bool gameOver = false;
        public bool gameRunning = true;
        public Player player = new Player();
        public bool vsCPU;
        #endregion

        #region class constructor
        public TicTacToeGame()
        {
            
        }
        #endregion

        public void StartTicTacToe()
        {
            DisplayTitle();
            while (gameRunning)
            {
                gameOver = false;
                GameMenu();
            }

        }

        /*blic void testData()
        {
            boardGame = new char[3,3]{ { 'X', 'O', ' ' }, { 'X', ' ', 'O' }, { 'X', ' ', ' ' } };   
        }*/

        public void DisplayScore()
        {
            Console.WriteLine("\r\nPlayer X has {0} points so far.", player.Xpoints);
            Console.WriteLine("Player O has {0} points so far.", player.Opoints);
            Console.ReadKey();
            ClearExtension.Clear(Console.CursorTop, 6, 0);


        }

        public void NewGame()
        {
            InitalizeBoard();
            DeclaringFirstPlayersSigns();
            while (!gameOver)
            {
                DisplayBoard();
                InsertXandO();
                player.ChangeTurn();
                GameOutcome();
            }

        }

        public void DeclaringFirstPlayersSigns()
        {
            player.PlayerSign = char.ToUpper(player.SetPlayerSign());
            if (player.PlayerSign == 'X')
            {
                Console.Write("Player 2 is with O sign. \n\r");
            }
            else if (player.PlayerSign == 'O')
            {
                Console.Write("Player 2 is with X sign. \n\r");
            }
            player.NumberOfMoves = 0;
        }


        public void GameMenu()
        {
            Console.WriteLine("1. Start New Game \n\r" +
                "2. Display Score \n\r" +
                "3. Exit");
            Console.Write("Choose option: ");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    NewGame();
                    break;

                case "2":
                    DisplayScore();
                    break;

                case "3":
                    gameRunning = false;
                    break;

                default:
                    break;
            }
        }
        public void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------- Console Tic-Tac-Toe Game. ---------------");
            Console.WriteLine();
            Console.WriteLine("                             *                         ");
            Console.WriteLine("                             *                         ");
            Console.WriteLine("                             *                         ");
            Console.Write("\r\n\r\n");
            Console.ResetColor();
        }
        public bool IsValidInput(int s)
        {
            if (s == 0 || s == 1 || s == 2)
            {
                return true;
            }
            Console.Write("Only numbers that represent rows and columns can be accepted. ");
            Console.WriteLine();
            return false;
        }
        public void InsertXandO()
        {
            Console.Write("Player {0} turn! \r\n", player.PlayerSign);
            int rowVal = 0;
            int colVal = 0;
            do
            {
                Console.Write("Insert row: ");
                rowVal = Convert.ToInt32(Console.ReadLine());
                rowVal--;
                Console.Write("Insert column: ");
                colVal = Convert.ToInt32(Console.ReadLine());
                colVal--;
            } while (!IsValidInput(rowVal) || !IsValidInput(colVal) || !SpotFree(rowVal, colVal));
            player.NumberOfMoves++;
            boardGame[rowVal, colVal] = player.PlayerSign;
            ClearExtension.Clear(startingLine: Console.CursorTop, endingLine: 6, cursorPosX: 0);
            DisplayBoard();
            //Console.Write("Press <enter> to continue. \r\n");
            //Console.ReadKey();
            ClearExtension.Clear(startingLine: Console.CursorTop, endingLine: 6, cursorPosX: 0);
        }

        public bool SpotFree(int rowValue, int colValue)
        {
            if (boardGame[rowValue, colValue] != ' ')
             {
                  Console.Write("That spot is already taken \n\r");
                  return false;
             }
            return true;
        }

        public void InitalizeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    boardGame[row, col] = ' ';
                }
            }
        }

        public void DisplayBoard()
        {
            int rowPos = 1;
            Console.WriteLine("\n\r     1.  2.  3.");
            Console.WriteLine("   #---#---#---#");
            for (int row = 0; row < boardGame.GetLength(0); row++)
            {
                Console.Write("{0}. |", rowPos);
                for (int col = 0; col < boardGame.GetLength(1); col++)
                {
                    Console.Write(" {0} |", boardGame[row,col]);
                }
                rowPos++;
                Console.WriteLine();
                Console.WriteLine("   #---#---#---#");
  
            }
            //Console.Write("Player {0} turn! \r\n", player.PlayerSign);
        }

        public void GameWonMessage(ref int player, char plSign)
        {
            player++;
            DisplayBoard();
            Console.ForegroundColor = ConsoleColor.Green;
            if (plSign != 'X') //Right after each players move, sign is turned to opposite one. Thats why it is used != 'X'
            {
                Console.Write("\n\rPlayer X wins! Player X points: {0}\r\n", player);
            }
            else if (plSign != 'O')
            {
                Console.Write("\n\rPlayer O wins! Player O points: {0}\r\n", player);
            }
            gameOver = true;
            Console.ResetColor();
            Console.ReadKey();
            ClearExtension.Clear(startingLine: Console.CursorTop, endingLine: 6, cursorPosX: 0);
        }

        /*******************************************************/

        //Is it Win, Defeat or Draw
        public void GameOutcome()
        {
            for (int row = 0; row < 3; row++)
            {
                //Checks rows
                if (boardGame[row, 0] == 'X' && boardGame[row, 1] == 'X' && boardGame[row, 2] == 'X')
                {
                    GameWonMessage(ref player.Xpoints, player.PlayerSign);
                }
                else if (boardGame[row, 0] == 'O' && boardGame[row, 1] == 'O' && boardGame[row, 2] == 'O')
                {
                    GameWonMessage(ref player.Opoints, player.PlayerSign);
                }

                //checks columns
                for (int col = 0; col < 3; col++)
                {
                    if (boardGame[0, col] == 'X' && boardGame[1, col] == 'X' && boardGame[2, col] == 'X')
                    {
                        GameWonMessage(ref player.Xpoints, player.PlayerSign);
                    }

                    else if (boardGame[0, col] == 'O' && boardGame[1, col] == 'O' && boardGame[2, col] == 'O')
                    {
                        GameWonMessage(ref player.Opoints, player.PlayerSign);
                    }
                }

                //checks diag lef-right
                string topLeftToBottomRight = string.Concat(boardGame[0, 0], boardGame[1, 1], boardGame[2, 2]);
                if (topLeftToBottomRight == "XXX")
                {
                    diagonalWinner = true;
                    GameWonMessage(ref player.Xpoints, player.PlayerSign);
                }
                else if (topLeftToBottomRight == "OOO")
                {
                    diagonalWinner = true;
                    GameWonMessage(ref player.Opoints, player.PlayerSign);
                }

                //check dig right-left
                string topRightToBottomLeft = string.Concat(boardGame[0, 2], boardGame[1, 1], boardGame[2, 0]);
                if (topRightToBottomLeft == "XXX")
                {
                    diagonalWinner = true;
                    GameWonMessage(ref player.Xpoints, player.PlayerSign);
                }
                else if (topRightToBottomLeft == "OOO")
                {
                    diagonalWinner = true;
                    GameWonMessage(ref player.Opoints, player.PlayerSign);
                }

                //check draw
                if (player.NumberOfMoves == 9 && !diagonalWinner)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    gameOver = true;
                    vsCPU = false;
                    DisplayBoard();
                    Console.Write("Game is draw!");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                    ClearExtension.Clear(startingLine: Console.CursorTop, endingLine: 6, cursorPosX: 0);
                }
            }
        }
    }
}
