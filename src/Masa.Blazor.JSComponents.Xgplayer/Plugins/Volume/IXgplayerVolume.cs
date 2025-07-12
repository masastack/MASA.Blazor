namespace Masa.Blazor.Components.Xgplayer.Plugins.Start;

public interface IXgplayerVolume
{
    PluginPosition Position { get; set; }

    int Index { get; set; }

    bool ShowValueLabel { get; set; }

    double Default { get; set; }

    double MiniVolume { get; set; }
}