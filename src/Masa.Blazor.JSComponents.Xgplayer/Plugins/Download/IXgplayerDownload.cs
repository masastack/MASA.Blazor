namespace Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;

public interface IXgplayerDownload
{
    /// <summary>
    /// The plug-in Dom mount location
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    PluginPosition Position { get; set; }

    int Index { get; set; }

    bool Disable { get; set; }
}