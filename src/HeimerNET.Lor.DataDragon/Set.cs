namespace HeimerNET.Lor.DataDragon
{
    public record Set
    {
        public string IconAbsolutePath { get; init; } = default!;

        /// <summary>
        /// Localized <see cref="NameRef"/>.
        /// </summary>
        public string Name { get; init; } = default!;
        public string NameRef { get; init; } = default!;
    }
}
