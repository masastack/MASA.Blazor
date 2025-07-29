using System.Text;

namespace Masa.Blazor;

public partial class MSyntaxHighlight : MasaComponentBase
{
    [Parameter, EditorRequired] public string? Code { get; set; }

    [Parameter] public string? Language { get; set; }

    [Parameter] public bool Inline { get; set; }

    [Parameter] public bool IgnorePreCssOfTheme { get; set; }

    [Parameter] public bool IgnoreCodeCssOfTheme { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnHighlighted { get; set; }

    private bool _firstRender = true;
    private string _codeHtml = string.Empty;
    private string? _prevCode;
    private IJSObjectReference? _importJSObjectReference;

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

        _importJSObjectReference = await Js.InvokeAsync<IJSObjectReference>("import",
            "./_content/Masa.Blazor.JSComponents.MarkdownIt/markdown-it.js");

        _codeHtml = await Highlight(Code, Language, streaming: false) ?? String.Empty;

        if (OnHighlighted != null)
        {
            NextTick(async () => { await OnHighlighted.Invoke(Ref); });
        }
    }

    private async ValueTask<string?> Highlight(string code, string? language, bool streaming = false)
    {
        if (streaming)
        {
            var dataReference =
                await _importJSObjectReference.TryInvokeAsync<IJSStreamReference>("highlightToStream", code, language);
            await using var dataReferenceStream = await dataReference.OpenReadStreamAsync();
            using var reader = new StreamReader(dataReferenceStream);
            return await reader.ReadToEndAsync();
        }

        return await _importJSObjectReference.TryInvokeAsync<string>("highlight", code, language);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_importJSObjectReference is not null)
        {
            await _importJSObjectReference.DisposeAsync();
            _importJSObjectReference = null;
        }
    }
}