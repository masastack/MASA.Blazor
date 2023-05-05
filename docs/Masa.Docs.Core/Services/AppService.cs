﻿namespace Masa.Docs.Core.Services;

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

    public static List<(string Title, string URL, string? Target, string? Badge)> GetNavMenus(string? project)
    {
        var list = new List<(string Title, string URL, string? Target, string? Badge)>()
        {
            new("docs", "/", null, null)
        };

        if (project == "blazor")
        {
            list.Add(("getting-started", "/blazor/getting-started/installation", null, null));
            list.Add(("ui-components", "/blazor/components/alerts", null, null));
            list.Add(("pro", "https://blazor-pro.masastack.com", "_blank", "free-pro"));
            list.Add(("blog", "https://blogs.masastack.com/tags/Blazor/", "_blank", null));
            list.Add(("official-website", "https://www.masastack.com/blazor", "_blank", null));
        }
        else if (project == "framework")
        {
            list.Add(("blog", "https://blogs.masastack.com/tags/MASA-Framework/", "_blank", null));
            list.Add(("official-website", "https://www.masastack.com/framework", "_blank", null));
        }
        else if (project == "stack")
        {
            list.Add(("blog", "https://blogs.masastack.com/tags/MASA-Stack/", "_blank", null));
            list.Add(("official-website", "https://www.masastack.com/stack", "_blank", null));
        }
        else
        {
            list.Add(("blog", "https://blogs.masastack.com", "_blank", null));
            list.Add(("official-website", "https://www.masastack.com", "_blank", null));
        }

        return list;
    }
}
