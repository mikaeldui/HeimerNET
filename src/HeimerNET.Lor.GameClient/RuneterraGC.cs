using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeimerNET.Lor.GameClient;

public class RuneterraGC
{
    private readonly HttpClient _client = new();

    public int Port
    {
        get => _client.BaseAddress!.Port;
        set => _client.BaseAddress = new($"http://127.0.0.1:{value}");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RuneterraGC"/> class.
    /// </summary>
    /// <param name="port"></param>
    public RuneterraGC(int port = 21337)
    {
        Port = port;
    }

    /// <summary>
    /// Get player's current deck in an active game.
    /// </summary>
    /// <remarks>
    /// Sometimes it doesn't work (generated Decks - "Labs").
    /// </remarks>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<StaticDecklist> GetStaticDecklistAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<StaticDecklist>("static-decklist", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<PositionalRectangles> GetPositionalRectanglesAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<PositionalRectangles>("positional-rectangles", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<ExpeditionsState> GetExpeditionsStateAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<ExpeditionsState>("expeditions-state", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get <see cref="GameResult"/> after last game.
    /// </summary>
    /// <remarks>
    ///  The GameID resets every time the client restarts and is incremented after a game is completed.
    ///  The GameID isn't associated with any other source of data. Below is an example of a response from this endpoint.
    ///  </remarks>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<GameResult> GetGameResultAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<GameResult>("game-result", cancellationToken).ConfigureAwait(false);
    }

    /// <exception cref="HttpRequestException"></exception>
    async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken)
    {
        using var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, null, cancellationToken).ConfigureAwait(false);
    }
}
