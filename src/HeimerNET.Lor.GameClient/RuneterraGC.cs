using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeimerNET.Lor.GameClient;

public class RuneterraGC : IRuneterraGC
{
    private readonly HttpClient _client = new();

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<StaticDecklist> GetStaticDecklistAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<StaticDecklist>("static-decklist", cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<PositionalRectangles> GetPositionalRectanglesAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<PositionalRectangles>("positional-rectangles", cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<ExpeditionsState> GetExpeditionsStateAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<ExpeditionsState>("expeditions-state", cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<GameResult> GetGameResultAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<GameResult>("game-result", cancellationToken).ConfigureAwait(false);
    }

    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</exception>
    private async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken)
    {
        using var response = await _client.GetAsync(path, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var obj = await JsonSerializer.DeserializeAsync<T>(stream, nullOptions, cancellationToken).ConfigureAwait(false);
        return obj!;
    }

    private const JsonSerializerOptions? nullOptions = null;
}
