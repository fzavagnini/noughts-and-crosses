using System;
using System.Collections.Generic;
using System.Linq;

namespace noughts_and_crosses.Services
{
    public class TicTacToeRandomService : ITicTacToeRandomService
    {
        private readonly ITicTacToeService _ticTacToeService;
        static Random _random = new Random();
        private static int _nextMove;

        public TicTacToeRandomService(ITicTacToeService ticTacToeService)
        {
            _ticTacToeService = ticTacToeService;
        }

        public int GenerateNextPossibleMove(int[] board)
        {
           
            bool[] allPositionsChecked = new bool[board.Length];

            for (int i = 0; i < board.Length; i++)
            {
                allPositionsChecked[i] = board[i] != i && (board[i] == 79 || (board[i] == 88));
            }

            var lengthOfPossibleMoves = allPositionsChecked.Count(x => !x);

            if (lengthOfPossibleMoves == 0)
            {
                return 0;
            }

            _nextMove = board[_random.Next(board.Length)];

            if (lengthOfPossibleMoves != 0 && (_nextMove == 79 || _nextMove == 88))
            {
                do
                {
                    _nextMove = board[_random.Next(board.Length)];
                } while (_nextMove == 79 || _nextMove == 88);

                return _nextMove;

            }

            return _nextMove;
        }

        public int GenerateNextBestPossibleMove(int[] board, int numberOfRowsAndColumns, bool isDiagonal = false)
        {
            int[,] ticTacProperBoard = new int[numberOfRowsAndColumns, numberOfRowsAndColumns];
            int bestPossibleMove = 0;
            
            var ticTacToeBoard = _ticTacToeService.ProcessTicTacToeLines(ticTacProperBoard, board, numberOfRowsAndColumns);

            //Is there any line that contains more than numberOfRowsAndColumns - 1 checked positions
            //If yes get the !position of numberOfRowsAndColumns - 1
            
            //Rows
            bestPossibleMove =
                GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item1, numberOfRowsAndColumns);
            //Columns
            bestPossibleMove = bestPossibleMove == 0 ?
                GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item2, numberOfRowsAndColumns) : bestPossibleMove;
            //Diagonals
            bestPossibleMove = bestPossibleMove == 0 ?
                GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item3, numberOfRowsAndColumns, true) : bestPossibleMove;

            if (bestPossibleMove == 0)
            {
                bestPossibleMove = GenerateNextPossibleMove(board);
            }

            return bestPossibleMove;
        }

        private int GetBestPossibleMoveFromRowColumnDiagonal(List<List<int>> listOfList, int numberOfRowsAndColumns, bool isDiagonal = false)
        {
            int bestPossibleMove = 0;
            for (int i = 0; i < listOfList.Count; i++)
            {
                var duplicates = listOfList[i].GroupBy(x => x)
                    .Where(g => !isDiagonal ? g.Count() > 1 && g.Count() < numberOfRowsAndColumns : g.Count() > 1 && g.Count() < numberOfRowsAndColumns + 1)
                    .Select(y => y.Key)
                    .ToList();

                if (duplicates.Count == 0)
                {
                    continue;
                }

                var nextPossibleMoveList = listOfList[i];
                bestPossibleMove = nextPossibleMoveList.FirstOrDefault(x => x != 88 && x != 79);
            }

            return bestPossibleMove;
        }
    }
}