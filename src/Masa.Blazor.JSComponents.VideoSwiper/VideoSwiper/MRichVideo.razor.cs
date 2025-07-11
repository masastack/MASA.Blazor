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

    private static readonly Element Element = new("m-video-feed", "video");
    private readonly ModifierBuilder _modifierBuilder = Element.CreateModifierBuilder();

    private bool _prevPlaying;
    private bool _internalPlaying;
    private bool _prevMuted;
    private MVideoControls? _videoControls;
    private MXgplayer? _xgplayer;

    private bool _available;

    private bool _fullscreen;

    protected override Task OnInitializedAsync()
    {
        _prevMuted = Muted;
        _prevPlaying = Data.Playing;
        _internalPlaying = Data.Playing;

        _available = Data.Playing;

        Console.Out.WriteLine($"[MRichVideo] OnInitializedAsync {Data.Title} {Data.Playing}");

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
        _fullscreen = fullscreen;
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
}