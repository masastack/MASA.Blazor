using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.JSComponents.VideoFeeder;
using Masa.Blazor.JSComponents.Xgplayer;
using Element = BemIt.Element;

namespace Masa.Blazor.Components.VideoFeeder;

public partial class Player : MasaComponentBase
{
    [Parameter] [EditorRequired] public Video Data { get; set; } = null!;

    [Parameter] public bool Muted { get; set; }

    [Parameter] public EventCallback<bool> MutedChanged { get; set; }

    [Parameter] public double PlaybackRate { get; set; } = 1;

    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs> OnFullscreen { get; set; }

    [Parameter] public RenderFragment<Video>? ActionsContent { get; set; }

    [Parameter] public RenderFragment<Video>? TopContent { get; set; }

    [Parameter] public RenderFragment<Video>? BottomContent { get; set; }

    private static readonly string[] IgnoredXgplayerPlugins =
        [BuiltInPlugin.Play, BuiltInPlugin.PlaybackRate, BuiltInPlugin.CssFullscreen, BuiltInPlugin.Volume];

    private static readonly Block _block = new("m-video-feeder");
    private static readonly Element ControlsElement = _block.Element("controls");
    private static readonly Element ControlsTopElement = _block.Element("controls-top");
    private static readonly Element ControlsBottomElement = _block.Element("controls-bottom");
    private static readonly Element ControlsRightElement = _block.Element("controls-right");
    private static readonly Element ControlsFullscreenElement = _block.Element("controls-fullscreen");

    private bool _prevPlaying;
    private bool _internalPlaying;
    private bool _prevMuted;
    private double _playbackRate = 1;
    private MXgplayer? _xgplayer;

    private bool _available;
    private string? _fullscreenChipStyle;

    protected override Task OnInitializedAsync()
    {
        _prevMuted = Muted;
        _prevPlaying = Data.Playing;
        _internalPlaying = Data.Playing;

        _available = Data.Playing;

        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (Data.Playing)
            {
                _ = ToggleMute(Muted);
            }
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!_available && Data.Playing)
        {
            _available = true;
        }

        if (_prevMuted != Muted)
        {
            _prevMuted = Muted;
            _ = ToggleMute(Muted);
        }

        if (_playbackRate != PlaybackRate)
        {
            _playbackRate = PlaybackRate;
            _ = SetPlaybackRateAsync();
        }

        var playing = _internalPlaying;

        if (_prevPlaying != Data.Playing)
        {
            _prevPlaying = Data.Playing;
            playing = Data.Playing;
        }

        if (_internalPlaying != playing)
        {
            _ = playing ? Play() : Pause();
        }
    }

    private async Task HandleOnFullscreen(bool fullscreen)
    {
        FullscreenEventArgs args = new(Data, fullscreen);
        await OnFullscreen.InvokeAsync(args);
    }

    private void HandleOnVideoResize(VideoSize videoSize)
    {
        _fullscreenChipStyle = "--m-video-height: " +
                               Math.Round(videoSize.VideoWidth / (videoSize.VideoScale / 1000), 2) + "px;";
        StateHasChanged();
    }

    private async Task ToggleMute(bool muted)
    {
        await _xgplayer.ToggleMutedAsync(muted).ConfigureAwait(false);
    }

    private async Task Pause()
    {
        _internalPlaying = false;
        await _xgplayer.TogglePlayAsync(false).ConfigureAwait(false);
    }

    private async Task Play()
    {
        _internalPlaying = true;
        await _xgplayer.TogglePlayAsync(true).ConfigureAwait(false);
    }

    private async Task GetFullscreenAsync()
    {
        await _xgplayer.InvokeVoidAsync("getRotateFullscreen").ConfigureAwait(false);
    }

    private async Task SetPlaybackRateAsync()
    {
        await _xgplayer.SetPropAsync("playbackRate", PlaybackRate).ConfigureAwait(false);
    }
}