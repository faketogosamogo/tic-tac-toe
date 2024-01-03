using FluentValidation;
using TicTacToe.Api.Configuration;
using TicTacToe.Api.ValueObjects;

namespace TicTacToe.Api.Validators;

public class CoordinatesValidator : AbstractValidator<Coordinates>
{
    public CoordinatesValidator()
    {
        RuleFor(c => c.X).Must(c => c < GameConfiguration.LineSize);
        RuleFor(c => c.Y).Must(c => c < GameConfiguration.LineSize);
    }
}