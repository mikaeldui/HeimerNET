using System.Diagnostics.CodeAnalysis;

namespace HeimerNET.Lor.GameClient;

public record DraftPick
{
    [MemberNotNullWhen(true, nameof(SwappedIn), nameof(SwappedOut))]
    [MemberNotNullWhen(false, nameof(Picks))]
    public bool IsSwap { get; init; }

    public string[]? Picks { get; init; }

    public string[]? SwappedIn { get; init; }

    public string[]? SwappedOut { get; init; }
}
