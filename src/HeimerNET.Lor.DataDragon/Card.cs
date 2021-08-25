using System.Text.Json.Serialization;

namespace HeimerNET.Lor.DataDragon
{
    public record Card
    {
        //public [] AssociatedCards { get; init; } = default!;
        public string[] AssociatedCardRefs { get; init; } = default!;
        public Asset[] Assets { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="Region"/>.
        /// </summary>
        public string Region { get; init; } = default!;
        public string RegionRef { get; init; } = default!;
        public string[] Regions { get; init; } = default!;
        public string[] RegionRefs { get; init; } = default!;
        public int Attack { get; init; } = default!;
        public int Cost { get; init; } = default!;
        public int Health { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string DescriptionRaw { get; init; } = default!;
        public string LevelupDescription { get; init; } = default!;
        public string LevelupDescriptionRaw { get; init; } = default!;
        public string FlavorText { get; init; } = default!;
        public string ArtistName { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string CardCode { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="KeywordRefs"/>.
        /// </summary>
        public string[] Keywords { get; init; } = default!;
        public string[] KeywordRefs { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="SpellSpeedRef"/>.
        /// </summary>
        public string SpellSpeed { get; init; } = default!;
        public string SpellSpeedRef { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="RarityRef"/>.
        /// </summary>
        public string Rarity { get; init; } = default!;
        public string RarityRef { get; init; } = default!;
        public string Subtype { get; init; } = default!;
        public string[] Subtypes { get; init; } = default!;
        public string Supertype { get; init; } = default!;
        public string Type { get; init; } = default!;

        [JsonPropertyName("collectible")]
        public bool IsCollectible { get; init; } = default!;
        public string Set { get; init; } = default!;
    }
}
