﻿using Masa.Blazor.Extensions;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MSwiper : MasaComponentBase
{
    [Inject] private SwiperJsModule SwiperJsModule { get; set; } = null!;

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public bool AutoHeight { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool AllowTouchMove { get; set; } = true;

    /// <summary>
    /// Set to true to enable continuous loop mode
    /// </summary>
    [Parameter] public bool Loop { get; set; }

    /// <summary>
    /// Object with parallax parameters or boolean true
    /// to enable with default settings.
    /// </summary>
    [Parameter] public bool Parallax { get; set; }

    /// <summary>
    /// Distance between slides in px.
    /// </summary>
    [Parameter] public int SpaceBetween { get; set; }

    /// <summary>
    /// Duration of transition between slides (in ms)
    /// </summary>
    [Parameter] [MasaApiParameter(300)] public int Speed { get; set; } = 300;

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public int Index { get; set; }

    [Parameter] public EventCallback<int> IndexChanged { get; set; }

    private ElementReference _elementReference;
    private DotNetObjectReference<object>? _swiperInteropHandle;
    private ISwiperJSObjectReferenceProxy? _swiperProxy;
    private bool _isJsInteropAndRefReady;

    private int _prevIndex;

    private MSwiperPagination? _pagination;
    private MSwiperNavigation? _navigation;
    private MSwiperAutoplay? _autoplay;

    private CancellationTokenSource _cancellationTokenSource = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _swiperInteropHandle = DotNetObjectReference.Create<object>(new SwiperInteropHandle(this));
    }

    private static Block _block = new("m-stepper");
    private static Block _paginationBlock = _block.Extend("pagination");
    private static Block _nextBlock = _block.Extend("next");
    private static Block _prevBlock = _block.Extend("prev");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.AppendClasses("swiper");
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return StyleBuilder.Create().AddHeight(Height).AddWidth(Width).Build();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevIndex != Index)
        {
            _prevIndex = Index;

            if (_swiperProxy is null)
            {
                return;
            }

            await _swiperProxy.SlideToAsync(Index, Speed);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _isJsInteropAndRefReady = true;
            await InitSwiperAsync();
        }
    }

    internal async Task AddModuleAsync(object module)
    {
        switch (module)
        {
            case MSwiperPagination pagination:
                _pagination = pagination;
                break;
            case MSwiperNavigation navigation:
                _navigation = navigation;
                break;
            case MSwiperAutoplay autoplay:
                _autoplay = autoplay;
                break;
        }

        StateHasChanged();

        await InitSwiperAsync();
    }

    private async Task InitSwiperAsync()
    {
        if (!_isJsInteropAndRefReady)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        await RunTaskInMicrosecondsAsync(async () =>
        {
            var rootSelector = _elementReference.GetSelector();

            SwiperOptions options = new()
            {
                AllowTouchMove = AllowTouchMove,
                AutoHeight = AutoHeight,
                Direction = Vertical ? "vertical" : "horizontal",
                Loop = Loop,
                Parallax = Parallax,
                SpaceBetween = SpaceBetween,
                Autoplay = _autoplay?.GetOptions(),
                Pagination = _pagination?.GetOptions($"{rootSelector} .swiper-pagination"),
                Navigation = _navigation?.GetOptions($"{rootSelector} .swiper-button-next", $"{rootSelector} .swiper-button-prev")
            };

            _swiperProxy = await SwiperJsModule.Init(_elementReference, options, _swiperInteropHandle);
        }, 16, _cancellationTokenSource.Token);
    }

    internal async Task UpdateIndexAsync(int index)
    {
        Index = index;
        _prevIndex = index;
        await IndexChanged.InvokeAsync(index);
    }

    [MasaApiPublicMethod]
    public async Task SlideToAsync(int index, int? speed = null)
    {
        if (_swiperProxy == null) return;

        await _swiperProxy!.SlideToAsync(index, speed ?? Speed);
    }

    [MasaApiPublicMethod]
    public async Task SlideNextAsync(int? speed = null)
    {
        if (_swiperProxy == null) return;
        await _swiperProxy!.SlideNextAsync(speed ?? Speed);
    }

    [MasaApiPublicMethod]
    public async Task SlidePrevAsync(int? speed = null)
    {
        if (_swiperProxy == null) return;
        await _swiperProxy!.SlidePrevAsync(speed ?? Speed);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_swiperProxy != null)
        {
            await _swiperProxy.DisposeAsync();
        }
    }
}
