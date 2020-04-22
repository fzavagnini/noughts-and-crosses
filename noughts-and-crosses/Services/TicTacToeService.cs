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

        public List<int> CheckTicTacToeWinningLine(Tuple<List<List<int>>, List<List<int>>, List<List<int>>> allBoardPossibilities)
        {
            List<int> winningLine = new List<int>();

            var ticTacToeLines = allBoardPossibilities.Item1.ZipThree(allBoardPossibilities.Item2,
                allBoardPossibilities.Item3,
                (rows, columns, diagonals) => new { rows, columns, diagonals }).ToList();

            //There can only exist one winning line
            foreach (var ticTacToeLine in ticTacToeLines)
            {
                if (ticTacToeLine.rows.Distinct().Count() == 1)
                {
                    winningLine = ticTacToeLine.rows;
                }
                if (ticTacToeLine.columns.Distinct().Count() == 1)
                {
                    winningLine = ticTacToeLine.columns;
                }
                if (ticTacToeLine.diagonals.Distinct().Count() == 1)
                {
                    winningLine = ticTacToeLine.diagonals;
                }
                
            }

            return winningLine;
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