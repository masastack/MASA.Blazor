namespace Masa.Blazor;

public partial class MECharts : BDomComponentBase, IAsyncDisposable
{
    [Inject]
    protected I18n I18n { get; set; }

    [Inject]
    protected EChartsJSModule Module { get; set; }

    [Parameter]
    public StringNumber Width { get; set; } = "100%";

    [Parameter]
    public StringNumber Height { get; set; } = "100%";

    [Parameter]
    public StringNumber MinWidth { get; set; }

    [Parameter]
    public StringNumber MinHeight { get; set; }

    [Parameter]
    public StringNumber MaxWidth { get; set; }

    [Parameter]
    public StringNumber MaxHeight { get; set; }

    [Parameter]
    public Action<EChartsInitOptions> InitOptions { get; set; }

    [Parameter]
    public object Option { get; set; } = new { };

    [Parameter]
    public bool Light { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public string Theme { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    private EChartsInitOptions DefaultInitOptions { get; set; } = new();

    private IJSObjectReference _echarts;
    private bool _isEChartsDisposed = false;
    private object _prevOption;

    public string ComputedTheme
    {
        get
        {
            if (Theme is not null)
            {
                return Theme;
            }

            if (Dark)
            {
                return "dark";
            }

            return null;
        }
    }

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(styleAction: styleBuilder =>
            {
                styleBuilder
                    .AddWidth(Width)
                    .AddHeight(Height)
                    .AddMinWidth(MinWidth)
                    .AddMinHeight(MinHeight)
                    .AddMaxHeight(MaxHeight)
                    .AddMaxWidth(MaxWidth);
            });
    }

    protected override void OnParametersSet()
    {
        InitOptions?.Invoke(DefaultInitOptions);

        DefaultInitOptions.Locale ??= I18n.Culture.TwoLetterISOLanguageName.ToUpperInvariant();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (IsDisposed)
        {
            return;
        }

        if (firstRender || _isEChartsDisposed)
        {
            _isEChartsDisposed = false;
            await InitECharts();
        }

        if (_prevOption != Option)
        {
            _prevOption = Option;

            if (firstRender) return;

            await SetOption();
        }
    }

    public async Task InitECharts()
    {
        _echarts = await Module.Init(Ref, ComputedTheme, DefaultInitOptions);
        await SetOption();
    }

    public async Task DisposeECharts()
    {
        await Module.Dispose(_echarts);
        _isEChartsDisposed = true;
    }

    public async Task SetOption(object option = null, bool notMerge = true, bool lazyUpdate = false)
    {
        await Module.SetOption(_echarts, option ?? Option, notMerge, lazyUpdate);
    }

    public async Task Resize(int width, int height)
    {
        await Module.Resize(_echarts, width, height);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await DisposeECharts();
            if (_echarts is not null)
            {
                await _echarts.DisposeAsync();
            }
        }
        catch
        {
            // ignored
        }
    }
}
