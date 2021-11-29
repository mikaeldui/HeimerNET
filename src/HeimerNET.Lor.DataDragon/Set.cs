using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon;

public record Set
{
    [JsonPropertyName("iconAbsolutePath")]
    public string IconAbsolutePath { get; init; } = default!;

    /// <summary>
    /// Localized <see cref="NameRef"/>.
    /// </summary>
    [JsonPropertyName("name")]

    public string Name { get; init; } = default!;
    [JsonPropertyName("nameRef")]
    public string NameRef { get; init; } = default!;
}
