using MongoDB.Driver;
using TicTacToe.Api.Entities;

namespace TicTacToe.Api.Data;

public interface IMongoDbContext
{
    IMongoCollection<Game> Games { get; }
}