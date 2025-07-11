namespace Masa.Blazor.JSComponents.VideoSwiper;

public class FullscreenEventArgs(Video video, bool isFullscreen) : EventArgs
{
    public Video Video { get; } = video;

    public bool IsFullscreen { get; } = isFullscreen;
}