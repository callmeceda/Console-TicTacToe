﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static char[,] boardGame; //board of a game
        static int player1Points = 0; //number of points that player 1 has collected
        static int player2Points = 0; //number of points that player 2 has collected
        static int moves = 0; //Number of moves made in a game
        static char c; //charachter that holds first selected sign for player 1
        static char playerSign; //first selected sign just uppercased
        static bool haveAdigWinner = false; //if there is a winner by diagonal
        static bool gameEnd = false; //if game has ended

        static string OtherPlayerSign(char sign)
        {
            if (sign == 'X')
            {
                return "Player 2 sign is O";
            }
            return "Player 2 sign is X";
        }

        static void Main(string[] args)
        { 
            DisplayTitle();
            //boardGame = new char[3, 3];

            InitalizeBoard();

            Console.Write("Enter Player 1 sign(must be X or O): ");
            c = Convert.ToChar(Console.ReadLine());
            playerSign = char.ToUpper(c);
            Console.Write(OtherPlayerSign(playerSign) + "\n\r");

            while (!gameEnd)
            {
                DisplayBoard();
                InsertSing();
                //testTada();
                ChangeTurn();
                CheckOutcome();
                //Console.ReadKey();
            }

        }

        static void CheckOutcome()
        {
            //Checks rows
            for (int row = 0; row < 3; row++)
            {
                if (boardGame[row, 0] == 'X' && boardGame[row,1] == 'X' && boardGame[row,2] == 'X')
                {
                    DisplayBoard();
                    gameEnd = true;
                    player1Points++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Player 1 wins! Player 1 points: {0}\r\n", player1Points);
                }
                else if (boardGame[row, 0] == 'O' && boardGame[row, 1] == 'O' && boardGame[row, 2] == 'O')
                {
                    DisplayBoard();
                    gameEnd = true;
                    player2Points++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Player 2 wins! Player 2 points: {0}\r\n", player2Points);
                    Console.ReadKey();
                }

            }

            //checks columns
            for (int col = 0; col < 3; col++)
            {
                if (boardGame[0, col] == 'X' && boardGame[1, col] == 'X' && boardGame[2, col] == 'X')
                {
                    DisplayBoard();
                    gameEnd = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Player 1 wins! \r\n");
                    Console.ReadKey();
                }

                else if (boardGame[0, col] == 'O' && boardGame[1, col] == 'O' && boardGame[2, col] == 'O')
                {
                    DisplayBoard();
                    gameEnd = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Player 2 wins! \r\n");
                    Console.ReadKey();
                }
            }

            //checks diag lef-right
            /*for (int row = 0; row < 3; row++)
            {
                for (int col = row; col < 3; col++)
                {
                    if (boardGame[row, col] == 'X' || boardGame[row, col] == 'O')
                    {
                        if (row == 2 && col == 2)
                        {
                            if (playerSign != 'X')
                            {
                                DisplayBoard();
                                gameEnd = true;
                                player1Points++;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Player 1 wins! Player 1 has: {0}\r\n", player1Points);
                                Console.ReadKey();
                                haveAdigWinner = true;
                            }
                            else if (playerSign != 'O')
                            {
                                DisplayBoard();
                                gameEnd = true;
                                player2Points++;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Player 2 wins! Player 2 has: {0}\r\n", player2Points);
                                Console.ReadKey();
                                haveAdigWinner = true;
                            }
                        }
                        continue;
                    }
                }
            }*/

            //check dig right-left
            /*for (int col = 2; c >= 0; col--)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (boardGame[row,col] == 'X' || boardGame[row,col] == 'O')
                    {
                        if (row == 2 && col == 0)
                        {

                        }
                    }
                }
            }*/

            //check draw
            if (moves == 9 && !haveAdigWinner)
            {
                DisplayBoard();
                Console.Write("Game is draw!");
                gameEnd = true;
                Console.ReadKey();
            }


        }

        static void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------- Console Tic-Tac-Toe Game. ---------------");
            Console.WriteLine();
            Console.WriteLine("                             *                         ");
            Console.WriteLine("                             *                         ");
            Console.WriteLine("                             *                         ");
            Console.Write("\r\n\r\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void ChangeTurn()
        {
            if (playerSign == 'X')
            {
                playerSign = 'O';
            }
            else
                playerSign = 'X';
        }
        static void InsertSing()
        {
            int rowVal = 0;
            int colVal = 0;
            Console.Write("Player {0} turn!\n\r", playerSign);
            do
            {
                Console.Write("Enter a row: ");
                rowVal = Convert.ToInt32(Console.ReadLine());
                rowVal -= 1;
                Console.Write("Enter a column: ");
                colVal = Convert.ToInt32(Console.ReadLine());
                colVal -= 1;

            } while (!SpotFree(rowVal, colVal));
            moves++;
            boardGame[rowVal, colVal] = char.ToUpper(playerSign);
        }
        
        static bool SpotFree(int rowVal, int colVal)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (boardGame[rowVal,colVal] != ' ')
                    {
                        Console.Write("That spot is taken\n\r");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static void DisplayBoard()
        {
            int RowNo = 1;
            Console.WriteLine("\n\r     1.  2.  3.");
            Console.WriteLine("    --- --- ---");

            for (int row = 0; row < 3; row++)
            {
                Console.Write("{0}. |", RowNo);
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(" {0} |", boardGame[row,col]);
                }
                Console.WriteLine();
                RowNo++;
            }
        }

        static void InitalizeBoard()
        {
            boardGame = new char[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    boardGame[row, col] = ' ';
                }
            }
        }
    }
}
