namespace noughts_and_crosses.Services
{
    public interface ITicTacToeRandomService
    {
        int GenerateNextPossibleMove(int[] board);
    }
}