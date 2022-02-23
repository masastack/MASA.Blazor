using Masa.Blazor.Doc.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Highlight
{
    public class PrismHighlighter : IPrismHighlighter
    {
        private readonly IJSRuntime jsRuntime;

        public PrismHighlighter(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task HighlightAsync(string code, string language, DotNetObjectReference<HighlightedCode> objectReference, ElementReference codeElement)
        {
            await jsRuntime.InvokeVoidAsync("BlazorComponent.Prism.highlight", code, language, objectReference, codeElement);
        }

        public async Task HighlightAllAsync()
        {
            await jsRuntime.InvokeVoidAsync("BlazorComponent.Prism.highlightAll");
        }
    }
}