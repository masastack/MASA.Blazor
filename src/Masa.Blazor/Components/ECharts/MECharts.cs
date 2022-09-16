using BlazorComponent.Web;

namespace Masa.Blazor;

public class MECharts : BECharts, IAsyncDisposable
{
    [Inject]
    protected I18n I18n { get; set; }

    [Parameter]
    public StringNumber Width { get; set; } = 600;

    [Parameter]
    public StringNumber Height { get; set; } = 400;

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

        if (firstRender)
        {
            _echarts = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/echarts-helper.js");
        }

        if (firstRender || _isEChartsDisposed)
        {
            _isEChartsDisposed = false;
            await _echarts.InvokeVoidAsync("init", Ref.GetSelector(), ComputedTheme, DefaultInitOptions, Option);
        }

        if (_prevOption != Option)
        {
            _prevOption = Option;

            if (firstRender) return;

            await ResetOption();
        }
    }

    public async Task DisposeECharts()
    {
        await _echarts.InvokeVoidAsync("dispose", Ref.GetSelector());
        _isEChartsDisposed = true;
    }

    public async Task ResetOption()
    {
        await _echarts.InvokeVoidAsync("setOption", Ref.GetSelector(), Option);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await DisposeECharts();
            await _echarts.DisposeAsync();
        }
        catch
        {
            // ignored
        }
    }
}
