using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using noughts_and_crosses.Services;

namespace noughts_and_crosses
{
    class Program
    {
        private static ITicTacToeService _ticTacToeService;
        private static ITicTacToeRandomService _ticTacToeRandomService;
        private static int player = 1;
        private static int choice;
        
        static void Main(string[] args)
        {
            //Initialize Service Providers (Dependency Injection)
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITicTacToeService, TicTacToeService>()
                .AddSingleton<ITicTacToeRandomService, TicTacToeRandomService>()
                .BuildServiceProvider();

            //Get instance of TicTacToeService to be used throughout (injectable)
            _ticTacToeService = serviceProvider.GetService<ITicTacToeService>();
            _ticTacToeRandomService = serviceProvider.GetService<ITicTacToeRandomService>();

            //Initialize Noughts and Crosses
            InitializeTicTacToe(3);

            //Indicate user the game has finished!
            Console.WriteLine();
            Console.WriteLine("Thank you for using Noughts and Crosses!!");
            Console.WriteLine();
        }

        private static void InitializeTicTacToe(int numberOfRowsAndColumns)
        {
            //Console message printing indicating the initialization of the board
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Initial state of the board:");

            //Get initial state of the tic-tac-toe board
            var board = _ticTacToeService.GetTicTacToeBoard(numberOfRowsAndColumns);

            //Print current tic-tac-toe board
            _ticTacToeService.PrintCurrentTicTacToeBoard(board, numberOfRowsAndColumns);
            
            Console.WriteLine();
            Console.WriteLine();

            //Loop that keeps track of selections by the player 1 and 2, state of the board, and validity of inputs.
            do
            {
                //Check for draw
                if (_ticTacToeService.CheckAllTicTacToePositionsChecked(board))
                {
                    Console.WriteLine("It is a draw!!");
                    Console.WriteLine("--------------------------------------------");
                    break;
                }

                //Let user know which player is currently on turn
                Console.WriteLine("Player 1: X and CPU: O");
                Console.WriteLine("\n");

                Console.WriteLine(player % 2 == 0 ? "CPU Chance" : "Player 1 Chance");
                Console.WriteLine("\n");

                bool isValid;
                
                if (player % 2 == 0)
                {
                    Console.WriteLine("CPU is thinking...");
                    Console.WriteLine();
                    choice = _ticTacToeRandomService.GenerateNextBestPossibleMove(board, numberOfRowsAndColumns);
                    Thread.Sleep(2000);
                    Console.WriteLine($"CPU has chosen {choice}...");
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    isValid = true;
                }

                else
                {
                    isValid = int.TryParse(Console.ReadLine(), out choice);
                }
                
                //Check valid input, if not valid, print error message and indicate user to enter valid input as presented
                if (!isValid | _ticTacToeService.CheckTicTacToeValidInput(choice, numberOfRowsAndColumns))
                {
                    _ticTacToeService.PrintTicTacToeInvalidInputMessage(board, numberOfRowsAndColumns);
                    continue;
                }

                //Check if input has already been entered/checked, if not, enter it, else, indicate user that selection has been entered already.
                if (board[choice - 1] != 'X' && board[choice - 1] != 'O')
                {
                    if (player % 2 == 0)
                    {
                        board[choice - 1] = 'O';
                        player++;
                    }

                    else
                    {
                        board[choice - 1] = 'X';
                        player++;
                    }
                }

                else
                {
                    Console.WriteLine($"Sorry the position {choice} is already marked ");
                    Console.WriteLine("\n");
                }

                //Print current state of the board
                _ticTacToeService.PrintCurrentTicTacToeBoard(board, numberOfRowsAndColumns);

                Console.WriteLine();
                Console.WriteLine();

                //Keep looping until win condition is found or draw condition is found
            } while (_ticTacToeService.CheckTicTacToeBoardState(board, numberOfRowsAndColumns) != 1);

            Console.WriteLine();

            //Win condition found print winner
            if (_ticTacToeService.CheckTicTacToeBoardState(board, numberOfRowsAndColumns) == 1)
            {
                Console.WriteLine(player % 2 == 0 ? "Player 1 Wins!!" : "CPU Wins!!");
                Console.WriteLine();

                _ticTacToeService.PrintCurrentTicTacToeBoard(board, numberOfRowsAndColumns);

                Console.WriteLine();
                Console.WriteLine("--------------------------------------------");

            }

            //Indicate user to play again
            //If player 1 wins, player 2 starts next game and viceversa, if a choice of replay game is entered.
            //If game ends in draw, player to start is the one that did not go last in the last game, if a choice of replay is entered .

            Console.WriteLine();
            Console.WriteLine("Would you like to play again? (Y/N)");
            Console.WriteLine();
            
            var playAgainOption = Console.ReadLine();

            if (playAgainOption?.ToLower() == "y")
            {
                Console.WriteLine();
                InitializeTicTacToe(numberOfRowsAndColumns);
                Console.WriteLine();
            }
        }
    }
}
