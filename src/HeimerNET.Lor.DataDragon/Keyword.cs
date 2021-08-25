namespace HeimerNET.Lor.DataDragon
{
    public record Keyword
    {
        public string Description { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="NameRef"/>.
        /// </summary>
        public string Name { get; init; } = default!;
        public string NameRef { get; init; } = default!;
    }
}
