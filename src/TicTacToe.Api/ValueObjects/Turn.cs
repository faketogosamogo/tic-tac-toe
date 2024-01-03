using TicTacToe.Api.Enums;

namespace TicTacToe.Api.ValueObjects;

public record Turn(Sign Sign, Coordinates Coordinates);