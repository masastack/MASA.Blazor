#nullable enable

using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageTabsProvider : ComponentBase, IPageTabsProvider
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public Dictionary<string, string> PathTitles { get; } = new();

    public void UpdateTabTitle(string absolutePath, string title)
    {
        PathTitles[absolutePath] = title;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<IPageTabsProvider>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<IPageTabsProvider>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<IPageTabsProvider>.IsFixed), true);
        builder.AddAttribute(3, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
