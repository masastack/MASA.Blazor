using System.Text.Json;

namespace Masa.Blazor;

public partial class MECharts : BDomComponentBase, IEChartsJsCallbacks, IAsyncDisposable
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    [Inject]
    protected EChartsJSModule Module { get; set; } = null!;

    [Parameter]
    public StringNumber Width { get; set; } = "100%";

    [Parameter]
    public StringNumber Height { get; set; } = "100%";

    [Parameter]
    public StringNumber? MinWidth { get; set; }

    [Parameter]
    public StringNumber? MinHeight { get; set; }

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public StringNumber? MaxHeight { get; set; }

    [Parameter]
    public Action<EChartsInitOptions>? InitOptions { get; set; }

    [Parameter]
    public object Option { get; set; } = new { };

    [Parameter]
    public bool Light { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public string? Theme { get; set; }

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

    [Parameter]
    public bool IncludeFunctionsInOption { get; set; }

    private static readonly Regex s_functionRegex
        = new(@"""\s*function\s?\([a-zA-Z][a-zA-Z0-9,\s]*\)\s?\{((?<BR>\{)|(?<-BR>\})|[^{}]*)+\}\s*""", RegexOptions.IgnoreCase);

    private static readonly Regex s_lambdaRegex
        = new(@"""\([a-zA-Z]?[a-zA-Z0-9\s,]*\)\s?=>\s?[a-zA-Z{}`][^""]+""", RegexOptions.IgnoreCase);

    private EChartsInitOptions DefaultInitOptions { get; set; } = new();

    private IEChartsJSObjectReferenceProxy? _echarts;
    private object? _prevOption;
    private string? _prevComputedTheme;

    public string? ComputedTheme
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

    public async Task SetOption(object? option = null, bool notMerge = true, bool lazyUpdate = false)
    {
        if (_echarts == null) return;

        option ??= Option;

        if (IncludeFunctionsInOption && IsAnyFunction(option, out var optionJson))
        {
            // unescape verbatim text
            optionJson = Regex.Unescape(optionJson);

            // remove the double quotes around the function
            optionJson = FormatFunction(optionJson);

            // remove the double quotes around the lambda
            optionJson = FormatLambda(optionJson);

            // convert unicode to string
            optionJson = Unicode2String(optionJson);

            await _echarts.SetJsonOptionAsync(optionJson, notMerge, lazyUpdate);
        }
        else
        {
            try
            {
                await _echarts.SetOptionAsync(option, notMerge, lazyUpdate);
            }
            catch (JSException e)
            {
                if (e.Message.Contains("not a function"))
                {
                    throw new JSException("Are you trying to use a function in the option? " +
                                          "If so, please set the IncludeFunctionsInOption property to true. " +
                                          "If not, please check the option.", e);
                }

                throw;
            }
        }
    }

    public static string Unicode2String(string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
            source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
    }

    public static bool IsAnyFunction(object option, out string optionJson)
    {
        if (option is null)
        {
            optionJson = string.Empty;
            return false;
        }

        optionJson = JsonSerializer.Serialize(option, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // check if the option contains "function" or "=>"
        return optionJson.Contains("function") || optionJson.Contains("=\\u003e", StringComparison.OrdinalIgnoreCase);
    }

    public static string FormatFunction(string optionJson)
    {
        return s_functionRegex.Replace(optionJson, (m) => m.Value.Trim().Substring(1, m.Value.Length - 2));
    }

    public static string FormatLambda(string optionJson)
    {
        return s_lambdaRegex.Replace(optionJson, m => m.Value.Trim().Substring(1, m.Value.Length - 2));
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

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await DisposeECharts();

            if (_echarts is not null)
            {
                await _echarts.DisposeAsync();
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
