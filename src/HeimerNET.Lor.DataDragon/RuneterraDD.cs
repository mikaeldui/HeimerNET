using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

//namespace HeimerNET.Lor.DataDragon
//{
//    public class RuneterraDD
//    {
//        readonly HttpClient _client = new() { BaseAddress = new Uri("https://dd.b.pvp.net") };
//        readonly string _path;

//        public RuneterraDD(string path)
//        {
//            _path = path;
//        }

//        public async Task DownloadSetBundleAsync(string version, string set, string language, bool lite = false, CancellationToken cancellationToken = default)
//        {
//            var stream = await _client.GetStreamAsync($"{version}/{set}-{(lite ? "lite-" : "")}{language}.zip", cancellationToken).ConfigureAwait(false);
//            stream.Position = 0;
//            var zip = new ZipArchive(stream);
//            zip.ExtractToDirectory(Path.Combine(_path, version), true);
//        }

//        public async Task DownloadLatestSetBundleAsync(string language, string set, bool lite = false, CancellationToken cancellationToken = default)
//        {
//            await DownloadSetBundleAsync("latest", set, language, lite, cancellationToken).ConfigureAwait(false);
//        }

//        public async Task DownloadCoreBundleAsync(string version, string language, CancellationToken cancellationToken = default)
//        {
//            var stream = await _client.GetStreamAsync($"{version}/core-{language}.zip", cancellationToken).ConfigureAwait(false);
//            stream.Position = 0;
//            var zip = new ZipArchive(stream);
//            zip.ExtractToDirectory(Path.Combine(_path, version), true);
//        }

//        public async Task DownloadLatestCoreBundleAsync(string language, CancellationToken cancellationToken = default)
//        {
//            await DownloadCoreBundleAsync("latest", language, cancellationToken).ConfigureAwait(false);
//        }

//        public async Task<Card[]> GetSetBundleDataAsync(string version, string set, string language, CancellationToken cancellationToken = default)
//        {
//            using var stream = new FileStream(Path.Combine(_path, version, language, "data", $"{set}-{language}.json"), FileMode.Open, FileAccess.Read, FileShare.Read);
//            return await JsonSerializer.DeserializeAsync<Card[]>(stream, null, cancellationToken).ConfigureAwait(false);
//        }

//        public async Task<Card[]> GetLatestSetBundleDataAsync(string language, string set, CancellationToken cancellationToken = default)
//        {
//            return await GetSetBundleDataAsync("latest", set, language, cancellationToken).ConfigureAwait(false);
//        }

//        public async Task<CoreBundle> GetCoreBundleDataAsync(string version, string language, CancellationToken cancellationToken = default)
//        {
//            using var stream = new FileStream(Path.Combine(_path, version, language, "data", $"globals-{language}.json"), FileMode.Open, FileAccess.Read, FileShare.Read);
//            return await JsonSerializer.DeserializeAsync<CoreBundle>(stream, null, cancellationToken).ConfigureAwait(false);
//        }

//        public async Task<CoreBundle> GetCoreBundleDataAsync(string language, CancellationToken cancellationToken = default)
//        {
//            return await GetCoreBundleDataAsync("latest", language, cancellationToken).ConfigureAwait(false);
//        }
//    }
//}
