namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerPropsAndStates
{
    public bool Autoplay { get; init; }

    public string? CrossOrigin { get; init; }

    public string? CurrentSrc { get; init; }

    public double CurrentTime { get; init; }

    public double Duration { get; init; }

    public double CumulateTime { get; init; }

    public double Volume { get; init; }

    public bool Muted { get; init; }

    public bool DefaultMuted { get; init; }

    public double PlaybackRate { get; init; }

    public bool Loop { get; init; }

    public string? Src { get; init; }

    public string? Lang { get; init; }

    public string? Version { get; init; }

    public XgplayerState State { get; init; }

    public bool Ended { get; init; }

    public bool Paused { get; init; }

    public XgplayerNetworkState NetworkState { get; init; }

    public XgplayerReadyState ReadyState { get; init; }

    public bool IsFullscreen { get; init; }

    public bool IsCssFullscreen { get; init; }

    public bool IsSeeking { get; init; }
}

public enum XgplayerState
{
    Error,
    Initial,
    Ready,
    Attaching,
    Attached,
    NotAllow,
    Running,
    Ended,
    Destroyed
}

public enum XgplayerNetworkState
{
    NetworkEmpty,
    NetworkIdle,
    NetworkLoading,
    NetworkNoSource
}

public enum XgplayerReadyState
{
    HaveNothing,
    HaveMetadata,
    HaveCurrentData,
    HaveFutureData,
    HaveEnoughData
}
