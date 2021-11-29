using System.Text.Json.Serialization;

namespace HeimerNET.Lor.GameClient;

/// <summary>
/// Game screen resolution
/// </summary>
public record Screen
{
    /// <summary>
    /// Screen width
    /// </summary>
    [JsonPropertyName("ScreenWidth")]
    public int Width { get; init; }

    /// <summary>
    /// Screen height
    /// </summary>
    [JsonPropertyName("ScreenHeight")]
    public int Height { get; init; }
}
