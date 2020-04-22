using System;
using System.Collections.Generic;
using System.Linq;

namespace noughts_and_crosses.Services
{
    public class TicTacToeServiceBase
    {
        protected internal List<int> GetRowSlice(int[,] doubleArr, int row)
        {
            List<int> slice = new List<int>();

            for (int i = row; i < row + 1; i++)
            {
                for (int j = 0; j < doubleArr.GetLength(1); j++)
                {
                    slice.Add(doubleArr[i, j]);
                }
            }

            return slice;
        }

        protected internal List<int> GetColumnSlice(int[,] doubleArr, int column)
        {
            List<int> slice = new List<int>();

            for (int i = 0; i < doubleArr.GetLength(0); i++)
            {
                for (int j = column; j < column + 1; j++)
                {
                    slice.Add(doubleArr[i, j]);
                }
            }

            return slice;
        }
        protected internal List<List<int>> GetDiagonalSlices(List<List<int>> rows, int numberOfRowsAndColumns)
        {
            List<List<int>> slice = new List<List<int>>();
            List<int> firstDiagonal = new List<int>();
            List<int> secondDiagonal = new List<int>();

            var counter = 1;

            foreach (var diagonal in rows)
            {
                for (int i = 0; i < 1; i++)
                {
                    firstDiagonal.Add(diagonal[counter - 1]);
                }

                counter++;
            }

            slice.Add(firstDiagonal);

            counter = numberOfRowsAndColumns;

            foreach (var diagonal in rows)
            {
                for (int i = 0; i < 1; i++)
                {
                    secondDiagonal.Add(diagonal[counter - 1]);
                }

                counter--;
            }

            slice.Add(secondDiagonal);

            return slice;
        }

        
        public void PrintCurrentTicTacToeBoard(int[] board, int numberOfRowsAndColumns)
        {

            for (int i = 0; i < board.Length; i++)
            {
                if (i % numberOfRowsAndColumns == 0)
                {
                    Console.WriteLine();
                }

                //ASCII 79 ==> 'O', ASCII 88 ==> 'X'
                var value = board[i] == i ? "-" : board[i] == 79 ? "O" : board[i] == 88 ? "X" : "-";

                Console.Write(value + " ");
            }
        }

        public bool CheckAllTicTacToePositionsChecked(int[] board)
        {
            bool[] allPositionsChecked = new bool[board.Length];

            for (int i = 0; i < board.Length; i++)
            {
                allPositionsChecked[i] = board[i] != i && (board[i] == 79 || (board[i] == 88));
            }

            return allPositionsChecked.All(x => x);
        }

        public bool CheckTicTacToeValidInput(int input, int numberOfRowsAndColumns)
        {
            return input > numberOfRowsAndColumns * numberOfRowsAndColumns | input < 1;
        }

        public void PrintTicTacToeInvalidInputMessage(int[] board, int numberOfRowsAndColumns)
        {
            Console.WriteLine();
            Console.WriteLine("Please enter a valid choice");
            Console.WriteLine("The following inputs are valid: ");
            Console.WriteLine();

            for (int i = 0; i < numberOfRowsAndColumns * numberOfRowsAndColumns; i++)
            {
                Console.Write(i + 1 + " ");
            }

            Console.WriteLine();
            PrintCurrentTicTacToeBoard(board, numberOfRowsAndColumns);
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}