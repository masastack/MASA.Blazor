using Masa.Blazor.Components.VideoFeed;

namespace Masa.Blazor;

public partial class MVideoFeed : MasaComponentBase
{
    [Parameter] public List<Video> Videos { get; set; } = [];

    [Parameter] public EventCallback<Video> OnFullscreen { get; set; }

    [Parameter] public EventCallback<Video> OnCloseFullscreen { get; set; }

    [Parameter] public RenderFragment<Video>? ActionsContent { get; set; }

    [Parameter] public bool InternalFullscreenMode { get; set; }

    [Parameter] public StringNumber? Height { get; set; } = "100vh";

    [Parameter] [MasaApiParameter("100%")] public StringNumber? Width { get; set; } = "100%";

    private static readonly Block _block = new("m-video-feed");
    private ModifierBuilder _blockBuilder = _block.CreateModifierBuilder();

    private Video? _prevVideo;
    private MSwiper? _swiper;
    private int _index;
    private bool _muted = true;
    private bool _autoPlayFirstVideo;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        AutoPlayFirstVideo();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AutoPlayFirstVideo();
    }

    private void AutoPlayFirstVideo()
    {
        if (_autoPlayFirstVideo || Videos.Count == 0) return;

        _prevVideo = Videos.First();
        _prevVideo.Playing = true;
        _autoPlayFirstVideo = true;
    }

    private void OnClick()
    {
        var activeVideo = Videos.ElementAtOrDefault(_index);
        if (activeVideo is not null)
        {
            activeVideo.Playing = !activeVideo.Playing;
        }
    }

    private void IndexChanged(int index)
    {
        _index = index;

        OnIndexUpdated();
    }

    private void OnIndexUpdated()
    {
        if (_prevVideo is not null)
        {
            _prevVideo.Playing = false;
        }

        var video = Videos.ElementAtOrDefault(_index);
        _prevVideo = video;
        if (video is null)
        {
            return;
        }

        video.Playing = true;
    }

    private async Task HandleOnFullscreen(Video video)
    {
        if (InternalFullscreenMode)
        {
            await _swiper.InvokeVoidAsync("disable");
        }

        await OnFullscreen.InvokeAsync(video);
    }

    private async Task HandleOnCloseFullscreen(Video video)
    {
        if (InternalFullscreenMode)
        {
            await _swiper.InvokeVoidAsync("enable");
        }

        await OnCloseFullscreen.InvokeAsync(video);
    }
}