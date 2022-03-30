using Masa.Blazor.Doc.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Highlight
{
    public interface IPrismHighlighter
    {
        Task HighlightAsync(string code, string language, DotNetObjectReference<HighlightedCode> objectReference, ElementReference codeElement);

        Task HighlightAllAsync();
    }
}