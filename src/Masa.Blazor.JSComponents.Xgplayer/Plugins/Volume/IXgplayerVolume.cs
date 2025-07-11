namespace Masa.Blazor.Components.Xgplayer.Plugins.Start;

public interface IXgplayerVolume
{
    PluginPosition Position { get; set; }

    int Index { get; set; }

    bool ShowValueLabel { get; set; }

    float Default { get; set; }

    float MiniVolume { get; set; }
}