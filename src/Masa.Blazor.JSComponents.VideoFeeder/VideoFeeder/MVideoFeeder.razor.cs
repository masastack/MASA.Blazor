﻿using Masa.Blazor.JSComponents.VideoFeeder;

namespace Masa.Blazor;

public partial class MVideoFeeder<TItem> where TItem : notnull
{
    [Parameter] public List<TItem> Items { get; set; } = [];

    [Parameter] [EditorRequired] public Func<TItem, string>? ItemUrl { get; set; }

    [Parameter] public Func<TItem, string?>? ItemPoster { get; set; }

    [Parameter] [MasaApiParameter(844)] public StringNumber? Height { get; set; } = 844;

    [Parameter] [MasaApiParameter(390)] public StringNumber? Width { get; set; } = 390;

    [Parameter] public bool DefaultAutoplayNext { get; set; }

    [Parameter] public bool Autoplay { get; set; }

    [Parameter] public bool DynamicBg { get; set; }

    /// <summary>
    /// Rotated 90 degrees in the vertical screen state to achieve the horizontal screen effect,
    /// which is generally used on the mobile terminal
    /// </summary>
    [Parameter] public bool RotateFullscreen { get; set; }

    [Parameter] public EventCallback<FullscreenEventArgs<TItem>> OnFullscreen { get; set; }

    /// <summary>
    /// Event callback when clicking the "Download" item in the action menu.
    /// Only show when the parameter is set.
    /// </summary>
    [Parameter] public EventCallback<TItem> OnDownload { get; set; }

    /// <summary>
    /// The slot for right action buttons.
    /// </summary>
    [Parameter] public RenderFragment<TItem>? SideActionsContent { get; set; }

    /// <summary>
    /// The slot for top content. Used to display video title, subtitle, etc.
    /// </summary>
    [Parameter] public RenderFragment<TItem>? TopContent { get; set; }

    /// <summary>
    /// The slot for bottom content. Used to display video title, subtitle, etc.
    /// </summary>
    [Parameter] public RenderFragment<TItem>? BottomContent { get; set; }

    /// <summary>
    /// The slot for bottom sheet actions. Accepts the <see cref="MListItem"/> components.
    /// </summary>
    [Parameter] public RenderFragment<BottomActionContext<TItem>>? BottomActionsContent { get; set; }

    private static readonly Block Block = new("m-video-feeder");
    private readonly ModifierBuilder _blockBuilder = Block.CreateModifierBuilder();

    private Video<TItem>? _prevVideo;
    private MSwiper? _swiper;
    private bool _sheet;
    private int _index;
    private bool _globalMuted = true;
    private bool _fullscreen;
    private StringNumber? _playbackRate = 1.0;
    private bool _autoplayNext;
    private List<Video<TItem>> _videos = [];
    private bool _itemsHasSet;

    private Video<TItem>? CurrentVideo => _index >= 0 && _index < _videos.Count ? _videos[_index] : null;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _autoplayNext = DefaultAutoplayNext;
        UpdateVideos();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UpdateVideos();
    }

    private void UpdateVideos()
    {
        // As the Swiper has a virtual list feature, that cannot be incrementally updated,
        // here we limit the setting to only once.
        // TODO: Consider a better way to handle this, update Swiper component.
        if (!_itemsHasSet && Items.Count > 0)
        {
            _itemsHasSet = true;
            _videos = Items.Select(i => new Video<TItem>(i, ItemUrl!, ItemPoster!)).ToList();
        }
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _blockBuilder.Add("rotate-fullscreen", RotateFullscreen && _fullscreen).Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        yield return CssStyleUtils.GetHeight(Height);
        yield return CssStyleUtils.GetWidth(Width);
    }

    private async Task IndexChanged(int index)
    {
        _prevVideo ??= _videos.ElementAtOrDefault(_index);

        _index = index;
        _playbackRate = 1;

        await OnIndexUpdated();
    }

    private async Task OnIndexUpdated()
    {
        if (_prevVideo is not null)
        {
            _prevVideo.Playing = false;
            _prevVideo.Muted = true;
            await _prevVideo.Player!.SetMuteAsync(true);
            await _prevVideo.Player.SetPlayingAsync(false);
        }

        var video = _videos.ElementAtOrDefault(_index);
        _prevVideo = video;
        if (video is null)
        {
            return;
        }

        video.Playing = true;
        video.Muted = _globalMuted;
        await video.Player!.SetMuteAsync(_globalMuted);
        await video.Player.SetPlayingAsync(true);
    }

    private async Task HandleOnFullscreen(FullscreenEventArgs<TItem> args)
    {
        await OnFullscreen.InvokeAsync(args);
        _fullscreen = args.IsFullscreen;
        await _swiper.InvokeVoidAsync(args.IsFullscreen ? "disable" : "enable");
    }

    private async Task HandleOnEnded()
    {
        if (!_autoplayNext || _index >= Items.Count - 1)
        {
            return;
        }

        _index++;
        await _swiper.InvokeVoidAsync("slideTo", _index);
        await OnIndexUpdated();
    }

    private void OpenSheet() => _sheet = true;

    private void CloseSheet() => _sheet = false;

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