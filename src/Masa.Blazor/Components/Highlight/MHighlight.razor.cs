#nullable enable

namespace Masa.Blazor;

public partial class MHighlight : BDomComponentBase
{
    [Inject]
    protected MarkdownItJSModule MarkdownItJSModule { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public string? Code { get; set; }

    [Parameter]
    [EditorRequired]
    public string? Language { get; set; }

    [Parameter]
    public bool Inline { get; set; }

    [Parameter]
    public bool IgnorePreCssOfTheme { get; set; }

    [Parameter]
    public bool IgnoreCodeCssOfTheme { get; set; }

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

        if (_prevCode is not null && _prevCode != Code)
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
            _prevCode = Code;

            await TryHighlight();

            StateHasChanged();
        }
    }

    private async Task TryHighlight()
    {
        if (Code == null) return;

        _codeHtml = await MarkdownItJSModule.Highlight(Code, Language);
    }
}
