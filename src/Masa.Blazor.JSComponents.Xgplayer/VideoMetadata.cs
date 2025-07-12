namespace Masa.Blazor.JSComponents.Xgplayer;

public class VideoMetadata
{
    public double Width { get; set; }

    public double Height { get; set; }

    /// <summary>
    /// The aspect ratio of the video, calculated as width / height.
    /// </summary>
    public double AspectRatio { get; set; }

    public bool IsHorizontal => AspectRatio > 1;
}