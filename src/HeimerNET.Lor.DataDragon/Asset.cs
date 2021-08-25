namespace HeimerNET.Lor.DataDragon
{
    public record Asset
    {
        public string GameAbsolutePath { get; init; } = default!;
        public string FullAbsolutePath { get; init; } = default!;
    }
}
