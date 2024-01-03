using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Interfaces;

public interface IBotTurnGenerator
{
    Coordinates GenerateFirstTurn();
    
    Coordinates GenerateTurn(IReadOnlyCollection<Coordinates> turns);
}