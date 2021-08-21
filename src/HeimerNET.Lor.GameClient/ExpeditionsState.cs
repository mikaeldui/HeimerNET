using System.Text.Json.Serialization;

namespace HeimerNET.Lor.GameClient;

public record ExpeditionsState
{
    /// <summary>
    /// Flag indicating whether expedition is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Indicate the current state of the Expedition
    /// </summary>
    public StateExpe State { get; init; }

    /// <summary>
    /// win,loss
    /// </summary>
    [JsonPropertyName("Record")]
    public string[]? RecordRaw { get; init; }

    // todo Record to enum

    /// <summary>
    /// Picked card in draft,
    /// </summary>
    public DraftPick[]? DraftPicks { get; init; }

    /// <summary>
    /// Card (CardCode) in deck
    /// </summary>
    public string[]? Deck { get; init; }

    /// <summary>
    /// Count of games.
    /// </summary>
    public int Games { get; init; }

    /// <summary>
    /// Count of wins.
    /// </summary>
    public int Wins { get; init; }

    /// <summary>
    /// Count of losses. 
    /// </summary>
    public int Losses { get; init; }
}
