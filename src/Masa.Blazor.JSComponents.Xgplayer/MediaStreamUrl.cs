namespace Masa.Blazor.Components.Xgplayer;

public class MediaStreamUrl
{
    public string Src { get; set; }

    public string? Type { get; set; }

    public MediaStreamUrl(string src)
    {
        Src = src;
    }

    public MediaStreamUrl(string src, string type) : this(src)
    {
        Type = type;
    }
}
