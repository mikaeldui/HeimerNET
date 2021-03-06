using System.Text.Json.Serialization;

namespace HeimerNET.Lor.GameClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GameState
{
    /// <summary>
    /// Player is in the collection view, deck builder or Expedition drafts.
    /// </summary>
    Menus,

    /// <summary>
    /// Player is in an active game.
    /// </summary>
    InProgress
}
