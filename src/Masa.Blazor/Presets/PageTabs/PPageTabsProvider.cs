#nullable enable

using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageTabsProvider : ComponentBase, IPageTabsProvider
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public Dictionary<string, Func<string?>> PathTitles { get; } = new();

    public EventHandler<string>? TabTitleChanged { get; set; }

    public void RemovePathTitles(params string[] absolutePaths)
    {
        absolutePaths.ForEach(path => PathTitles.Remove(path));
    }

    public void UpdateTabTitle(string absolutePath, Func<string?> titleFunc)
    {
        PathTitles[absolutePath] = titleFunc;

        TabTitleChanged?.Invoke(this, absolutePath);
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
