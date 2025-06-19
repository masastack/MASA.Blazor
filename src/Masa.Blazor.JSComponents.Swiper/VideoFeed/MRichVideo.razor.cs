using Element = BemIt.Element;

namespace Masa.Blazor.Components.VideoFeed;

public partial class MRichVideo : MasaComponentBase
{
    [Parameter] [EditorRequired] public Video Data { get; set; } = null!;

    [Parameter] public bool Muted { get; set; }

    [Parameter] public bool InternalFullscreenMode { get; set; }

    [Parameter] public EventCallback<bool> MutedChanged { get; set; }

    [Parameter] public EventCallback<Video> OnFullscreen { get; set; }

    [Parameter] public EventCallback<Video> OnCloseFullscreen { get; set; }

    [Parameter] public RenderFragment<Video>? ActionsContent { get; set; }

    private static readonly Element Element = new("m-video-feed", "video");
    private readonly ModifierBuilder _modifierBuilder = Element.CreateModifierBuilder();

    private readonly string _id = "mv-" + Guid.NewGuid().ToString("N");

    private string? _markup;
    private bool _prevPlaying;
    private bool _internalPlaying;
    private bool _prevMuted;
    private MVideoControls? _videoControls;

    private VideoMetadata _videoMetadata = new();
    private DotNetObjectReference<MRichVideo>? _dotnetObjectReference;
    private IJSObjectReference? _videoFeedHelper;
    private bool _hasRendered;
    private bool _available;

    private bool _fullscreen;

    protected override Task OnInitializedAsync()
    {
        _prevMuted = Muted;
        _prevPlaying = Data.Playing;
        _internalPlaying = Data.Playing;

        _markup = $$"""
                    <video id="{{_id}}"
                           poster="{{Data.Poster}}"
                           preload="metadata"
                           loop
                           x5-video-player-type="h5"
                           x5-video-player-fullscreen="false"
                           webkit-playsinline="true"
                           x5-playsinline="true"
                           playsinline="true"
                           fullscreen="false"
                           {{(Data.Playing ? "autoplay" : "")}}
                           muted
                           loading="lazy">
                        <source src="{{Data.Src}}" type="video/mp4">
                        <p>Your browser does not support HTML5 video.</p>
                    </video>
                    """;


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

            _hasRendered = true;
            StateHasChanged();
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_dotnetObjectReference is null && Data.Playing && _hasRendered)
        {
            await InitJSInteropAsync();
        }
    }

    private async Task InitJSInteropAsync()
    {
        _dotnetObjectReference = DotNetObjectReference.Create(this);
        var importJS = await Js.InvokeAsync<IJSObjectReference>("import",
            "./_content/Masa.Blazor.JSComponents.Swiper/video-feed.js");
        _videoFeedHelper = await importJS.InvokeAsync<IJSObjectReference>("create", _id, _dotnetObjectReference);
        _ = importJS.DisposeAsync();
        _videoMetadata = await _videoFeedHelper.InvokeAsync<VideoMetadata>("init");
        _ = ToggleMute(Muted);
        _available = true;
        StateHasChanged();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

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

    [JSInvokable]
    public void OnLoadedMetadata(VideoMetadata metadata)
    {
        _videoMetadata = metadata;
        StateHasChanged();
    }

    [JSInvokable]
    public void OnTimeUpdate(double currentTime)
    {
        _videoControls?.UpdateTimeInternal(currentTime);
    }

    private async Task HandleOnFullscreen()
    {
        if (InternalFullscreenMode)
        {
            _fullscreen = true;
        }
        else
        {
            await _videoFeedHelper.TryInvokeVoidAsync("requestFullscreen");
        }

        await OnFullscreen.InvokeAsync(Data);
    }

    private async Task CloseFullscreen()
    {
        _fullscreen = false;
        await OnCloseFullscreen.InvokeAsync(Data);
    }

    private async Task UpdateVideoTimeInJS(double time)
    {
        await _videoFeedHelper.TryInvokeVoidAsync("update", time).ConfigureAwait(false);
    }

    private async Task ToggleMute(bool muted)
    {
        await _videoFeedHelper.TryInvokeVoidAsync("toggleMute", muted).ConfigureAwait(false);
    }

    private async Task Pause()
    {
        _internalPlaying = false;
        await _videoFeedHelper.TryInvokeVoidAsync("pause").ConfigureAwait(false);
    }

    private async Task Play()
    {
        _internalPlaying = true;
        await _videoFeedHelper.TryInvokeVoidAsync("play").ConfigureAwait(false);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_videoFeedHelper is not null)
        {
            _ = _videoFeedHelper.InvokeVoidAsync("dispose").ConfigureAwait(false);
        }
        
        await base.DisposeAsyncCore();
    }
}