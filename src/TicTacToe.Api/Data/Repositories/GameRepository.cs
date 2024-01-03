using MongoDB.Driver;
using TicTacToe.Api.Entities;
using TicTacToe.Api.Interfaces;

namespace TicTacToe.Api.Data.Repositories;

public class GameRepository : IGameRepository
{
    private readonly IMongoDbContext _context;

    public GameRepository(IMongoDbContext context)
    {
        _context = context;
    }

    public Task Add(Game game, CancellationToken cancellationToken) =>
        _context.Games.InsertOneAsync(game, new InsertOneOptions(), cancellationToken);

    public Task Update(Game game, CancellationToken cancellationToken) =>
        _context.Games.ReplaceOneAsync(Builders<Game>.Filter.Eq(c => c.Id, game.Id), game, cancellationToken: cancellationToken);

    public Task<Game?> GetByToken(string token, CancellationToken cancellationToken) =>
        _context.Games.Find(c => c.Token == token).FirstOrDefaultAsync(cancellationToken);
}