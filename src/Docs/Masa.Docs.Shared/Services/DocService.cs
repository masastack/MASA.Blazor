using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Masa.Docs.Shared.Services;

public class DocService
{
    private readonly static ConcurrentCache<string, ValueTask<List<NavItem>>> s_projectNavsCache = new();
    private readonly static ConcurrentCache<string, ValueTask<string>> s_documentCache = new();
    private readonly static ConcurrentCache<string, ValueTask<string>> s_exampleCache = new();
    private readonly static ConcurrentCache<string, ValueTask<Dictionary<string, Dictionary<string, string>>?>> s_apiCache = new();

    private readonly I18n _i18n;
    private readonly ILogger<DocService> _logger;

    private readonly HttpClient _httpClient;

    private readonly Lazy<Task<Dictionary<string, string>?>> _projectMap;

    private readonly Lazy<Task<List<NavItem>>> _navs;

    private readonly Task<Dictionary<string, Dictionary<string, Dictionary<string, string>>>?> _commonApisAsync;

    private Dictionary<string, List<string>>? _apiInPageCache;

    public DocService(IHttpClientFactory factory, I18n i18n, ILogger<DocService> logger)
    {
        _i18n = i18n;

        _logger = logger;

        _httpClient = factory.CreateClient("masa-docs");

        _projectMap = new Lazy<Task<Dictionary<string, string>?>>(() =>
            _httpClient.GetFromJsonAsync<Dictionary<string, string>>("_content/Masa.Docs.Shared/data/project.json"));

        _commonApisAsync =
            _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(
                "_content/Masa.Docs.Shared/data/apis/common.json");
    }

    public async Task<string> ReadDocumentAsync(string project, string category, string title)
    {
        var projectMap = await ReadProjectMapAsync();

        var projectFullName = projectMap[project];

        await s_projectNavsCache.GetOrAdd(project, async _ =>
        {
            var navs = await _httpClient.GetFromJsonAsync<List<NavItem>>($"_content/{projectFullName}/data/nav.json", new JsonSerializerOptions()
            {
                Converters = { new NavItemsJsonConverter() }
            });
            return navs ?? new List<NavItem>();
        });

        var key = $"{category}/{title}:{_i18n.Culture.Name}";
        return await s_documentCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/{projectFullName}/pages/{category}/{title}/{_i18n.Culture.Name}.md"));
    }

    public async Task<Dictionary<string, string>> ReadProjectMapAsync()
    {
        var projectMap = await _projectMap.Value;
        ArgumentNullException.ThrowIfNull(projectMap);

        return projectMap;
    }

    public async Task<List<NavItem>> ReadNavsAsync(string project)
    {
        var projectMap = await ReadProjectMapAsync();

        var projectFullName = projectMap[project];

        return await s_projectNavsCache.GetOrAdd(project, async _ =>
        {
            var navs = await _httpClient.GetFromJsonAsync<List<NavItem>>($"_content/{projectFullName}/data/nav.json", new JsonSerializerOptions()
            {
                Converters = { new NavItemsJsonConverter() }
            });
            return navs ?? new List<NavItem>();
        });
    }

    public async Task<string> ReadExampleAsync(string category, string title, string example)
    {
        var key = $"{category}/{title}/{example}";

        try
        {
            return await s_exampleCache.GetOrAdd(key,
                async _ => await _httpClient.GetStringAsync($"_content/Masa.Docs.Shared/pages/{category}/{title}/examples/{example}.txt"));
        }
        catch (Exception e)
        {
            // TODO: log only in dev environment
            Console.WriteLine(e);
        }

        return string.Empty;
    }

    public async Task<Dictionary<string, List<string>>> ReadPageToApiAsync()
    {
        if (_apiInPageCache?.Any() is true)
        {
            return _apiInPageCache;
        }

        try
        {
            _apiInPageCache = await _httpClient.GetFromJsonAsync<Dictionary<string, List<string>>>("_content/Masa.Docs.Shared/data/page-to-api.json");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return _apiInPageCache ?? new Dictionary<string, List<string>>();
    }

    public async Task<Dictionary<string, Dictionary<string, string>>?> ReadApisAsync(string kebabCaseComponent, string? apiName = null)
    {
        var key = $"{kebabCaseComponent}/{(apiName is null ? "" : apiName + "-")}{_i18n.Culture.Name}";

        try
        {
            return await s_apiCache.GetOrAdd(key, async _ =>
            {
                var apiInfo = await _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string>>>(
                    $"_content/Masa.Docs.Shared/data/apis/{key}.json").ConfigureAwait(false);
                var commonApis = await _commonApisAsync;
                if (commonApis.TryGetValue(_i18n.Culture.Name, out var commonApiInfo))
                {
                    foreach (var (category, api) in apiInfo)
                    {
                        if (commonApiInfo.TryGetValue(category, out var commonApi))
                        {
                            foreach (var (prop, desc) in commonApi)
                                if (api.ContainsKey(prop) is false)
                                    api.Add(prop, desc);
                        }
                    }
                }

                return apiInfo;
            });
        }
        catch (Exception e)
        {
            // TODO: log only in dev environment
            Console.WriteLine(e);
            return null;
        }
    }
}
