using System.Diagnostics;
using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.JSComponents.VideoFeeder;
using Masa.Blazor.JSComponents.Xgplayer;
using Masa.Blazor.JSModules;
using Masa.Blazor.JSModules.LongPress;
using Element = BemIt.Element;

namespace Masa.Blazor.Components.VideoFeeder;

public partial class Player : MasaComponentBase
{
    [Inject] private ILongPressJSModule LongPressJSModule { get; set; } = null!;

    [Parameter] [EditorRequired] public Video Data { get; set; } = null!;

    [Parameter] public int Index { get; set; }

    [Parameter] public bool Muted { get; set; }

    [Parameter] public EventCallback<bool> MutedChanged { get; set; }

    [Parameter] public double PlaybackRate { get; set; } = 1;

    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs> OnFullscreen { get; set; }

    [Parameter] public EventCallback OnEnded { get; set; }

    [Parameter] public EventCallback<Video> OnLongPress { get; set; }

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

    private readonly IDictionary<string, IDictionary<string, object>> _rightActionDefaults =
        new Dictionary<string, IDictionary<string, object>>()
        {
            [nameof(MButton)] = new Dictionary<string, object>()
            {
                [nameof(MButton.Large)] = true,
                [nameof(MButton.Ripple)] = false
            },
            [nameof(MIcon)] = new Dictionary<string, object>()
            {
                [nameof(MIcon.Large)] = true,
            },
        };

    private bool _prevPlaying;
    private bool _internalPlaying;
    private bool _prevMuted;
    private double _playbackRate = 1;
    private MXgplayer? _xgplayer;

    /// <summary>
    /// due to swiper virtual mode, the player may not be available
    /// when the video is not in view
    /// </summary>
    private bool _available;

    private string? _fullscreenChipStyle;

    private LongPressJSObject? _longPressJSObject;

    protected override Task OnInitializedAsync()
    {
        _prevMuted = Muted;
        _prevPlaying = Data.Playing;
        _internalPlaying = Data.Playing;

        _available = Data.Playing;

        return Task.CompletedTask;
    }

    private async Task OnReady()
    {
        Console.Out.WriteLine("[Masa.Blazor.JSComponents.VideoFeeder] Player.OnReady() " + Index);

        await ToggleMute();
        await SetPlaybackRateAsync();

        await RegisterLongPressEventAsync();
    }

    private async Task RegisterLongPressEventAsync()
    {
        // on mobile devices, the ".trigger" element is used to detect long press events
        var selector = "[data-player-index='" + Index + "'] .trigger";

        _longPressJSObject =
            await LongPressJSModule.RegisterAsync(selector, () => OnLongPress.InvokeAsync(Data));

        // on desktop devices, the "video" element is used to detect long press events
        if (_longPressJSObject is null)
        {
            selector = "[data-player-index='" + Index + "'] video";
            _longPressJSObject =
                await LongPressJSModule.RegisterAsync(selector, () => OnLongPress.InvokeAsync(Data));
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!_available && Data.Playing)
        {
            _available = true;
        }

        if (!_available)
        {
            return;
        }

        if (_prevMuted != Muted)
        {
            _prevMuted = Muted;
            _ = ToggleMute();
        }

        if (Math.Abs(_playbackRate - PlaybackRate) > 0)
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

    private async Task ToggleMute()
    {
        await _xgplayer.ToggleMutedAsync(force: Muted);
    }

    private async Task Pause()
    {
        _internalPlaying = false;
        await _xgplayer.TogglePlayAsync(false);
    }

    private async Task Play()
    {
        _internalPlaying = true;
        await _xgplayer.TogglePlayAsync(true);
    }

    private async Task GetFullscreenAsync()
    {
        await _xgplayer.InvokeVoidAsync("getRotateFullscreen");
    }

    private async Task SetPlaybackRateAsync()
    {
        await _xgplayer.SetPropAsync("playbackRate", PlaybackRate);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_longPressJSObject is not null)
        {
            await _longPressJSObject.DisposeAsync();
        }
    }
}