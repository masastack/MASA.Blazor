using Masa.Blazor.Components.VideoFeeder;

namespace Masa.Blazor.JSComponents.VideoFeeder;

public class Video<TItem> where TItem : notnull
{
    internal Video(TItem item, Func<TItem, string?> url, Func<TItem, string?>? poster, Func<TItem, double>? startTime)
    {
        Poster = poster?.Invoke(item);
        Url = url.Invoke(item);
        StartTime = startTime?.Invoke(item) ?? 0;
        Item = item;
    }

    public string? Poster { get; init; }

    public string? Url { get; init; }

    public double StartTime { get; init; }

    public TItem Item { get; init; }

    internal bool Playing { get; set; }

    internal bool Muted { get; set; } = true;

    internal Player<TItem>? Player { get; set; }
}