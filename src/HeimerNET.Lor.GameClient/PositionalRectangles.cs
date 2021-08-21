namespace HeimerNET.Lor.GameClient;

public record PositionalRectangles
{
    /// <summary>
    /// Local player name
    /// </summary>
    public string? PlayerName { get; init; }

    /// <summary>
    /// Opponent name.
    /// </summary>
    public string? OpponentName { get; init; }

    public GameState? GameState { get; init; }

    /// <summary>
    /// Screen resolution in game.
    /// </summary>
    public Screen Screen { get; init; } = default!;

    public Rectangles[] Rectangles { get; init; } = default!;
}
