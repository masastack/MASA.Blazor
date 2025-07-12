using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.JSComponents.VideoSwiper;
using Element = BemIt.Element;

namespace Masa.Blazor.Components.VideoSwiper;

public partial class MRichVideo : MasaComponentBase
{
    [Parameter] [EditorRequired] public Video Data { get; set; } = null!;

    [Parameter] public bool Muted { get; set; }

    [Parameter] public EventCallback<bool> MutedChanged { get; set; }

    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs> OnFullscreen { get; set; }

    [Parameter] public RenderFragment<Video>? ActionsContent { get; set; }

    [Parameter] public RenderFragment<Video>? TopContent { get; set; }

    [Parameter] public RenderFragment<Video>? BottomContent { get; set; }

    private static readonly string[] IgnoredXgplayerPlugins =
        [BuiltInPlugin.Play, BuiltInPlugin.PlaybackRate, BuiltInPlugin.CssFullscreen, BuiltInPlugin.Volume];

    private static readonly Block _block = new("m-video-feed");
    private static readonly Element ControlsElement = _block.Element("controls");
    private static readonly Element ControlsTopElement = _block.Element("controls-top");
    private static readonly Element ControlsBottomElement = _block.Element("controls-bottom");
    private static readonly Element ControlsRightElement = _block.Element("controls-right");
    private static readonly Element ControlsFullscreenElement = _block.Element("controls-fullscreen");

    private bool _prevPlaying;
    private bool _internalPlaying;
    private bool _prevMuted;
    private MXgplayer? _xgplayer;

    private bool _available;

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
                await InitJSInteropAsync();
            }

            StateHasChanged();
        }
    }

    private async Task InitJSInteropAsync()
    {
        // _videoMetadata = await _VideoSwiperHelper.InvokeAsync<VideoMetadata>("init");
        _ = ToggleMute(Muted);
        _available = true;
        StateHasChanged();
        
        NextTick(() =>
        {
            
        });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Console.Out.WriteLine($"[MRichVideo] OnParametersSet {Data.Title} {Data.Playing}");
        if (!_available && Data.Playing)
        {
            _available = true;
        }

        if (_prevMuted != Muted)
        {
            _prevMuted = Muted;
            _ = ToggleMute(Muted);
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
}