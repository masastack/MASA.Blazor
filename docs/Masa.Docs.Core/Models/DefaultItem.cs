namespace Masa.Docs.Core.Models;

public class DefaultItem : IDefaultItem<DefaultItem>
{
    public string? Heading { get; set; }

    public bool Divider { get; set; }

    public string? Href { get; set; }

    public bool Exact { get; set; }

    public string? MatchPattern { get; set; }

    public string? Icon { get; set; }

    public string? Title { get; set; }

    public string? State { get; set; }

    public string? StateBackgroundColor { get; set; }

    public string? ReleasedOn { get; set; } 

    public StringNumber Value { get; set; }

    public List<DefaultItem>? Children { get; set; }

    public string? Target
    {
        get
        {
            if (Href == null)
            {
                return null;
            }

            return Href.StartsWith("http") ? "_blank" : "_self";
        }
    }

    public DefaultItem()
    {
    }

    public DefaultItem(string title, string href)
    {
        Title = title;
        Href = href;
    }

    public DefaultItem(string title, string href, string matchPattern) : this(title, href)
    {
        MatchPattern = matchPattern;
    }
    
    public DefaultItem(string title, string href, string state, string stateBackgroundColor) : this(title, href)
    {
        State = state;
        StateBackgroundColor = stateBackgroundColor;
    }
}
