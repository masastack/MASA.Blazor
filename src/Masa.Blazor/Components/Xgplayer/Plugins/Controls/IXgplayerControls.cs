using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.Xgplayer.Plugins.Controls;

public interface IXgplayerControls
{
    /// <summary>
    /// Whether to support automatic hiding, the default value is <see langword="true"/>,
    /// If the value is <see langword="false"/>, it will stay resident
    /// </summary>
    bool AutoHide { get; }

    /// <summary>
    /// The internal layout of the controlBar
    /// </summary>
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    ControlsMode Mode { get; }

    /// <summary>
    /// Whether to show the control bar when the player is initialized
    /// </summary>
    bool InitShow { get; }
}
