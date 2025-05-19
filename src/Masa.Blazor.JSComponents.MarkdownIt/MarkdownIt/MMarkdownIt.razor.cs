namespace Masa.Blazor;

public partial class MMarkdownIt : MasaComponentBase
{
    /// <summary>
    /// Enable markdown-it-header-sections plugin.
    /// </summary>
    [Parameter]
    public bool HeaderSections { get; set; }

    [Parameter, EditorRequired]
    public string? Source { get; set; }

    [Parameter]
    public string? Scope { get; set; }

    #region MarkdownItOptions

    /// <summary>
    /// Enable HTML tags in a source
    /// </summary>
    [Parameter]
    public bool Html { get; set; }

    /// <summary>
    /// Use '/' to close single tags (<br />).
    /// This is only for full CommonMark compatibility.
    /// </summary>
    [Parameter]
    public bool XHtmlOut { get; set; }

    /// <summary>
    /// Convert '\n' in paragraphs into br tag.
    /// </summary>
    [Parameter]
    public bool Breaks { get; set; }

    /// <summary>
    /// CSS language prefix for fenced blocks.
    /// Can be useful for external highlighters.
    /// </summary>
    [Parameter]
    [MasaApiParameter("language-")]
    public string? LangPrefix { get; set; } = "language-";

    /// <summary>
    /// Autoconvert URL-like text to links
    /// </summary>
    [Parameter]
    public bool Linkify { get; set; }

    /// <summary>
    /// Enable some language-neutral replacement + quotes beautification
    /// </summary>
    [Parameter]
    public bool Typographer { get; set; }

    /// <summary>
    /// Double + single quotes replacement pairs,
    /// when typographer enabled, and smartquotes on. 
    /// </summary>
    [Parameter]
    [MasaApiParameter("[\"“\", \"”\", \"‘\", \"’\"]")]
    public string[]? Quotes { get; set; } = ["“", "”", "‘", "’"];

    #endregion

    /// <summary>
    /// Determines whether to wrap it with elements
    /// </summary>
    [Parameter]
    public bool NoWrapper { get; set; }

    /// <summary>
    /// A callback that will be invoked when the <see cref="Source"/> rendered.
    /// </summary>
    [Parameter]
    public EventCallback<string?> OnFrontMatterParsed { get; set; }

    /// <summary>
    /// Enable markdown-it-anchor plugin.
    /// </summary>
    [Parameter]
    public MarkdownItAnchorOptions? AnchorOptions { get; set; }

    /// <summary>
    /// A callback that will be invoked when the <see cref="AnchorOptions"/> was instantiated and the <see cref="Source"/> rendered.
    /// </summary>
    [Parameter]
    public EventCallback<List<MarkdownItTocContent>?> OnTocParsed { get; set; }

    [Parameter]
    public EventCallback OnAfterRendered { get; set; }

    private string _mdHtml = string.Empty;

    private string? _prevSource;
    private IJSObjectReference? _markdownIt;
    private IJSObjectReference? _importJSObjectReference;
    private bool _prevHtml;
    private bool _prevXHtmlOut;
    private bool _prevBreaks;
    private string? _prevLangPrefix;
    private bool _prevLinkify;
    private bool _prevTypographer;
    private string[]? _prevQuotes;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        _prevHtml = Html;
        _prevXHtmlOut = XHtmlOut;
        _prevBreaks = Breaks;
        _prevLangPrefix = LangPrefix;
        _prevLinkify = Linkify;
        _prevTypographer = Typographer;
        _prevQuotes = Quotes;
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-markdown-it";
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var needsRecreate = false;
        var needsParse = false;
        
        if (_prevSource != Source)
        {
            _prevSource = Source;
            await TryParse();
        }

        if (_prevHtml != Html)
        {
            _prevHtml = Html;
            needsRecreate = true;
        }
        
        if (_prevXHtmlOut != XHtmlOut)
        {
            _prevXHtmlOut = XHtmlOut;
            needsRecreate = true;
        }
        
        if (_prevBreaks != Breaks)
        {
            _prevBreaks = Breaks;
            needsRecreate = true;
        }
        
        if (_prevLangPrefix != LangPrefix)
        {
            _prevLangPrefix = LangPrefix;
            needsRecreate = true;
        }
        
        if (_prevLinkify != Linkify)
        {
            _prevLinkify = Linkify;
            needsRecreate = true;
        }
        
        if (_prevTypographer != Typographer)
        {
            _prevTypographer = Typographer;
            needsRecreate = true;
        }
        
        if (_prevQuotes != Quotes)
        {
            _prevQuotes = Quotes;
            needsRecreate = true;
        }

        if (needsRecreate)
        {
            await CreateMarkdownItProxy();
            NextTick(TryParse);
        }
        else if (needsParse)
        {
            await TryParse();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await CreateMarkdownItProxy();
            await TryParse();
        }
    }

    private async Task CreateMarkdownItProxy()
    {
        _importJSObjectReference ??= await Js.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.JSComponents.MarkdownIt/markdown-it.js")
            .ConfigureAwait(false);

        var options = new MarkdownItOptions()
        {
            Html = Html,
            XHtmlOut = XHtmlOut,
            Breaks = Breaks,
            LangPrefix = LangPrefix,
            Linkify = Linkify,
            Typographer = Typographer,
            Quotes = Quotes
        };

        if (_markdownIt is not null)
        {
            await _markdownIt.DisposeAsync();
        }

        _markdownIt =
            await _importJSObjectReference.InvokeAsync<IJSObjectReference>("create", options, HeaderSections,
                AnchorOptions, Scope);
    }

    private async Task TryParse()
    {
        if (_markdownIt is null) return;

        if (Source == null)
        {
            _mdHtml = string.Empty;
        }
        else
        {
            var result = await ParseAll(_markdownIt, Source);

            if (result is null) return;

            if (OnFrontMatterParsed.HasDelegate)
            {
                await OnFrontMatterParsed.InvokeAsync(result.FrontMatter);
            }

            _mdHtml = result.MarkupContent ?? string.Empty;

            if (OnTocParsed.HasDelegate)
            {
                await OnTocParsed.InvokeAsync(result.Toc);
            }
        }

        NextTick(async () =>
        {
            await AfterRender(_markdownIt);

            if (OnAfterRendered.HasDelegate)
            {
                await OnAfterRendered.InvokeAsync();
            }
        });

        StateHasChanged();
    }

    private async ValueTask<string?> Parse(IJSObjectReference instance, string source)
        => await _importJSObjectReference.TryInvokeAsync<string>("parse", instance, source);

    private async ValueTask<MarkdownItParsedResult?> ParseAll(IJSObjectReference instance, string source)
        => await _importJSObjectReference.TryInvokeAsync<MarkdownItParsedResult>("parseAll", instance, source);

    private async Task AfterRender(IJSObjectReference instance)
        => await _importJSObjectReference.TryInvokeVoidAsync("afterRender", instance);
}