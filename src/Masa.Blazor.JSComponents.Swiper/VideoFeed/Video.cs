namespace Masa.Blazor.Components.VideoFeed;

public class Video(string? title, string? subtitle, string? src, string? poster)
{
    public Video(string title, string src) : this(title, null, src, null)
    {
    }

    public string? Title { get; set; } = title;

    public string? Subtitle { get; set; } = subtitle;

    public string? Poster { get; init; } = poster;

    public string? Src { get; init; } = src;

    internal bool Playing { get; set; }
}