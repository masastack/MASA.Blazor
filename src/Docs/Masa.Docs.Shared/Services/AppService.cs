using System.Net.Http.Json;
using System.Text.Json;

namespace Masa.Docs.Shared.Services;

public partial class AppService
{
    // private static readonly Dictionary<string, (string Icon, string color)> Categories = new()
    // {
    //     { "components", ("mdi-view-dashboard-outline", "indigo darken-1") },
    //     { "features", ("mdi-image-edit-outline", "red") },
    //     { "styles", ("mdi-palette-outline", "deep-purple accent-4") },
    // };

    public const int AppBarHeight = 96;
    public const int MobileAppBarHeight = 64;

    public static List<(string Title, string URL, string Target)> TopNavMenus => new()
    {
        ("Document","",""),
        ("Getting started","",""),
        ("Components","/components/alerts",""),
        ("Pro","http://blazor-pro.masastack.com","_blank"),
        ("Blog","https://blogs.masastack.com/categories/NET/Blazor","_blank"),
        ("Community","",""),
        ("Official website","","")
    };

    private readonly Lazy<Task<List<NavItem>>> _navs;
    private List<MarkdownItTocContent>? _toc;



    public event EventHandler<List<MarkdownItTocContent>?>? TocChanged;

    public AppService(IHttpClientFactory factory)
    {
        var httpClient = factory.CreateClient("masa-docs");

        _navs = new Lazy<Task<List<NavItem>>>(async () =>
        {
            var navs = await httpClient.GetFromJsonAsync<List<NavItem>>("_content/Masa.Docs.Shared/data/nav.json", new JsonSerializerOptions()
            {
                Converters = { new NavItemsJsonConverter() }
            });

            return navs ?? new List<NavItem>();
        });
    }

    public List<MarkdownItTocContent>? Toc
    {
        get => _toc;
        set
        {
            _toc = value;
            TocChanged?.Invoke(this, value);
        }
    }

    public async Task<List<NavItem>> GetNavs()
    {
        return await _navs.Value;
    }
}
