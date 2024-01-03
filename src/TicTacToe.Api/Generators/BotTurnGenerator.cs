using TicTacToe.Api.Configuration;
using TicTacToe.Api.Interfaces;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Generators;

public class BotTurnGenerator : IBotTurnGenerator
{
    private readonly Random _rnd = new();

    public Coordinates GenerateFirstTurn()
    {
        var x = (uint)_rnd.Next((int)GameConfiguration.LineSize);
        var y = (uint)_rnd.Next((int)GameConfiguration.LineSize);

        return new Coordinates(x, y);
    }

    public Coordinates GenerateTurn(IReadOnlyCollection<Coordinates> turns)
    {
        if (turns.Count >= GameConfiguration.MapSize)
        {
            throw new Exception();
        }

        while (true)
        {
            var x = (uint)_rnd.Next((int)GameConfiguration.LineSize);
            var y = (uint)_rnd.Next((int)GameConfiguration.LineSize);
            
            if (!turns.Any(c => c.X == x && c.Y == y))
            {
                return new Coordinates(x, y);
            }
        }
    }
}