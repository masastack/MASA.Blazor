using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MASA.Blazor.Doc.Shared;

namespace MASA.Blazor.Doc.Highlight
{
    public interface IPrismHighlighter
    {
        Task HighlightAsync(string code, string language, DotNetObjectReference<HighlightedCode> objectReference,ElementReference codeElement);

        Task HighlightAllAsync();
    }
}