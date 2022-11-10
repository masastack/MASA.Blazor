using System.Globalization;
using System.Net.Http.Json;
using BlazorComponent.I18n;

namespace Masa.Docs.Shared.Services;

public class DocService
{
    private readonly I18n _i18n;
    private readonly ConcurrentCache<string, ValueTask<string>> _documentCache = new();
    private readonly ConcurrentCache<string, ValueTask<string>> _exampleCache = new();
    private readonly ConcurrentCache<string, ValueTask<Dictionary<string, Dictionary<string, string>>?>> _apiCache = new();

    private readonly HttpClient _httpClient;

    private Dictionary<string, List<string>>? _apiInPageCache;

    public DocService(IHttpClientFactory factory, I18n i18n)
    {
        _i18n = i18n;
        _httpClient = factory.CreateClient("masa-docs");
    }

    public async Task<string> ReadDocumentAsync(string category, string title)
    {
        var key = $"{category}/{title}:{_i18n.Culture.Name}";
        return await _documentCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/Masa.Docs.Shared/docs/pages/{category}/{title}/{_i18n.Culture.Name}.md"));
    }

    public async Task<string> ReadExampleAsync(string category, string title, string example)
    {
        var key = $"{category}/{title}/{example}";
        return await _exampleCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/Masa.Docs.Shared/docs/pages/{category}/{title}/examples/{example}.txt"));
    }

    public async Task<Dictionary<string, List<string>>> ReadPageToApiAsync()
    {
        if (_apiInPageCache is not null && _apiInPageCache.Any())
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

    public async Task<Dictionary<string, Dictionary<string, string>>?> ReadApisAsync(string kebabCaseComponent)
    {
        var key = $"{kebabCaseComponent}:{_i18n.Culture.Name}";

        try
        {
            return await _apiCache.GetOrAdd(key, async _ => await _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string>>>(
                $"_content/Masa.Docs.Shared/docs/pages/apis/{kebabCaseComponent}/{_i18n.Culture.Name}.json"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
