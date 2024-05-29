using Microsoft.Extensions.Primitives;

namespace Masa.Blazor;

public partial class MSyntaxHighlight : MasaComponentBase
{
    [Inject] protected MarkdownItJSModule MarkdownItJSModule { get; set; } = null!;

    [Parameter, EditorRequired] public string? Code { get; set; }

    [Parameter] public string? Language { get; set; }

    [Parameter] public bool Inline { get; set; }

    [Parameter] public bool IgnorePreCssOfTheme { get; set; }

    [Parameter] public bool IgnoreCodeCssOfTheme { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnHighlighted { get; set; }

    private bool _firstRender = true;
    private string _codeHtml = string.Empty;
    private string? _prevCode;

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "hljs m-code-highlight";

        if (Language != null && !IgnorePreCssOfTheme)
        {
            yield return $"language-{Language.ToLower()}";
        }
    }

    private string GetCodeClass()
    {
        StringBuilder stringBuilder = new("m-code-highlight__code");
        if (Language != null && !IgnoreCodeCssOfTheme)
        {
            stringBuilder.Append($" language-{Language.ToLower()}");
        }

        if (Inline && !string.IsNullOrWhiteSpace(Class))
        {
            stringBuilder.Append($" {Class}");
        }

        return stringBuilder.ToString();
    }

    private string? GetCodeStyle()
    {
        return Inline ? Style : null;
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