namespace Masa.Blazor;

public class Router : IRoutable
{
    public Router(IRoutable routable)
    {
        Attributes = routable.Attributes;
        Disabled = routable.Disabled;
        Href = routable.Href;
        Link = routable.Link;
        OnClick = routable.OnClick;
        Tag = routable.Tag;
        Target = routable.Target;
        Exact = routable.Exact;
        NavigationManager = routable.NavigationManager;
        MatchPattern = routable.MatchPattern;
    }

    public IDictionary<string, object?> Attributes { get; set; }

    public bool Disabled { get; set; }

    public string? Href { get; set; }

    public bool Link { get; set; }

    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public string? Tag { get; set; }

    public string? Target { get; set; }

    public bool Exact { get; set; }

    public string? MatchPattern { get; set; }

    public NavigationManager NavigationManager { get; set; }
}
