namespace TicTacToe.Api.Exceptions;

public class CoordinatesDuplicateException : ApplicationException
{
    public CoordinatesDuplicateException() : base("Coordinates duplicate")
    {
        
    }
}