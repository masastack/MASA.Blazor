using System.Net.Http.Json;

namespace Masa.Docs.Shared.Services;

public class DocService
{
    private readonly I18n _i18n;
    private readonly static ConcurrentCache<string, ValueTask<string>> _documentCache = new();
    private readonly static ConcurrentCache<string, ValueTask<string>> _exampleCache = new();
    private readonly static ConcurrentCache<string, ValueTask<Dictionary<string, Dictionary<string, string>>?>> _apiCache = new();
    private readonly Task<Dictionary<string, Dictionary<string, Dictionary<string, string>>>> _commonApisAsync;

    private readonly HttpClient _httpClient;

    private Dictionary<string, List<string>>? _apiInPageCache;

    public DocService(IHttpClientFactory factory, I18n i18n)
    {
        _i18n = i18n;
        _httpClient = factory.CreateClient("masa-docs");
        _commonApisAsync = _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>("_content/Masa.Docs.Shared/data/apis/common.json");
    }

    public async Task<string> ReadDocumentAsync(string category, string title)
    {
        var key = $"{category}/{title}:{_i18n.Culture.Name}";
        return await _documentCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/Masa.Docs.Shared/pages/{category}/{title}/{_i18n.Culture.Name}.md"));
    }

    public async Task<string> ReadExampleAsync(string category, string title, string example)
    {
        var key = $"{category}/{title}/{example}";
        return await _exampleCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/Masa.Docs.Shared/pages/{category}/{title}/examples/{example}.txt"));
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
            return await _apiCache.GetOrAdd(key, async _ =>
            {
                var apiInfo = await _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string>>>(
                $"_content/Masa.Docs.Shared/data/apis/{key}.json").ConfigureAwait(false);
                var _commonApis = await _commonApisAsync;
                if (_commonApis.TryGetValue(_i18n.Culture.Name, out var commonApiInfo))
                {
                    foreach (var (category, api) in apiInfo)
                    {
                        if (commonApiInfo.TryGetValue(category, out var commonApi))
                        {
                            foreach (var (prop, desc) in commonApi)
                                if (api.ContainsKey(prop) is false) api.Add(prop, desc);
                        }
                    }
                }
                return apiInfo;
            });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
