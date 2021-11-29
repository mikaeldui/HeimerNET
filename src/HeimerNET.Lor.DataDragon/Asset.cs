using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon;

public record Asset
{
    [JsonPropertyName("gameAbsolutePath")]
    public string GameAbsolutePath { get; init; } = default!;

    [JsonPropertyName("fullAbsolutePath")]
    public string FullAbsolutePath { get; init; } = default!;
}
