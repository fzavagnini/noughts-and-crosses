using System;
using System.Linq;

namespace noughts_and_crosses.Services
{
    public class TicTacToeRandomService : ITicTacToeRandomService
    {
        static Random _random = new Random();
        private static int _nextMove;

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
    }
}