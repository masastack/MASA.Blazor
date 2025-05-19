namespace Masa.Blazor.Components.Xgplayer.Plugins.Time;

public interface IXgplayerTime
{
    /// <summary>
    /// The order of plugin dom in the block.
    /// </summary>
    int Index { get; }

    /// <summary>
    /// The position of the plugin dom.
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    PluginPosition Position { get; }
}
