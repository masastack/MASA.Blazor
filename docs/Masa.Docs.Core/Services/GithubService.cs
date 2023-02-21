using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace Masa.Docs.Core.Services;

public class GithubService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ConcurrentDictionary<string, GitHubClient> _cache = new();

    public GithubService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    private GitHubClient CreateClient(string owner, string repo)
    {
        var key = $"{owner}-{repo}";

        var client = _cache.GetOrAdd(key, new GitHubClient(new ProductHeaderValue(key)));

        return client;
    }

    public async Task<IReadOnlyList<Release>> FetchReleasesAsync(string owner, string repo)
    {
        var releases = await _memoryCache.GetOrCreateAsync($"{owner}-{repo}__releases", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            var client = CreateClient(owner, repo);
            return await client.Repository.Release.GetAll(owner, repo);
        });

        return releases ?? new List<Release>();
    }
}
