﻿using System.Diagnostics.CodeAnalysis;

namespace HeimerNET.Lor.GameClient
{
    /// <summary>
    /// Represent action in draft pick.
    /// </summary>
    public record DraftPick
    {
        /// <summary>
        /// Flag indicate if is swap action or pick action.
        /// </summary>
        [MemberNotNullWhen(true, nameof(SwappedIn), nameof(SwappedOut))]
        [MemberNotNullWhen(false, nameof(Picks))]
        public bool IsSwap { get; init; }

        /// <summary>
        /// Picked cards.
        /// </summary>
        public string[]? Picks { get; init; }

        public string[]? SwappedIn { get; init; }

        public string[]? SwappedOut { get; init; }
    }
}
