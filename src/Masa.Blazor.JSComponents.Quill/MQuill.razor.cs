using Masa.Blazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public partial class MQuill : DisposableComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public RenderFragment? ToolbarContent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var importJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.JSComponents.Quill/MQuill.razor.js").ConfigureAwait(false);

        }

        await base.OnAfterRenderAsync(firstRender);
    }
}