using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeimerNET.Lor.GameClient
{
    public class RuneterraGC
    {
        private readonly HttpClient _client = new();

        /// <summary>
        ///  Gets or sets the port number of <see cref="RuneterraGC"/>
        /// </summary>
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
        /// The static-decklist endpoint can be used to describe the player's current deck in an active game.
        /// As the name of the endpoint suggests, this response remains static even after cards in the deck have been played.
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
        /// <remarks>
        /// The positional-rectangles endpoint can be used to determine the position of the cards in the collection, deck builder, Expedition drafts, and active games.
        /// Unlike the static-decklist endpoint, the positional-rectangles endpoint will return the position of the cards at the time of the request.
        /// The response time of this endpoint will vary by computer, however Riot suggest polling this endpoint no more than once per second.
        /// </remarks>
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
        /// <remarks>
        /// The expeditions-state endpoint can be used to determine the cards a player drafts during an Expedition.
        /// The request returns a number of fields including the current state of the Expedition and a list of card codes that have been drafted.
        /// </remarks>
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
        /// A request to the game-result endpoint can be made to determine the result of the player's most recently completed game.
        /// The <see cref="GameResult.GameID"/> resets every time the client restarts and is incremented after a game is completed.
        /// The <see cref="GameResult.GameID"/> isn't associated with any other source of data.
        /// </remarks>
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
}
