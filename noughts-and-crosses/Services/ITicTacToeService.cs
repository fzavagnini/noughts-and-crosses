using System;
using System.Collections.Generic;

namespace noughts_and_crosses.Services
{
    public interface ITicTacToeService
    {
        int[] GetTicTacToeBoard(int numberOfRowsAndColumns);
        int CheckTicTacToeBoardState(int[] board, int numberOfRowsAndColumns);
        bool CheckAllTicTacToePositionsChecked(int[] board);
        void PrintCurrentTicTacToeBoard(int[] board, int numberOfRowsAndColumns);
        bool CheckTicTacToeValidInput(int input, int numberOfRowsAndColumns);
        void PrintTicTacToeInvalidInputMessage(int[] board, int numberOfRowsAndColumns);
        List<int> CheckTicTacToeWinningLine(Tuple<List<List<int>>, List<List<int>>, List<List<int>>> allBoardPossibilities);
        Tuple<List<List<int>>, List<List<int>>, List<List<int>>> ProcessTicTacToeLines(int[,] ticTacToeProperBoard, int[] ticTacToeBoard, int numberOfRowsAndColumns);
    }
}