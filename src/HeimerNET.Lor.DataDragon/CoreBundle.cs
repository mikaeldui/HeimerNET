using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon;

public record CoreBundle
{
    [JsonPropertyName("vocabTerms")]
    public VocabTerm[] VocabTerms { get; init; } = default!;

    [JsonPropertyName("keywords")]
    public Keyword[] Keywords { get; init; } = default!;

    [JsonPropertyName("regions")]
    public Region[] Regions { get; init; } = default!;

    [JsonPropertyName("spellSpeeds")]
    public SpellSpeed[] SpellSpeeds { get; init; } = default!;

    [JsonPropertyName("rarities")]
    public Rarity[] Rarities { get; init; } = default!;

    [JsonPropertyName("sets")]
    public Set[] Sets { get; init; } = default!;
}
