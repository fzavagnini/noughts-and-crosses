using System;
using System.Collections.Generic;
using System.Linq;
using noughts_and_crosses.Helpers;

namespace noughts_and_crosses.Services
{
    public class TicTacToeService : TicTacToeServiceBase, ITicTacToeService
    {
        public int[] GetTicTacToeBoard(int numberOfRowsAndColumns)
        {
            int[] board = new int[numberOfRowsAndColumns * numberOfRowsAndColumns];

            for (int i = 0; i < board.Length; ++i)
            {
                board[i] = i + 1;
            }

            return board;
        }

        public Tuple<List<int>, Tuple<int, string>>  CheckTicTacToeWinningLine(Tuple<List<List<int>>, List<List<int>>, List<List<int>>> allBoardPossibilities)
        {
            List<int> winningLine = new List<int>();

            //var ticTacToeLines = TicTacToeHelper.ZipThree(allBoardPossibilities.Item1, allBoardPossibilities.Item2,
            //    allBoardPossibilities.Item3,
            //    (rows, columns, diagonals) => new { rows, columns, diagonals });

            //This builds a data structure that could contain all possible lines - TODO Requires more testing
            // ----------------------------------------------
            // 1)   1 2 3                2)   - 2 3
            //      4 5 -      <====>         4 5 6
            //      7 - 9                     7 8 -
            // ----------------------------------------------
            
            var (rows, columns, diagonals) = allBoardPossibilities;
            Tuple<int, string> winningPosition = new Tuple<int, string>(0, "");
            int count = 1;

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i].Distinct().Count() == 1)
                {
                    winningLine = rows[i];
                    winningPosition = new Tuple<int, string>(i + 1, "Row");
                }
            }

            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Distinct().Count() == 1)
                {
                    winningLine = columns[i];
                    winningPosition = new Tuple<int, string>(i + 1, "Column");
                }
            }

            for (int i = 0; i < diagonals.Count; i++)
            {
                if (diagonals[i].Distinct().Count() == 1)
                {
                    winningLine = diagonals[i];
                    winningPosition = new Tuple<int, string>(i + 1, "Diagonal");
                }
            }

            return new Tuple<List<int>, Tuple<int, string>>(winningLine, winningPosition);
        }

        public Tuple<List<List<int>>, List<List<int>>, List<List<int>>> ProcessTicTacToeLines(int[,] ticTacToeProperBoard, int[] ticTacToeBoard,
            int numberOfRowsAndColumns)
        {
            List<List<int>> rows = new List<List<int>>();
            List<List<int>> columns = new List<List<int>>();
            List<List<int>> diagonals = new List<List<int>>();
            
            var counter = 1;

            for (int i = 0; i < numberOfRowsAndColumns; i++)
            {
                for (int j = 0; j < numberOfRowsAndColumns; j++)
                {
                    ticTacToeProperBoard[i, j] = ticTacToeBoard[counter - 1];
                    counter++;
                }
            }

            for (int i = 0; i < ticTacToeProperBoard.GetLength(0); i++)
            {
                var row = GetRowSlice(ticTacToeProperBoard, i);
                rows.Add(row);
            }

            for (int j = 0; j < ticTacToeProperBoard.GetLength(0); j++)
            {
                var column = GetColumnSlice(ticTacToeProperBoard, j);
                columns.Add(column);
            }

            diagonals = GetDiagonalSlices(rows, numberOfRowsAndColumns);

            return new Tuple<List<List<int>>, List<List<int>>, List<List<int>>>(rows, columns, diagonals);

        }

        public int CheckTicTacToeBoardState(int[] board, int numberOfRowsAndColumns)
        {
            int[,] ticTacProperBoard = new int[numberOfRowsAndColumns, numberOfRowsAndColumns];

            var ticTacToeBoard = ProcessTicTacToeLines(ticTacProperBoard, board, numberOfRowsAndColumns);
            
            if (ticTacToeBoard.Item1.Any(x=>x.Distinct().Count() == 1) ||
                ticTacToeBoard.Item2.Any(x => x.Distinct().Count() == 1) ||
                ticTacToeBoard.Item3.Any(x => x.Distinct().Count() == 1))
            {
                return 1;
            }

            return 0;
        }
    }
}