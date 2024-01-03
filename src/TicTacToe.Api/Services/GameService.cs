using TicTacToe.Api.Entities;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Exceptions;
using TicTacToe.Api.Interfaces;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Services;

public class GameService : IGameService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IGameRepository _gameRepository;
    private readonly IBotTurnGenerator _botTurnGenerator;
    
    public GameService(ITokenGenerator tokenGenerator, IGameRepository gameRepository, IBotTurnGenerator botTurnGenerator)
    {
        _tokenGenerator = tokenGenerator;
        _gameRepository = gameRepository;
        _botTurnGenerator = botTurnGenerator;
    }

    public async Task<Game> CreateGame(Sign playerSign, Sign startSign, CancellationToken cancellationToken)
    {
        var token = _tokenGenerator.Generate();
        var game = new Game(token, playerSign, startSign);
        
        if (playerSign != startSign)
        {
            var botTurn = _botTurnGenerator.GenerateFirstTurn();
            game.MakeTurn(botTurn);
        }
        
        await _gameRepository.Add(game, cancellationToken);

        return game;
    }

    public async Task<Game> GetGame(string token, CancellationToken cancellationToken)
    { 
        var game = await _gameRepository.GetByToken(token, cancellationToken);

        if (game == null)
        {
            throw new GameNotFoundException();
        }

        return game;
    }
    
    public async Task<Game> MakeTurn(string token, Coordinates coordinates, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByToken(token, cancellationToken);

        if (game == null)
        {
            throw new GameNotFoundException();
        }
        
        game.MakeTurn(coordinates);

        if (game.State == GameState.InProgress)
        {
            var botTurn = _botTurnGenerator.GenerateTurn(game.Turns.Select(c => c.Coordinates).ToArray());
            
            game.MakeTurn(botTurn);
        }

        await _gameRepository.Update(game, cancellationToken);
        
        return game;
    }
}