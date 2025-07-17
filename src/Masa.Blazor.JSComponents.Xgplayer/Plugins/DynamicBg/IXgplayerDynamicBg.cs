namespace Masa.Blazor.Components.Xgplayer.Plugins.DynamicBg;

public interface IXgplayerDynamicBg
{
    /// <summary>
    /// Whether to render only in the display area of the video screen 
    /// If true is selected when the control bar is separated from the video screen,
    /// the position of the control bar will not be rendered
    /// </summary>
    bool IsInnerRender { get; }

    /// <summary>
    /// Disable dynamic background. Default is <see langword="true"/>.
    /// </summary>
    bool Disable { get; }

    /// <summary>
    /// Default is <see cref="DynamicBgMode.Framerate"/>.
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    DynamicBgMode Mode { get; }

    /// <summary>
    /// The frame rate is rendered when framed,
    /// Only effective when <see cref="Mode"/> is <see cref="DynamicBgMode.Framerate"/>.
    /// Default is 10.
    /// </summary>
    double FrameRate { get; }

    /// <summary>
    /// Background Gaussian Blur Filter Settings. Default is "blur(50px)".
    /// </summary>
    string Filter { get; }

    /// <summary>
    /// A transparency mask. Default is <see langword="true"/>.
    /// </summary>
    bool AddMask { get; }

    /// <summary>
    /// Mask color configuration, only effective when addMask is <see langword="true"/>.
    /// Default is "rgba(0, 0, 0, 0.7)".
    /// </summary>
    string MaskBg { get; }

    /// <summary>
    /// The multiple of the screen magnification when rendering.
    /// Default is 1.2.
    /// </summary>
    double Multiple { get; }
}