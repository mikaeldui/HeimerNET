namespace HeimerNET.Lor.DeckCodes;

/// <summary>
/// Represend read-only card in deck.
/// </summary>
public interface IReadOnlyDeckCard
{
    /// <summary>
    /// Code of card.
    /// </summary>
    public string CardCode { get; }

    /// <summary>
    /// Count of card.
    /// </summary>
    public int Count { get; }
}
