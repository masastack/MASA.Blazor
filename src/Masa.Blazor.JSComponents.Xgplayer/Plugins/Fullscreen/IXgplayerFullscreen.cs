namespace Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;

public interface IXgplayerFullscreen
{
    /// <summary>
    /// The plug-in Dom mount location
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    PluginPosition Position { get; set; }

    int Index { get; set; }

    /// <summary>
    /// Whether to use rotating horizontal screen If the configuration is true, when the icon clicked,
    /// the dom will be rotated 90 degrees in the vertical screen state to achieve the horizontal screen effect,
    /// which is generally used on the mobile terminal
    /// </summary>
    bool RotateFullscreen { get; set; }

    /// <summary>
    /// Whether to use page full screen instead of full-screen function
    /// If the configuration is true, the full-screen button will call the full screen in the page
    /// </summary>
    bool UseCssFullscreen { get; set; }

    /// <summary>
    /// Whether to use the return button in the upper right corner when in full screen, this configuration is generally turned on on the mobile terminal
    /// </summary>
    bool NeedBackIcon { get; set; }

    /// <summary>
    /// Customize the full-screen dom，If it is null, we will call full screen on player.root.
    /// </summary>
    string? Target { get; set; }

    /// <summary>
    /// This configuration only takes effect when the system is in fullscreen and does not take effect when the CSS is in full screen.
    /// When this configuration is true, if ScreenOrientation Lock is available and the video aspect ratio is greater than 1,
    /// it will lock the screen orientation to landscape after requestFullScreen.
    /// Similar to rotateFullscreen, but rotateFullscreen is used in conjunction with CSS fullscreen.
    /// </summary>
    bool UseScreenOrientation { get; set; }

    /// <summary>
    /// Lock orientation after fullscreen, only effective when system fullscreen, not effective in CSS fullscreen.
    /// </summary>
    string LockOrientationType { get; set; }
}