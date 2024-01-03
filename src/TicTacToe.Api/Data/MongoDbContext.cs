using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TicTacToe.Api.Configuration;
using TicTacToe.Api.Entities;

namespace TicTacToe.Api.Data;

public class MongoDbContext : IMongoDbContext
{
    public MongoDbContext(IOptions<MongoDbContextConfiguration> configurationOptions)
    {
        BsonClassMap.RegisterClassMap<Game>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));;
            cm.MapField("_turns").SetElementName("Turns");
        });
        
        var configuration = configurationOptions.Value;
        
        var client = new MongoClient(configuration.ConnectionString);

        var database = client.GetDatabase(configuration.DatabaseName);

        Games = database.GetCollection<Game>(configuration.GamesCollectionName);
        
        var gamesTokenIndex = Builders<Game>.IndexKeys.Ascending(c => c.Token);
        var gamesTokenIndexModel = new CreateIndexModel<Game>(gamesTokenIndex, new CreateIndexOptions { Unique = true });
        
        Games.Indexes.CreateOne(gamesTokenIndexModel);
    }

    public IMongoCollection<Game> Games { get; }
}