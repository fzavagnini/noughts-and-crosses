namespace noughts_and_crosses.Services
{
    public interface ITicTacToeRandomService
    {
        int GenerateNextPossibleMove(int[] board);
        int GenerateNextBestPossibleMove(int[] board, int numberOfRowsAndColumns, bool isDiagonal = false);
    }
}