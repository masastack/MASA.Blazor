namespace Masa.Docs.Core.Services;

public class AppService
{
    public const int AppBarHeight = 96;
    public const int MobileAppBarHeight = 64;
    public const string ColorForNewState = "#05CD99";
    public const string ColorForUpdateState = "#FF5252";
    public const string ColorForBreakingChangeState = "#E040FB";
    public const string ColorForDeprecatedState = "#9E9E9E";

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

    public static List<DefaultItem> GetNavMenus(string? project)
    {
        var list = new List<DefaultItem>()
        {
            new("docs", "/", "^/$|^/((?!(blazor/components|blazor/getting-started)).)*/[^/]*"),
        };

        if (project == "blazor")
        {
            list.Add(new("getting-started", "/blazor/getting-started/installation", "/blazor/getting-started"));
            list.Add(new("ui-components", "/blazor/components/all", "/blazor/components"));
        }

        list.Add(new("annual-service", "/annual-service", "pricing", "red"));
        
        return list;
    }

    public static List<DefaultItem> GetResources(string? project)
    {
        var list = new List<DefaultItem>();

        if (project == "blazor")
        {
            list.Add(new("made-with-masa-blazor", "/blazor/resources/made-with-masa-blazor"));
            list.Add(new("pro", "https://blazor-pro.masastack.com", "free-pro", "green"));
        }

        list.Add(new("blog", "https://blogs.masastack.com/tags/MASA-Stack/"));
        list.Add(new("official-website", "https://www.masastack.com/stack"));

        return list;
    }
}