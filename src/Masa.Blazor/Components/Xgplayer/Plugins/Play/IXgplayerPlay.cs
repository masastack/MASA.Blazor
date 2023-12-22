using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.Xgplayer.Plugins.Play;

public interface IXgplayerPlay
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
