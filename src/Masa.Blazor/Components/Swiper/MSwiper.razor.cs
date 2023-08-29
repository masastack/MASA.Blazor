namespace Masa.Blazor;

public partial class MSwiper
{
    [Inject] private SwiperJsModule SwiperJsModule { get; set; } = null!;

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private ElementReference _elementReference;

    private MSwiperPagination? _pagination;
    private MSwiperNavigation? _navigation;

    private CancellationTokenSource _cancellationTokenSource = new();

    private Block Block => new("m-swiper");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
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
        }

        StateHasChanged();

        await InitSwiperAsync();
    }

    private async Task InitSwiperAsync()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        await Task.Delay(16, _cancellationTokenSource.Token);

        var rootSelector = _elementReference.GetSelector();

        SwiperOptions options = new()
        {
            Pagination = _pagination?.GetOptions($"{rootSelector} .swiper-pagination"),
            Navigation = _navigation?.GetOptions($"{rootSelector} .swiper-button-next", $"{rootSelector} .swiper-button-prev")
        };

        await SwiperJsModule.Init(_elementReference, options);
    }
}
