using TicTacToe.Api.Configuration;

namespace TicTacToe.Api.ValueObjects;

public record Coordinates
{
    public uint X { get; }
    
    public uint Y { get; }

    public Coordinates(uint x, uint y)
    {
        if (X > GameConfiguration.LineSize - 1)
        {
            throw new ArgumentException($"X should be less {GameConfiguration.LineSize}");
        }

        if (Y > GameConfiguration.LineSize - 1)
        {
            throw new ArgumentException($"Y should be less {GameConfiguration.LineSize}");
        }

        X = x;
        Y = y;
    }
}