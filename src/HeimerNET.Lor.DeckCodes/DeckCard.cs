using System;
using System.Collections.Generic;

namespace HeimerNET.Lor.DeckCodes
{
    /// <summary>
    /// Represend card in deck.
    /// </summary>
    public class DeckCard : IDeckCard, IReadOnlyDeckCard, IEquatable<DeckCard?>
    {
        /// <inheritdoc/>
        public string CardCode { get; set; } = default!;

        /// <inheritdoc/>
        public int Count { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckCard"/> class.
        /// </summary>
        public DeckCard() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckCard"/> class.
        /// </summary>
        /// <param name="cardCode">Card cardCode</param>
        /// <param name="count">Count of card.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="cardCode"/> is <see langword="null"/>.</exception>
        public DeckCard(string cardCode, int count)
        {
            CardCode = cardCode ?? throw new ArgumentNullException(nameof(cardCode));
            Count = count;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as DeckCard);
        }

        /// <inheritdoc/>
        public bool Equals(DeckCard? other)
        {
            return other != null &&
                   CardCode == other.CardCode &&
                   Count == other.Count;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(CardCode, Count);
        }

        /// <summary>
        /// Determines whether two specified deckcards have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="DeckCard"/> to compare, or <see langword="null"/>.</param>
        /// <param name="right">The second <see cref="DeckCard"/> to compare, or <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the value of a is the same as the value of b; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(DeckCard? left, DeckCard? right)
        {
            return EqualityComparer<DeckCard>.Default.Equals(left, right);
        }

        /// <summary>
        /// Determines whether two specified deckcards have different value.
        /// </summary>
        /// <param name="left">The first <see cref="DeckCard"/> to compare, or <see langword="null"/>.</param>
        /// <param name="right">The second <see cref="DeckCard"/> to compare, or <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the value of a is different from the value of b; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(DeckCard? left, DeckCard? right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"CardCode = {CardCode}, Count = {Count}";
        }
    }
}
