using TicTacToe.Api.Configuration;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Exceptions;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Entities;

public class Game
{
    public Game(string token, Sign playerSign, Sign startSign)
    {
        Token = token;
        PlayerSign = playerSign;
        StartSign = startSign;
    }

    public void MakeTurn(Coordinates coordinates)
    {
        if (State != GameState.InProgress)
        {
            throw new GameEndedException();
        }

        if (Turns.Select(c => c.Coordinates).Contains(coordinates))
        {
            throw new CoordinatesDuplicateException();
        }
        
        var currentPlayerSign = !Turns.Any() ? 
            StartSign : 
            Turns.Last().Sign == Sign.X ?
                Sign.O :
                Sign.X;

        var turn = new Turn(currentPlayerSign, coordinates);
        
        _turns.Add(turn);

        if (Turns.Count >= GameConfiguration.MinTurnsCountForEnding)
        {
            var currentPlayerCoordinatesList = Turns
                .Where(c => c.Sign == currentPlayerSign)
                .Select(c => c.Coordinates)
                .ToArray();

            var isWon = IsWonByX(currentPlayerCoordinatesList) || 
                        IsWonByY(currentPlayerCoordinatesList) ||
                        IsWonOnLeftDiagonal(currentPlayerCoordinatesList) || 
                        IsWonOnRightDiagonal(currentPlayerCoordinatesList);

            if (isWon)
            {
                State = currentPlayerSign == Sign.O ? GameState.OWon : GameState.XWon;
            }
            else if (Turns.Count == GameConfiguration.MapSize)
            {
                State = GameState.Draw;
            }
        }
    }

    public string Id { get; private set; }
    
    public string Token { get; private set; }
    
    public Sign PlayerSign { get; private set; }
    
    public Sign StartSign { get; private set; }

    private List<Turn> _turns = new();
    
    public IReadOnlyCollection<Turn> Turns => _turns.AsReadOnly();
    
    public GameState State { get; private set; }

    private bool IsWonByX(IReadOnlyCollection<Coordinates> currentPlayerCoordinatesList) =>
        currentPlayerCoordinatesList.GroupBy(c => c.X).Any(c => c.Count() == GameConfiguration.LineSize);

    private bool IsWonByY(IReadOnlyCollection<Coordinates> currentPlayerCoordinatesList) =>
        currentPlayerCoordinatesList.GroupBy(c => c.Y).Any(c => c.Count() == GameConfiguration.LineSize);

    private bool IsWonOnLeftDiagonal(IReadOnlyCollection<Coordinates> currentPlayerCoordinatesList) =>
        currentPlayerCoordinatesList.Count(c => c.X == c.Y) == GameConfiguration.LineSize;
    
    private bool IsWonOnRightDiagonal(IReadOnlyCollection<Coordinates> currentPlayerCoordinatesList) =>
        currentPlayerCoordinatesList.Count(c => c.X + c.Y == GameConfiguration.LineSize -1) == GameConfiguration.LineSize;
}