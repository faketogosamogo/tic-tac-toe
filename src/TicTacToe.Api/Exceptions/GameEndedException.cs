namespace TicTacToe.Api.Exceptions;

public class GameEndedException : ApplicationException
{
    public GameEndedException() : base("Game ended")
    {
        
    }
}