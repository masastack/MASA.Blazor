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
    private object _prevOption;
    private string _prevComputedTheme;

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

            if (Light)
            {
                return "light";
            }

            if (CascadingIsDark)
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

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        InitOptions?.Invoke(DefaultInitOptions);

        DefaultInitOptions.Locale ??= I18n.Culture.TwoLetterISOLanguageName.ToUpperInvariant();

        if (_prevComputedTheme != ComputedTheme)
        {
            _prevComputedTheme = ComputedTheme;

            await DisposeECharts();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsDisposed)
        {
            return;
        }

        if (firstRender)
        {
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
        _echarts = null;
    }

    public async Task ReinitializeECharts()
    {
        await DisposeECharts();
        NextTick(async () => { await InitECharts(); });
    }

    public async Task SetOption(object option = null, bool notMerge = true, bool lazyUpdate = false)
    {
        if (_echarts == null) return;
        await Module.SetOption(_echarts, option ?? Option, notMerge, lazyUpdate);
    }

    public async Task Resize(double width, double height)
    {
        if (_echarts == null) return;
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
