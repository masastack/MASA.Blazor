using System.Diagnostics.CodeAnalysis;
using Masa.Blazor.Components.VideoFeeder;
using Masa.Blazor.JSComponents.VideoFeeder;

namespace Masa.Blazor;

public partial class MVideoFeeder
{
    [Parameter] public List<Video> Videos { get; set; } = [];

    [Parameter] public StringNumber? Height { get; set; } = "100vh";

    [Parameter] [MasaApiParameter("100%")]
    public StringNumber? Width { get; set; } = "100%";

    [Parameter] public bool DefaultAutoPlayNext { get; set; }

    /// <summary>
    /// Rotated 90 degrees in the vertical screen state to achieve the horizontal screen effect,
    /// which is generally used on the mobile terminal
    /// </summary>
    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs> OnFullscreen { get; set; }

    /// <summary>
    /// Event callback when clicking the "Download" item in the action menu.
    /// Only show when the parameter is set.
    /// </summary>
    [Parameter] public EventCallback<Video> OnDownload { get; set; }

    /// <summary>
    /// The slot for right action buttons.
    /// </summary>
    [Parameter] public RenderFragment<Video>? SideActionsContent { get; set; }

    /// <summary>
    /// The slot for top content. Used to display video title, subtitle, etc.
    /// </summary>
    [Parameter] public RenderFragment<Video>? TopContent { get; set; }

    /// <summary>
    /// The slot for bottom content. Used to display video title, subtitle, etc.
    /// </summary>
    [Parameter] public RenderFragment<Video>? BottomContent { get; set; }

    /// <summary>
    /// The slot for bottom sheet actions. Accepts the <see cref="MListItem"/> components.
    /// </summary>
    [Parameter] public RenderFragment<Video>? BottomActionsContent { get; set; }

    private static readonly Block Block = new("m-video-feeder");
    private readonly ModifierBuilder _blockBuilder = Block.CreateModifierBuilder();

    private Video? _prevVideo;
    private MSwiper? _swiper;
    private bool _sheet;
    private int _index;
    private bool _muted = true;
    private bool _autoPlayFirstVideo;
    private bool _fullscreen;
    private StringNumber? _playbackRate = 1.0;
    private bool _autoPlayNext;

    private Video? CurrentVideo => _index >= 0 && _index < Videos.Count ? Videos[_index] : null;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _autoPlayNext = DefaultAutoPlayNext;

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
        _playbackRate = 1;

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

    private async Task HandleOnEnded()
    {
        if (!_autoPlayNext || _index >= Videos.Count - 1)
        {
            return;
        }

        _index++;
        await _swiper.InvokeVoidAsync("slideTo", _index);
        OnIndexUpdated();
    }

    private void HandleOnLongPress()
    {
        _sheet = true;
    }

    private async Task SetPlaybackRate(StringNumber? value)
    {
        _playbackRate = value!;

        if (CurrentVideo?.Player is not null)
        {
            await CurrentVideo.Player.SetPlaybackRateAsync(_playbackRate.AsT2);
        }
    }

    private async Task ToggleMode()
    {
        _sheet = false;

        if (CurrentVideo?.Player is not null)
        {
            await CurrentVideo.Player.ToggleModeAsync();
        }
    }
}