namespace Masa.Blazor.Components.VideoFeed;

public class VideoMetadata
{
    public double Duration { get; set; }

    /// <summary>
    /// The aspect ratio of the video, calculated as width / height.
    /// </summary>
    public double AspectRatio { get; set; }

    public bool IsHorizontal => AspectRatio > 1;
}