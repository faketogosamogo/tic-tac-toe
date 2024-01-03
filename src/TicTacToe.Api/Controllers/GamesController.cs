using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Entities;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Interfaces;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public Task<Game> CreateGame(Sign playerSign, Sign startSign, CancellationToken cancellationToken) =>
        _gameService.CreateGame(playerSign, startSign, cancellationToken);

    [HttpGet("{token}")]
    public Task<Game> GetGame(string token, CancellationToken cancellationToken) =>
        _gameService.GetGame(token, cancellationToken);
    
    [HttpPost("{token}/turn")]
    public Task<Game> MakeTurn(string token, Coordinates coordinates, CancellationToken cancellationToken) =>
        _gameService.MakeTurn(token, coordinates, cancellationToken);
}