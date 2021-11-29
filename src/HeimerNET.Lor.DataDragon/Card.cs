using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon;

public record Card
{
    //public [] AssociatedCards { get; init; } = default!;

    [JsonPropertyName("associatedCardRefs")]
    public string[] AssociatedCardRefs { get; init; } = default!;

    [JsonPropertyName("associatedCardRefs")]
    public Asset[] Assets { get; init; } = default!;

    /// <summary>
    /// Localized <see cref="RegionRefs"/>.
    /// </summary>
    [JsonPropertyName("regions")]
    public string[] Regions { get; init; } = default!;

    [JsonPropertyName("regionRefs")]
    public string[] RegionRefs { get; init; } = default!;

    [JsonPropertyName("attack")]
    public int Attack { get; init; } = default!;

    [JsonPropertyName("cost")]
    public int Cost { get; init; } = default!;

    [JsonPropertyName("health")]
    public int Health { get; init; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; init; } = default!;

    [JsonPropertyName("descriptionRaw")]
    public string DescriptionRaw { get; init; } = default!;

    [JsonPropertyName("levelupDescription")]
    public string LevelupDescription { get; init; } = default!;

    [JsonPropertyName("levelupDescriptionRaw")]
    public string LevelupDescriptionRaw { get; init; } = default!;

    [JsonPropertyName("flavorText")]
    public string FlavorText { get; init; } = default!;

    [JsonPropertyName("artistName")]
    public string ArtistName { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("cardCode")]
    public string CardCode { get; init; } = default!;


    /// <summary>
    /// Localized <see cref="KeywordRefs"/>.
    /// </summary>
    [JsonPropertyName("keywords")]
    public string[] Keywords { get; init; } = default!;

    [JsonPropertyName("keywordRefs")]
    public string[] KeywordRefs { get; init; } = default!;


    /// <summary>
    /// Localized <see cref="SpellSpeedRef"/>.
    /// </summary>
    [JsonPropertyName("spellSpeed")]
    public string SpellSpeed { get; init; } = default!;

    [JsonPropertyName("spellSpeedRef")]
    public string SpellSpeedRef { get; init; } = default!;


    /// <summary>
    /// Localized <see cref="RarityRef"/>.
    /// </summary>
    [JsonPropertyName("rarity")]
    public string Rarity { get; init; } = default!;

    [JsonPropertyName("rarityRef")]
    public string RarityRef { get; init; } = default!;

    [JsonPropertyName("subtypes")]
    public string[] Subtypes { get; init; } = default!;

    [JsonPropertyName("supertype")]
    public string Supertype { get; init; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; init; } = default!;

    [JsonPropertyName("collectible")]
    public bool IsCollectible { get; init; } = default!;

    [JsonPropertyName("set")]
    public string Set { get; init; } = default!;
}
