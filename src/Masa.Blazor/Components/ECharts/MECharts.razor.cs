using System.Text.Json;

namespace Masa.Blazor;

public partial class MECharts : BDomComponentBase, IEChartsJsCallbacks, IAsyncDisposable
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

    [Parameter]
    public EventCallback<EChartsEventArgs> OnClick { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnDoubleClick { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnMouseDown { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnMouseMove { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnMouseUp { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnMouseOver { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnMouseOut { get; set; }

    [Parameter]
    public EventCallback OnGlobalOut { get; set; }

    [Parameter]
    public EventCallback<EChartsEventArgs> OnContextMenu { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    private EChartsInitOptions DefaultInitOptions { get; set; } = new();

    private IEChartsJSObjectReferenceProxy _echarts;
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

            await ReinitializeECharts();
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
        _echarts = await Module.Init(Ref, ComputedTheme, DefaultInitOptions, this);

        await SetOption();
    }

    public async Task DisposeECharts()
    {
        if (_echarts == null) return;

        await _echarts.DisposeEChartsAsync();

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

        option ??= Option;
        if (IsAnyFunction(option, out string optionJson))
        {
            optionJson = FormatterFunction(optionJson);
            await _echarts.SetOptionStrAsync(optionJson, notMerge, lazyUpdate);
        }
        else
        {
            await _echarts.SetOptionAsync(option, notMerge, lazyUpdate);
        }
    }

    public static bool IsAnyFunction(object option, out string optionJson)
    {
        if (option is null)
        {
            optionJson = string.Empty;
            return false;
        }
        optionJson = JsonSerializer.Serialize(option);
        return optionJson.Contains("function");
    }

    public static string FormatterFunction(string optionJson)
    {
        string pattern = @":\s*""\s*function\s?\(.*\)\s?\{.+\}[\s;]?""[\n\s]?";
        var regex = new Regex(pattern);

        string newOptionJson = regex.Replace(optionJson,
            (m) =>
            {
                string newValue = m.Value.Trim()
                    .Substring(2, m.Value.Length - 3)
                    .Replace("\\u0022", "\"");
                newValue = $" : {newValue}";
                return newValue;
            });
        return newOptionJson;
    }

    public async Task Resize(double width = 0, double height = 0)
    {
        if (_echarts == null) return;

        if (width == 0 || height == 0)
        {
            await _echarts.ResizeAsync();
        }
        else
        {
            await _echarts.ResizeAsync(width, height);
        }
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
