using FluentValidation;
using FluentValidation.AspNetCore;
using TicTacToe.Api.Configuration;
using TicTacToe.Api.Data;
using TicTacToe.Api.Data.Repositories;
using TicTacToe.Api.Filters;
using TicTacToe.Api.Generators;
using TicTacToe.Api.Interfaces;
using TicTacToe.Api.Services;
using TicTacToe.Api.Validators;
using TicTacToe.Api.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => 
    opt.Filters.Add(new ExceptionFilter()));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Coordinates>, CoordinatesValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbContextConfiguration>(builder.Configuration.GetSection(nameof(MongoDbContextConfiguration)));

builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddSingleton<IBotTurnGenerator, BotTurnGenerator>();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddCors(c => 
    c.AddPolicy(name: "CorsAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsAll");

app.Run();