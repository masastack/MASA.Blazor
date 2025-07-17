namespace Masa.Blazor.JSComponents.VideoFeeder;

public class FullscreenEventArgs<TItem>(TItem item, bool isFullscreen) : EventArgs
{
    public TItem Item { get; } = item;

    public bool IsFullscreen { get; } = isFullscreen;
}