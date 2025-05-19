using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Controls;
using Masa.Blazor.Components.Xgplayer.Plugins.Play;
using Masa.Blazor.Components.Xgplayer.Plugins.Start;
using Masa.Blazor.Components.Xgplayer.Plugins.Time;

namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerOptions
{
    public string? Width { get; set; }

    public string? Height { get; set; }

    /// <summary>
    /// If set with value <see langword="true"/> , player would invoke video.play() after enough media data loaded.
    /// </summary>
    /// <remarks>
    /// Notice In many cases, autoplay action was limited by browser policy, for more details see https://h5player.bytedance.com/guide/extends/aautoplay.html
    /// </remarks>
    public bool Autoplay { get; set; }

    /// <summary>
    /// Autoplay with video muted
    /// </summary>
    public bool AutoplayMuted { get; set; }

    /// <summary>
    /// Loading media resource immediately after player initialized.
    /// </summary>
    public bool VideoInit { get; set; } = true;

    /// <summary>
    /// Enable inline playing mode, would set playsinline DOM attribute to media element.
    /// For more details about inline playing mode see https://webkit.org/blog/6784/new-video-policies-for-ios/
    /// </summary>
    public bool Playsinline { get; set; }

    /// <summary>
    /// Default playback rate for media element, reference values: 0.5, 0.75, 1, 1.5, 2
    /// </summary>
    public float DefaultPlaybackRate { get; set; } = 1;

    /// <summary>
    /// Default volume for media element, reference values: 0 ~ 1
    /// </summary>
    public float Volume { get; set; } = 0.6f;

    /// <summary>
    /// Determine whether to play in a loop
    /// </summary>
    public bool Loop { get; set; }

    /// <summary>
    /// Post image of video
    /// </summary>
    public string? Poster { get; set; }

    /// <summary>
    /// The second of video to start playing
    /// </summary>
    public double StartTime { get; set; }

    /// <summary>
    /// DOM attributes for media element, for more details
    /// see https://developer.mozilla.org/en-US/docs/Web/API/HTMLMediaElement
    /// </summary>
    public Dictionary<string, object?>? VideoAttributes { get; set; }

    /// <summary>
    /// Player language
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// The fluid layout allows the player's width varies to follow the width of the parent element's change,
    /// and the height varies according to the height and width proportion of the configuration item
    /// (the player's width and height is the internal default value when width and height are not set).
    /// </summary>
    public bool Fluid { get; set; }

    /// <summary>
    /// When video resource was playing, fit raw height、width of video resource to player's
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public FitVideoSize FitVideoSize { get; set; }

    /// <summary>
    /// The fill mode of media resource
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public VideoFillMode VideoFillMode { get; set; }

    /// <summary>
    /// Player status after seeking
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public SeekedStatus SeekedStatus { get; set; }

    /// <summary>
    /// Marks for progress bar
    /// </summary>
    [JsonPropertyName("progressDot")]
    public IEnumerable<ProgressDot>? ProgressDots { get; set; }

    /// <summary>
    /// Thumbnail for user to preview unplayed video content
    /// </summary>
    public Thumbnail? Thumbnail { get; set; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public DomEventType DomEventType { get; set; }

    /// <summary>
    /// Whether to enable the screen and control bar separation mode,
    /// set to <see langword="false"/>, the control bar will be resident
    /// and will not overlap with the video screen.
    /// </summary>
    public bool MarginControls { get; set; }

    /// <summary>
    /// A list of plugins to be ignored.
    /// You can find all built-in plugins in <see cref="BuiltInPlugin"/>.
    /// </summary>
    public IEnumerable<string>? Ignores { get; set; }

    public XgplayerMusic? Music { get; set; }

    /// <summary>
    /// The options for controls plugin
    /// </summary>
    public IXgplayerControls? Controls { get; set; }

    public IXgplayerPlay? Play { get; set; }

    public IXgplayerTime? Time { get; set; }

    public IXgplayerStart? Start { get; set; }
}

public enum DomEventType
{
    Default,
    Touch,
    Mouse
}

public enum FitVideoSize
{
    Fixed,
    FixWidth,
    FixHeight,
    Auto
}

public enum VideoFillMode
{
    Auto,
    FillWidth,
    FillHeight,
    Fill,
    Contain,
}

public enum SeekedStatus
{
    Play,
    Pause,
    Auto
}

public class ProgressDot
{
    /// <summary>
    /// Id of progress dot
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The dot position in progress bar
    /// </summary>
    public int Time { get; set; }

    /// <summary>
    /// Text shown in progress dot when user hover on it
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The duration of fragment
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Style object for progress dot
    /// </summary>
    public Dictionary<string, object?>? Style { get; set; }
}

public class Thumbnail
{
    /// <summary>
    /// Thumbnail picture urls
    /// </summary>
    public IEnumerable<string>? Urls { get; set; }

    /// <summary>
    /// Total number for all thumbnail pictures
    /// </summary>
    public int PicNum { get; set; }

    /// <summary>
    /// Row number in seperate thumbnail picture
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Col number in seperate thumbnail picture
    /// </summary>
    public int Col { get; set; }

    /// <summary>
    /// Height for video frame in seperate thumbnail picture by px
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Width for video frame in seperate thumbnail picture by px
    /// </summary>
    public int Width { get; set; }
}
