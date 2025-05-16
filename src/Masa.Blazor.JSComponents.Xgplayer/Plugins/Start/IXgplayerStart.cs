namespace Masa.Blazor.Components.Xgplayer.Plugins.Start;

public interface IXgplayerStart
{
    /// <summary>
    /// Whether to show the start button when the video is paused
    /// </summary>
    [JsonPropertyName("isShowPause")]
    public bool ShowAtPause { get; set; }

    /// <summary>
    /// Whether to show the start button when the video is ended
    /// </summary>
    [JsonPropertyName("isShowEnd")]
    public bool ShowAtEnd { get; set; }

    /// <summary>
    /// Disable the click animation
    /// </summary>
    public bool DisableAnimate { get; set; }

    /// <summary>
    /// Determine the follow mode of the start button
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public StartMode Mode { get; set; }
}

public enum StartMode
{
    /// <summary>
    /// Hide when playing
    /// </summary>
    Hide,

    /// <summary>
    /// Always show
    /// </summary>
    Show,

    Auto
}
