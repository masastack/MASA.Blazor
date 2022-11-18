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
    public string Language { get; set; } = null!;

    [Parameter]
    public bool Inline { get; set; }

    [Parameter]
    public bool IgnorePreCssOfTheme { get; set; }

    [Parameter]
    public bool IgnoreCodeCssOfTheme { get; set; }

    private string _codeHtml = string.Empty;
    private string? _prevCode;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Language);
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(css =>
            {
                css.Add("m-code-highlight__pre")
                   .AddIf($"language-{Language.ToLower()}", () => !IgnorePreCssOfTheme);
            }).Apply("code", css =>
            {
                css.Add("m-code-highlight__code")
                   .AddIf($"language-{Language.ToLower()}", () => !IgnoreCodeCssOfTheme);
            });
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevCode != Code)
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
            await TryHighlight();
        }
    }

    private async Task TryHighlight()
    {
        if (Code == null) return;

        _codeHtml = await MarkdownItJSModule.Highlight(Code, Language);

        StateHasChanged();
    }
}
