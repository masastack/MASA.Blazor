using System.Net.Http.Json;
using System.Text.Json;
using Masa.Docs.Core.JsonConverters;

namespace Masa.Docs.Core.Services;

public class DocService
{
    private readonly static ConcurrentCache<string, ValueTask<List<NavItem>>> s_projectNavsCache = new();
    private readonly static ConcurrentCache<string, ValueTask<string>> s_documentCache = new();

    private readonly I18n _i18n;

    private readonly HttpClient _httpClient;

    private readonly Lazy<Task<Dictionary<string, string>?>> _projectMap;

    public DocService(IHttpClientFactory factory, I18n i18n)
    {
        _i18n = i18n;

        _httpClient = factory.CreateClient("masa-docs");

        _projectMap = new Lazy<Task<Dictionary<string, string>?>>(async () =>
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Dictionary<string, string>>("_content/Masa.Docs.Shared/data/project.json");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        });
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
}
