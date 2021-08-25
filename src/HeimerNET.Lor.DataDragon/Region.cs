namespace HeimerNET.Lor.DataDragon
{
    public record Region
    {
        public string Abbreviation { get; init; } = default!;
        public string IconAbsolutePath { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="NameRef"/>.
        /// </summary>
        public string Name { get; init; } = default!;
        public string NameRef { get; init; } = default!;
    }
}
