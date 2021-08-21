namespace HeimerNET.Lor.DeckCodes
{
    /// <summary>
    /// Represend card in deck.
    /// </summary>
    public interface IDeckCard
    {
        /// <summary>
        /// Code of card.
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// Count of card.
        /// </summary>
        public int Count { get; set; }
    }
}
