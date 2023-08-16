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

    public async Task<(int open, int close)> SearchIssuesAsync(string owner, string repo, string term)
    {
        var issueCount = await _memoryCache.GetOrCreateAsync($"{owner}-{repo}__searchIssues_{term}", async entry =>
        {
            var request = new SearchIssuesRequest(term);
            request.Repos.Add(owner, repo);
            request.In = new[] { IssueInQualifier.Title };
            request.Type = IssueTypeQualifier.Issue;

            var client = CreateClient(owner, repo);

            var open = 0;
            var closed = 0;

            try
            {
                var result = await client.Search.SearchIssues(request);

                if (!result.IncompleteResults)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                    open = result.Items.Count(item => item.State == ItemState.Open);
                    closed = result.Items.Count(item => item.State == ItemState.Closed);
                }
            }
            catch
            {
                // ignored
            }

            return (open, closed);
        });

        return issueCount;
    }
    
    public async Task<(int open, int close)> SearchIssuesAsync(string owner, string repo, IEnumerable<string> labels, string cacheKey)
    {
        var issueCount = await _memoryCache.GetOrCreateAsync($"{owner}-{repo}__searchIssues_{cacheKey}", async entry =>
        {
            var request = new SearchIssuesRequest();
            request.Repos.Add(owner, repo);
            request.Type = IssueTypeQualifier.Issue;
            request.Labels = labels;

            var client = CreateClient(owner, repo);

            var open = 0;
            var closed = 0;

            try
            {
                var result = await client.Search.SearchIssues(request);

                if (!result.IncompleteResults)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                    open = result.Items.Count(item => item.State == ItemState.Open);
                    closed = result.Items.Count(item => item.State == ItemState.Closed);
                }
            }
            catch
            {
                // ignored
            }

            return (open, closed);
        });

        return issueCount;
    }

    public async Task<IReadOnlyList<Release>> FetchReleasesAsync(string owner, string repo)
    {
        var releases = await _memoryCache.GetOrCreateAsync($"{owner}-{repo}__releases", async entry =>
        {
            IReadOnlyList<Release>? result = null;

            try
            {
                var client = CreateClient(owner, repo);
                result = await client.Repository.Release.GetAll(owner, repo);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            }
            catch
            {
                // ignored
            }

            return result;
        });

        return releases ?? new List<Release>();
    }
}
