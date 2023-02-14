namespace Masa.Docs.Core.Services;

public class AppService
{
    public const int AppBarHeight = 96;
    public const int MobileAppBarHeight = 64;

    private List<MarkdownItTocContent>? _toc;

    public event EventHandler<List<MarkdownItTocContent>?>? TocChanged;

    public List<MarkdownItTocContent>? Toc
    {
        get => _toc;
        set
        {
            _toc = value;
            TocChanged?.Invoke(this, value);
        }
    }

    public static List<(string Title, string URL, string Target)> GetNavMenus(string? project)
    {
        var list = new List<(string Title, string URL, string Target)>()
        {
            new("docs", "/", "")
        };

        if (project == "blazor")
        {
            list.Add(("getting-started", "/blazor/getting-started/installation", ""));
            list.Add(("ui-components", "/blazor/components/alerts", ""));
            list.Add(("pro", "https://blazor-pro.masastack.com", "_blank"));
            list.Add(("blog", "https://blogs.masastack.com/tags/Blazor/", "_blank"));
            list.Add(("official-website", "https://www.masastack.com/blazor", "_blank"));
        }
        else if (project == "framework")
        {
            list.Add(("blog", "https://blogs.masastack.com/tags/MASA-Framework/", "_blank"));
            list.Add(("official-website", "https://www.masastack.com/framework", "_blank"));
        }
        else if (project == "stack")
        {
            list.Add(("blog", "https://blogs.masastack.com/tags/MASA-Stack/", "_blank"));
            list.Add(("official-website", "https://www.masastack.com/stack", "_blank"));
        }
        else
        {
            list.Add(("blog", "https://blogs.masastack.com", "_blank"));
            list.Add(("official-website", "https://www.masastack.com", "_blank"));
        }

        return list;
    }
}
