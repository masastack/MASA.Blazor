using System.Net.Http.Json;
using BlazorComponent.I18n;

namespace Masa.Blazor.Docs.Services;

public class BlazorDocService
{
    private readonly static ConcurrentCache<string, ValueTask<string>> s_exampleCache = new();
    private readonly static ConcurrentCache<string, ValueTask<Dictionary<string, Dictionary<string, string>>?>> s_apiCache = new();

    private readonly I18n _i18n;
    private readonly HttpClient _httpClient;
    private readonly Lazy<Task<Dictionary<string, Dictionary<string, Dictionary<string, string>>>?>> _commonApis;

    private Dictionary<string, List<string>>? _apiInPageCache;

    public BlazorDocService(IHttpClientFactory factory, I18n i18n)
    {
        _i18n = i18n;
        _httpClient = factory.CreateClient("masa-docs");

        _commonApis = new Lazy<Task<Dictionary<string, Dictionary<string, Dictionary<string, string>>>?>>(async () =>
            await _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(
                "_content/Masa.Blazor.Docs/data/apis/common.json"));
    }

    public async Task<string> ReadExampleAsync(string category, string title, string example)
    {
        var key = $"{category}/{title}/{example}";

        try
        {
            return await s_exampleCache.GetOrAdd(key,
                async _ => await _httpClient.GetStringAsync($"_content/Masa.Blazor.Docs/pages/{category}/{title}/examples/{example}.txt"));
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
            _apiInPageCache = await _httpClient.GetFromJsonAsync<Dictionary<string, List<string>>>("_content/Masa.Blazor.Docs/data/page-to-api.json");
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
                    $"_content/Masa.Blazor.Docs/data/apis/{key}.json").ConfigureAwait(false);
                var commonApis = await _commonApis.Value;
                if (commonApis is not null && commonApis.TryGetValue(_i18n.Culture.Name, out var commonApiInfo))
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
