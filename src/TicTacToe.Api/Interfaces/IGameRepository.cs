using TicTacToe.Api.Entities;

namespace TicTacToe.Api.Interfaces;

public interface IGameRepository
{
    Task Add(Game game, CancellationToken cancellationToken);

    Task Update(Game game, CancellationToken cancellationToken);

    Task<Game?> GetByToken(string token, CancellationToken cancellationToken);
}