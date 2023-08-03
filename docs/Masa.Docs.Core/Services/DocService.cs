using Masa.Docs.Core.JsonConverters;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Masa.Docs.Core.Services;

public class DocService
{
    private readonly static ConcurrentCache<string, ValueTask<List<NavItem>>> s_projectNavsCache = new();
    private readonly static ConcurrentCache<string, ValueTask<string>> s_documentCache = new();

    private readonly I18n _i18n;

    private readonly HttpClient _httpClient;

    private readonly Lazy<Task<Dictionary<string, Project>?>> _projectMap;

    public DocService(IHttpClientFactory factory, I18n i18n)
    {
        _i18n = i18n;

        _httpClient = factory.CreateClient("masa-docs");

        _projectMap = new Lazy<Task<Dictionary<string, Project>?>>(async () =>
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Dictionary<string, Project>>("_content/Masa.Docs.Shared/data/project.json");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        });
    }

    public async Task<string> ReadDocumentAsync(string project, string category, string title, string? subTitle = null)
    {
        var projectMap = await ReadProjectMapAsync();

        var projectFullName = projectMap[project].Path;

        if (subTitle != null)
        {
            subTitle += "/";
        }

        var cultureName = _i18n.Culture.Name;

        if (project != "blazor")
        {
            cultureName = "zh-CN";
        }

        var key = $"{category}/{title}/{subTitle}:{_i18n.Culture.Name}";
        return await s_documentCache.GetOrAdd(key,
            async _ => await _httpClient.GetStringAsync($"_content/{projectFullName}/pages/{category}/{title}/{subTitle}{cultureName}.md"));
    }

    public async Task<Dictionary<string, Project>> ReadProjectMapAsync()
    {
        var projectMap = await _projectMap.Value;
        ArgumentNullException.ThrowIfNull(projectMap);

        return projectMap;
    }

    public async Task<List<NavItem>> ReadNavsAsync(string project)
    {
        var projectMap = await ReadProjectMapAsync();

        var projectFullName = projectMap[project].Path;

        return await s_projectNavsCache.GetOrAdd(project, async _ =>
        {
            try
            {
                var navs = await _httpClient.GetFromJsonAsync<List<NavItem>>($"_content/{projectFullName}/data/nav.json", new JsonSerializerOptions()
                {
                    Converters = { new NavItemsJsonConverter() }
                });

                if (navs is not null)
                {
                    SetHref(navs, rootSegment: project);
                }

                return navs ?? new List<NavItem>();
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine(e);
                    return new List<NavItem>();
                }

                throw;
            }
        });
    }


    public static async Task<List<NavItem>> GetAllComponentsAsync()
    {
        var valueTaskResult = new ValueTask<List<NavItem>>();
        bool success = s_projectNavsCache.TryGetValue("blazor", out valueTaskResult);

        var result = new List<NavItem>();
        if (success)
        {
            var blazorNavs = await valueTaskResult.ConfigureAwait(false);
            result = blazorNavs.Where(nav => nav.Title == "ui-components").SelectMany(nav => nav.Children).ToList();
        }

        return result;
    }

    public static async Task<List<NavItem>> GetAllConponentsTileAsync()
    {
        var components = await GetAllComponentsAsync();

        if (components.Any())
        {
            // remove all-components
            components.RemoveAt(0);
            components = ResolveAllComponentsData(components);
        }

        return components;
    }

    private static List<NavItem> ResolveAllComponentsData(List<NavItem> navs)
    {
        var result = new List<NavItem>();

        foreach (var nav in navs)
        {
            if (nav.Children is not null && nav.Children.Any())
            {
                result.AddRange(ResolveAllComponentsData(nav.Children));
            }
            else
            {
                result.Add(nav);
            }
        }

        return result;
    }

    private static void SetHref(List<NavItem> navItems, string? segment = null, string? rootSegment = null)
    {
        foreach (var navItem in navItems)
        {
            if (((IDefaultItem<NavItem>)navItem).HasChildren())
            {
                var seg = navItem.Group ?? $"{segment}/{navItem.Title}";
                SetHref(navItem.Children!, seg, rootSegment);
            }
            else
            {
                navItem.Href = $"{rootSegment}/{segment?.Trim('/')}/{navItem.Segment}";
            }
        }
    }
}
