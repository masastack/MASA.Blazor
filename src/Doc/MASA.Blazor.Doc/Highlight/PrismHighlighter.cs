using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MASA.Blazor.Doc.Shared;

namespace MASA.Blazor.Doc.Highlight
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