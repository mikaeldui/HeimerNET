namespace HeimerNET.Lor.DataDragon
{
    public record Rarity
    {
        /// <summary>
        /// Localized <see cref="NameRef"/>.
        /// </summary>
        public string Name { get; init; } = default!;
        public string NameRef { get; init; } = default!;
    }
}
