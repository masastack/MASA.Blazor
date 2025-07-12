namespace Masa.Blazor.Components.Xgplayer.Plugins.DynamicBg;

public enum DynamicBgMode 
{
    /// <summary>
    /// Dynamic rendering according to the frame rate
    /// </summary>
    Framerate,

    /// <summary>
    /// Real-time rendering, with the highest rendering frequency
    /// </summary>
    Realtime,

    /// <summary>
    /// Render only the first frame
    /// </summary>
    Firstframe
}