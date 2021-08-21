﻿namespace HeimerNET.Lor.GameClient
{
    /// <summary>
    /// Result after the last game.
    /// </summary>
    public record GameResult
    {
        /// <summary>
        /// The <see cref="GameID"/> resets every time the client restarts and is incremented after a game is completed.
        /// </summary>
        /// <remarks>
        /// The GameID isn't associated with any other source of data. The value starts at -1.
        /// </remarks>
        public int GameID { get; init; } = -1;

        /// <summary>
        /// Flag indicating whether the local player has won,
        /// </summary>
        public bool LocalPlayerWon { get; init; }
    }
}
