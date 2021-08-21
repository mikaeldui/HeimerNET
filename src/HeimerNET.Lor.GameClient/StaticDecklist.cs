﻿using System.Collections.Generic;

namespace HeimerNET.Lor.GameClient
{
    /// <summary>
    /// Represent the player's deck in the game.
    /// </summary>
    public record StaticDecklist
    {
        /// <summary>
        /// Deckcode of current deck.
        /// </summary>
        public string? DeckCode { get; init; }

        /// <summary>
        /// Cards in current deck.
        /// </summary>
        public Dictionary<string, int>? CardsInDeck { get; init; }
    }
}
