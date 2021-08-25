namespace HeimerNET.Lor.DataDragon
{
    public record CoreBundle
    {
        public VocabTerm[] VocabTerms { get; init; } = default!;
        public Keyword[] Keywords { get; init; } = default!;
        public Region[] Regions { get; init; } = default!;
        public SpellSpeed[] SpellSpeeds { get; init; } = default!;
        public Rarity[] Rarities { get; init; } = default!;
        public Set[] Sets { get; init; } = default!;
    }
}
