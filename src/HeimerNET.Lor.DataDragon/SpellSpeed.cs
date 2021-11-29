using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon;

public record SpellSpeed
{
    /// <summary>
    /// Localized <see cref="NameRef"/>.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("nameRef")]
    public string NameRef { get; init; } = default!;
}
