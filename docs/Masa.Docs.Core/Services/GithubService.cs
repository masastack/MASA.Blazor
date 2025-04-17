using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Masa.Docs.Core.Services;

public class GithubService(ExpiryLocalStorage localStorage, ILogger<GithubService> logger)
{
    private readonly ConcurrentDictionary<string, GitHubClient> _cache = new();
    private IReadOnlyList<Release> _allReleases = [];

    private GitHubClient CreateClient(string owner, string repo)
    {
        var key = $"{owner}-{repo}";

        var client = _cache.GetOrAdd(key, new GitHubClient(new ProductHeaderValue(key))
        {
            Credentials = new Credentials("docs_token_read_only", AuthenticationType.Bearer)
        });

        return client;
    }

    public record IssueCount(int Open, int Closed);

    public record LatestBuild(string? Sha, string? Version);

    public async Task<IssueCount> SearchIssuesAsync(string owner, string repo, IEnumerable<string> labels,
        string cacheKey)
    {
        var key = repo + "__issueCount__" + cacheKey;

        var issueCount = await localStorage.GetExpiryItemAsync<IssueCount>(key);
        if (issueCount != null)
        {
            return issueCount;
        }

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
                open = result.Items.Count(item => item.State == ItemState.Open);
                closed = result.Items.Count(item => item.State == ItemState.Closed);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching issue count");
        }

        issueCount = new IssueCount(open, closed);
        await localStorage.SetExpiryItemAsync(key, issueCount, TimeSpan.FromDays(1));
        return new IssueCount(open, closed);
    }

    public async Task<IReadOnlyList<Release>> FetchReleasesAsync(string owner, string repo)
    {
        if (_allReleases.Count == 0)
        {
            var client = CreateClient(owner, repo);
            try
            {
                var releases = await client.Repository.Release.GetAll(owner, repo);
                if (releases.Count > 0)
                {
                    _allReleases = releases;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error fetching releases");
            }
        }

        return _allReleases;
    }

    public async Task<LatestBuild?> GetLatestBuildAsync(string owner, string repo)
    {
        var key = repo + "__latestBuild";

        var latestBuild = await localStorage.GetExpiryItemAsync<LatestBuild>(key);
        if (latestBuild != null)
        {
            return latestBuild;
        }

        try
        {
            var client = CreateClient(owner, repo);

            var releaseResponse = await client.Repository.Release.GetLatest(owner, repo);
            if (releaseResponse is null) return null;
            var release = releaseResponse.TagName;
            
            var runsResponse = await client.Actions.Workflows.Runs.ListByWorkflow(owner, repo,
                ".github/workflows/wasm-prd.yml",
                new WorkflowRunsRequest
                {
                    Status = CheckRunStatusFilter.Success
                });

            var latest = runsResponse.WorkflowRuns.FirstOrDefault();
            if (latest == null) return null;
            var commitSha = latest.HeadSha;

            latestBuild = new LatestBuild(commitSha, release);
            await localStorage.SetExpiryItemAsync(key, latestBuild, TimeSpan.FromHours(1));
            return latestBuild;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching latest build");
            return null;
        }
    }
}