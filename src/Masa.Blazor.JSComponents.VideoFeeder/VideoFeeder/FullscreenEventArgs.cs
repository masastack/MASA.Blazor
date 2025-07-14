namespace Masa.Blazor.JSComponents.VideoFeeder;

public class FullscreenEventArgs(Video video, bool isFullscreen) : EventArgs
{
    public Video Video { get; } = video;

    public bool IsFullscreen { get; } = isFullscreen;
}