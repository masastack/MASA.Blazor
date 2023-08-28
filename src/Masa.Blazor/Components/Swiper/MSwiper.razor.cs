namespace Masa.Blazor;

public partial class MSwiper
{
    [Inject] private SwiperJsModule SwiperJsModule { get; set; } = null!;

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private readonly Dictionary<string, object> _modules = new();

    private readonly SwiperOptions _swiperOptions = new();

    private ElementReference _elementReference;

    private CancellationTokenSource _cancellationTokenSource = new();

    internal async Task AddModuleAsync(string module, object options)
    {
        if (module == "pagination")
        {
            _swiperOptions.Pagination = options;
        }

        await Task.Delay(1000);
        await InitSwiperAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // await InitSwiperAsync();
        }
    }

    private async Task InitSwiperAsync()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        await Task.Delay(1000, _cancellationTokenSource.Token);
        Console.Out.WriteLine("InitSwiperAsync...");
        await SwiperJsModule.Init(_elementReference, _swiperOptions);
    }
}
