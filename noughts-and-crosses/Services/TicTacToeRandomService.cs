using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public Tuple<int, bool> CheckTicTacToeWinningCondition(int[] board, List<int> moves, int numberOfRowsAndColumns)
        {
            
            foreach (var move in moves.Where(x=> x!= 0))
            {
                var valuePlaced = board[move - 1];
                board[move - 1] = move;

                var winCondition = _ticTacToeService.CheckTicTacToeBoardState(board, numberOfRowsAndColumns);
                
                if (winCondition == 1)
                {
                    return new Tuple<int, bool>(move, true);
                }

                board[move - 1] = valuePlaced;
            }

            return null;
        }

        public int GenerateNextBestPossibleMove(int[] board, int numberOfRowsAndColumns)
        {
            int[,] ticTacProperBoard = new int[numberOfRowsAndColumns, numberOfRowsAndColumns];
            List<int> bestPossibleMoves = new List<int>();
            Tuple<int, bool> winningCondition = null;
            int bestRandomMove = 0;
            int bestPossibleMoveRow = 0;
            int bestPossibleMoveColumn = 0;
            int bestPossibleMoveDiagonal = 0;

            var ticTacToeBoard = _ticTacToeService.ProcessTicTacToeLines(ticTacProperBoard, board, numberOfRowsAndColumns);

            //Rows
            bestPossibleMoveRow = GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item1, numberOfRowsAndColumns);
            //Columns
            bestPossibleMoveColumn = GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item2, numberOfRowsAndColumns);
            //Diagonals
            bestPossibleMoveDiagonal = GetBestPossibleMoveFromRowColumnDiagonal(ticTacToeBoard.Item3, numberOfRowsAndColumns);

            if (bestPossibleMoveRow == 0 && bestPossibleMoveColumn == 0 && bestPossibleMoveDiagonal == 0)
            {
                bestRandomMove = GenerateNextPossibleMove(board);
                return bestRandomMove;
            }

            bestPossibleMoves.Add(bestPossibleMoveRow);
            bestPossibleMoves.Add(bestPossibleMoveColumn);
            bestPossibleMoves.Add(bestPossibleMoveDiagonal);

            if (bestPossibleMoves.Count(x => x != 0) > 1)
            {
                winningCondition = CheckTicTacToeWinningCondition(board, bestPossibleMoves, numberOfRowsAndColumns);
            }

            return winningCondition == null 
                ? bestPossibleMoves.FirstOrDefault(x => x != 0) 
                : bestPossibleMoves.FirstOrDefault(x=> x == winningCondition?.Item1);
        }

        private int GetBestPossibleMoveFromRowColumnDiagonal(List<List<int>> listOfList, int numberOfRowsAndColumns, int player = 0)
        {
            int bestPossibleMove = 0;
            List<List<int>> bestPossibleMoves = new List<List<int>>();
            for (int i = 0; i < listOfList.Count; i++)
            {
                var duplicates = listOfList[i].GroupBy(x => x)
                    .Where(g => g.Count() > 1 && g.Count() < numberOfRowsAndColumns)
                    .Select(y => y.Key)
                    .ToList();

                if (duplicates.Count == 0)
                {
                    continue;
                }

                if (duplicates.Count > 0)
                {
                    bestPossibleMoves.Add(listOfList[i]);
                }
                
                var nextPossibleMoveList = listOfList[i];
                
                bestPossibleMove = nextPossibleMoveList.FirstOrDefault(x => x != 88 && x != 79);
            }

            if (bestPossibleMove != 0 && bestPossibleMoves.Count == 1)
            {
                return bestPossibleMove;
            }

            if (bestPossibleMoves.Count > 1 && bestPossibleMoves.Find(x => x.Contains(88)).Count == 2)
            {
                bestPossibleMove = bestPossibleMoves.Find(x => x.Contains(79)).FirstOrDefault(x => x != 88 && x != 79);
            }

            return bestPossibleMove;
        }
    }
}