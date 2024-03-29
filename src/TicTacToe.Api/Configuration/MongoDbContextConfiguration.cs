namespace TicTacToe.Api.Configuration;

public class MongoDbContextConfiguration
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string GamesCollectionName { get; set; } = null!;
}