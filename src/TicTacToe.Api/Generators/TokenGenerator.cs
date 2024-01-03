using TicTacToe.Api.Configuration;
using TicTacToe.Api.Interfaces;

namespace TicTacToe.Api.Generators;

public class TokenGenerator : ITokenGenerator
{
    private readonly Random _rnd = new();
    private const string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNM";
    
    public string Generate()
    {
        var chars = new char[GameConfiguration.TokenLength];

        for (uint i = 0; i < GameConfiguration.TokenLength; i++)
        {
            chars[i] = Alphabet[_rnd.Next(Alphabet.Length)];
        }

        return new string(chars);
    }
}