namespace Masa.Blazor;

public partial class MSyntaxHighlight : BDomComponentBase
{
    [Inject]
    protected MarkdownItJSModule MarkdownItJSModule { get; set; } = null!;

    [Parameter, EditorRequired]
    public string? Code { get; set; }

    [Parameter]
    public string? Language { get; set; }

    [Parameter]
    public bool Inline { get; set; }

    [Parameter]
    public bool IgnorePreCssOfTheme { get; set; }

    [Parameter]
    public bool IgnoreCodeCssOfTheme { get; set; }

    [Parameter]
    public Func<ElementReference, Task>? OnHighlighted { get; set; }

    private bool _firstRender = true;
    private string _codeHtml = string.Empty;
    private string? _prevCode;

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(css =>
            {
                css.Add("hljs m-code-highlight__pre")
                   .AddIf($"language-{Language!.ToLower()}", () => Language is not null && !IgnorePreCssOfTheme);
            }).Apply("code", css =>
            {
                css.Add("m-code-highlight__code")
                   .AddIf($"language-{Language!.ToLower()}", () => Language is not null && !IgnoreCodeCssOfTheme)
                   .AddIf(Class, () => Inline);
            }, style => style.AddIf(Style, () => Inline));
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (!_firstRender && _prevCode != Code)
        {
            _prevCode = Code;

            await TryHighlight();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _firstRender = false;

            _prevCode = Code;

            await TryHighlight();

            StateHasChanged();
        }
    }

    private async Task TryHighlight()
    {
        if (Code == null)
        {
            _codeHtml = string.Empty;
            return;
        }

        _codeHtml = await MarkdownItJSModule.Highlight(Code, Language, streaming: false);

        if (OnHighlighted != null)
        {
            NextTick(async () => { await OnHighlighted.Invoke(Ref); });
        }
    }
}
