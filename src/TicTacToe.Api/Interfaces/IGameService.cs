using TicTacToe.Api.Entities;
using TicTacToe.Api.Enums;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Interfaces;

public interface IGameService
{
    Task<Game> CreateGame(Sign playerSign, Sign startSign, CancellationToken cancellationToken);

    Task<Game> GetGame(string token, CancellationToken cancellationToken);

    Task<Game> MakeTurn(string token, Coordinates coordinates, CancellationToken cancellationToken);
}