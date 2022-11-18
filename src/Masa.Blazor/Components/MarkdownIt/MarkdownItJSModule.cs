namespace Masa.Blazor;

public class MarkdownItJSModule : JSModule
{
    public MarkdownItJSModule(IJSRuntime jsRuntime) : base(jsRuntime, "./_content/Masa.Blazor/js/proxies/markdown-it-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Create(MarkdownItOptions options, bool enableHeaderSections = false,
        MarkdownItAnchorOptions? anchorOptions = null, string scope = null)
        => await InvokeAsync<IJSObjectReference>("create", options, enableHeaderSections, anchorOptions, scope);

    public async ValueTask<string> Parse(IJSObjectReference instance, string source) 
        => await InvokeAsync<string>("parse", instance, source);

    public async ValueTask<MarkdownItParsedResult> ParseAll(IJSObjectReference instance, string source) 
        => await InvokeAsync<MarkdownItParsedResult>("parseAll", instance, source);

    public async ValueTask<string> Highlight(string code, string language)
        => await InvokeAsync<string>("highlight", code, language);
}
