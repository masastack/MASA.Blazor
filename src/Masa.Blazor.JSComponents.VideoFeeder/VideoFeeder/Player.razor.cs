using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.JSComponents.VideoFeeder;
using Masa.Blazor.JSComponents.Xgplayer;
using Masa.Blazor.JSModules;
using Masa.Blazor.JSModules.LongPress;
using Element = BemIt.Element;

namespace Masa.Blazor.Components.VideoFeeder;

public partial class Player<TItem> : MasaComponentBase where TItem : notnull
{
    [Inject] private ILongPressJSModule LongPressJSModule { get; set; } = null!;

    [Parameter] [EditorRequired] public Video<TItem> Data { get; set; } = null!;

    [Parameter] public bool Autoplay { get; set; }

    [Parameter] public int Index { get; set; }

    [Parameter] public bool GlobalMuted { get; set; }

    [Parameter] public EventCallback<bool> GlobalMutedChanged { get; set; }

    [Parameter] public bool Loop { get; set; }

    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public bool DynamicBg { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs<TItem>> OnFullscreen { get; set; }

    [Parameter] public EventCallback OnEnded { get; set; }

    [Parameter] public EventCallback<TItem> OnLongPress { get; set; }

    [Parameter] public RenderFragment<TItem>? ActionsContent { get; set; }

    [Parameter] public RenderFragment<TItem>? TopContent { get; set; }

    [Parameter] public RenderFragment<TItem>? BottomContent { get; set; }

    private static readonly string[] IgnoredXgplayerPlugins =
    [
        BuiltInPlugin.Play, BuiltInPlugin.PlaybackRate, BuiltInPlugin.CssFullscreen, BuiltInPlugin.Volume
    ];

    private static readonly string[] IgnoredXgplayerMusicPlugins =
    [
        BuiltInPlugin.PlaybackRate, BuiltInPlugin.Volume, BuiltInPlugin.MusicBackward, BuiltInPlugin.MusicPrev,
        BuiltInPlugin.MusicForward, BuiltInPlugin.MusicNext
    ];

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
                [nameof(MButton.Text)] = true,
                [nameof(MButton.Ripple)] = false
            },
            [nameof(MIcon)] = new Dictionary<string, object>()
            {
                [nameof(MIcon.Large)] = true,
            },
        };

    private double _playbackRate = 1;
    private MXgplayer? _xgplayer;
    internal bool _isMusic;

    /// <summary>
    /// due to swiper virtual mode, the player may not be available
    /// when the video is not in view
    /// </summary>
    private bool _available;

    private double _aspectRatio;
    private string? _fullscreenChipStyle;

    private LongPressJSObject? _longPressJSObject;

    private bool ShowFullscreenBtn => !_isMusic && _fullscreenChipStyle != null && _aspectRatio > 1;

    protected override Task OnInitializedAsync()
    {
        _available = Index == 0;

        return Task.CompletedTask;
    }

    private async Task OnReady()
    {
        await SetMuteAsync(Data.Muted);
        if (Data.Playing)
        {
            await SetPlayingAsync(true);
        }

        await RegisterLongPressEventAsync();
    }

    private async Task RegisterLongPressEventAsync()
    {
        // on mobile devices, the ".trigger" element is used to detect long press events
        var selector = "[data-player-index='" + Index + "'] .trigger";

        _longPressJSObject =
            await LongPressJSModule.RegisterAsync(selector, () => OnLongPress.InvokeAsync(Data.Item));

        // on desktop devices, the "video" element is used to detect long press events
        if (_longPressJSObject is null)
        {
            selector = "[data-player-index='" + Index + "'] video";
            _longPressJSObject =
                await LongPressJSModule.RegisterAsync(selector, () => OnLongPress.InvokeAsync(Data.Item));
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!_available && Data.Playing)
        {
            _available = true;
        }
    }

    private async Task HandleOnGlobalMutedChange(bool muted)
    {
        await SetMuteAsync(muted);
        await GlobalMutedChanged.InvokeAsync(muted);
    }

    private async Task HandleOnFullscreen(bool fullscreen)
    {
        FullscreenEventArgs<TItem> args = new(Data.Item, fullscreen);
        await OnFullscreen.InvokeAsync(args);
    }

    private void HandleOnMetadataLoaded(VideoMetadata metadata)
    {
        _aspectRatio = metadata.AspectRatio;
    }

    private void HandleOnVideoResize(VideoSize videoSize)
    {
        _fullscreenChipStyle = "--m-video-height: " +
                               Math.Round(videoSize.VideoWidth / (videoSize.VideoScale / 1000), 2) + "px;";
        StateHasChanged();
    }

    internal async Task SetMuteAsync(bool muted)
    {
        if (_xgplayer is null) return;
        await _xgplayer.ToggleMutedAsync(force: muted);
    }

    internal async Task SetPlayingAsync(bool playing)
    {
        if (_xgplayer is null) return;
        await _xgplayer.TogglePlayAsync(playing);
    }

    private async Task GetFullscreenAsync()
    {
        if (_xgplayer is null) return;
        await _xgplayer.InvokeVoidAsync("getRotateFullscreen");
    }

    internal async Task SetPlaybackRateAsync(double playbackRate)
    {
        if (_xgplayer is null) return;
        await _xgplayer.SetPropAsync("playbackRate", playbackRate);
    }

    internal async Task ToggleModeAsync()
    {
        if (_xgplayer is null) return;

        _isMusic = !_isMusic;

        if (_isMusic)
        {
            await _xgplayer.ToMusicPlayerAsync();
        }
        else
        {
            await _xgplayer.ToVideoPlayerAsync();
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_longPressJSObject is not null)
        {
            await _longPressJSObject.DisposeAsync();
        }
    }
}