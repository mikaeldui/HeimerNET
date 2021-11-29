using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HeimerNET.Lor.DataDragon;

public class RuneterraDD
{
    private readonly HttpClient _client = new() { BaseAddress = new Uri("https://dd.b.pvp.net") };
    private readonly string _destinationDirectoryName;

    /// <summary>
    /// Initializes a new instance of the <see cref="RuneterraDD"/> class.
    /// </summary>
    /// <param name="destinationDirectoryName">The path to the destination directory on the file system.</param>
    public RuneterraDD(string destinationDirectoryName)
    {
        _destinationDirectoryName = destinationDirectoryName;
    }

    public async Task DownloadSetBundleAsync(string version, string set, string language, bool lite = false, CancellationToken cancellationToken = default)
    {
        var stream = await _client.GetStreamAsync($"{version}/{set}-{(lite ? "lite-" : "")}{language}.zip", cancellationToken).ConfigureAwait(false);
        stream.Position = 0;
        var zip = new ZipArchive(stream);
        zip.ExtractToDirectory(Path.Combine(_destinationDirectoryName, version), true);
    }

    public async Task DownloadLatestSetBundleAsync(string language, string set, bool lite = false, CancellationToken cancellationToken = default)
    {
        await DownloadSetBundleAsync("latest", set, language, lite, cancellationToken).ConfigureAwait(false);
    }

    public async Task DownloadCoreBundleAsync(string version, string language, CancellationToken cancellationToken = default)
    {
        var stream = await _client.GetStreamAsync($"{version}/core-{language}.zip", cancellationToken).ConfigureAwait(false);
        stream.Position = 0;
        var zip = new ZipArchive(stream);
        zip.ExtractToDirectory(Path.Combine(_destinationDirectoryName, version), true);
    }

    public async Task DownloadLatestCoreBundleAsync(string language, CancellationToken cancellationToken = default)
    {
        await DownloadCoreBundleAsync("latest", language, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Card[]> GetSetBundleDataAsync(string version, string set, string language, CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(Path.Combine(_destinationDirectoryName, version, language, "data", $"{set}-{language}.json"), FileMode.Open, FileAccess.Read, FileShare.Read);
        return await JsonSerializer.DeserializeAsync<Card[]>(stream, nullOptions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Card[]> GetLatestSetBundleDataAsync(string language, string set, CancellationToken cancellationToken = default)
    {
        return await GetSetBundleDataAsync("latest", set, language, cancellationToken).ConfigureAwait(false);
    }

    public async Task<CoreBundle> GetCoreBundleDataAsync(string version, string language, CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(Path.Combine(_destinationDirectoryName, version, language, "data", $"globals-{language}.json"), FileMode.Open, FileAccess.Read, FileShare.Read);
        return await JsonSerializer.DeserializeAsync<CoreBundle>(stream, nullOptions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<CoreBundle> GetCoreBundleDataAsync(string language, CancellationToken cancellationToken = default)
    {
        return await GetCoreBundleDataAsync("latest", language, cancellationToken).ConfigureAwait(false);
    }

    private const JsonSerializerOptions? nullOptions = null;
}
