using Masa.Blazor.JSComponents.VideoFeeder;

namespace Masa.Blazor;

public partial class MVideoFeeder
{
    [Parameter] public List<Video> Videos { get; set; } = [];

    [Parameter] public StringNumber? Height { get; set; } = "100vh";

    [Parameter] [MasaApiParameter("100%")] public StringNumber? Width { get; set; } = "100%";

    /// <summary>
    /// Rotated 90 degrees in the vertical screen state to achieve the horizontal screen effect,
    /// which is generally used on the mobile terminal
    /// </summary>
    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs> OnFullscreen { get; set; }

    [Parameter] public RenderFragment<Video>? ActionsContent { get; set; }
    
    [Parameter] public RenderFragment<Video>? TopContent { get; set; }

    [Parameter] public RenderFragment<Video>? BottomContent { get; set; }

    private static readonly Block _block = new("m-video-feeder");
    private ModifierBuilder _blockBuilder = _block.CreateModifierBuilder();

    private Video? _prevVideo;
    private MSwiper? _swiper;
    private int _index;
    private bool _muted = true;
    private bool _autoPlayFirstVideo;
    private bool _fullscreen;

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

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _blockBuilder.Add("rotate-fullscreen", RotateFullscreen && _fullscreen).Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        yield return $"--m-video-feed-width: {Width}";
        yield return $"--m-video-feed-height: {Height}";
    }

    private void AutoPlayFirstVideo()
    {
        if (_autoPlayFirstVideo || Videos.Count == 0) return;

        _prevVideo = Videos.First();
        _prevVideo.Playing = true;
        _autoPlayFirstVideo = true;
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

    private async Task HandleOnFullscreen(FullscreenEventArgs args)
    {
        await OnFullscreen.InvokeAsync(args);
        _fullscreen = args.IsFullscreen;
        await _swiper.InvokeVoidAsync(args.IsFullscreen ? "disable" : "enable");
    }
}