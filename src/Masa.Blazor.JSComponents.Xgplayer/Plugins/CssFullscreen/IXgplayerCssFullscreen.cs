namespace Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;

public interface IXgplayerCssFullscreen
{
    /// <summary>
    /// The plug-in Dom mount location
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    PluginPosition Position { get; set; }

    int Index { get; set; }

    bool Disable { get; set; }

    /// <summary>
    /// The custom DOM(CSS selector) for the web full screen, the default is the player root node.
    /// This configuration item must be a parent node of player.root.
    /// The scenario is to solve the scene where the dom at the same level as player.root needs to be displayed in the web full screen.
    /// </summary>
    string? Target { get; set; }
}