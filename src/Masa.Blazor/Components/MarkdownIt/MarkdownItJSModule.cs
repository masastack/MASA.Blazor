namespace Masa.Blazor;

public class MarkdownItJSModule : JSModule
{
    public MarkdownItJSModule(IJSRuntime jsRuntime) : base(jsRuntime, "./_content/Masa.Blazor/js/proxies/markdown-it-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Create(MarkdownItOptions options, bool enableHeaderSections = false,
        MarkdownItAnchorOptions? anchorOptions = null, string? scope = null)
        => await InvokeAsync<IJSObjectReference>("create", options, enableHeaderSections, anchorOptions, scope);

    public async ValueTask<string> Parse(IJSObjectReference instance, string source)
        => await InvokeAsync<string>("parse", instance, source);

    public async ValueTask<MarkdownItParsedResult> ParseAll(IJSObjectReference instance, string source)
        => await InvokeAsync<MarkdownItParsedResult>("parseAll", instance, source);

    public async Task AfterRender(IJSObjectReference instance)
        => await InvokeVoidAsync("afterRender", instance);

    /// <summary>
    /// Highlight the code and return markup string in html format.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="language">The language.</param>
    /// <param name="streaming">
    /// Read the streaming data directly instead of string.
    /// TODO: But there is a problem under WASM: https://github.com/dotnet/aspnetcore/issues/45799.
    /// </param>
    /// <returns>A <see cref="ValueTask{TResult}"/> including the markup string in html format</returns>
    public async ValueTask<string> Highlight(string code, string? language, bool streaming = false)
    {
        if (streaming)
        {
            var dataReference = await InvokeAsync<IJSStreamReference>("highlightToStream", code, language);
            await using var dataReferenceStream = await dataReference.OpenReadStreamAsync();
            using var reader = new StreamReader(dataReferenceStream);
            return await reader.ReadToEndAsync();
        }

        return await InvokeAsync<string>("highlight", code, language);
    }
}
