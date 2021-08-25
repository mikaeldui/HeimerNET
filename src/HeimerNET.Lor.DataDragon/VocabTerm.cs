namespace HeimerNET.Lor.DataDragon
{
    public record VocabTerm
    {
        public string Description { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="NameRef"/>.
        /// </summary>
        public string Name { get; init; } = default!;
        public string NameRef { get; init; } = default!;
    }
}
