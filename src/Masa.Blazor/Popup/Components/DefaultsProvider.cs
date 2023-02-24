using Masa.Blazor.Popup.Components;
using Masa.Blazor.Presets.EnqueuedSnackbars;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDefaultsProvider : ComponentBase
{
    [Parameter] public Action<SnackbarParameters> SnackbarPopup { get; set; }

    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<MDefaultsProvider>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<MDefaultsProvider>.IsFixed), true);
        builder.AddAttribute(2, nameof(CascadingValue<MDefaultsProvider>.Value), this);
        builder.AddContent(3, ChildContent);
        builder.CloseComponent();
    }
}
