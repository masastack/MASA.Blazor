using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageTabsProvider : ComponentBase, IPageTabsProvider
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public Dictionary<string, Func<string?>> PathTitles { get; } = new();

    public EventHandler<string>? TabTitleChanged { get; set; }

    internal void RemovePathTitles(params string[] absolutePaths)
    {
        absolutePaths.ForEach(path => PathTitles.Remove(path));
    }

    internal void RemoveAllPathTitles()
    {
        PathTitles.Clear();
    }

    [MasaApiPublicMethod]
    public void UpdateTabTitle(string absolutePath, Func<string?> titleFunc)
    {
        PathTitles[absolutePath] = titleFunc;

        TabTitleChanged?.Invoke(this, absolutePath);
    }

    [MasaApiPublicMethod]
    public void UpdateTabTitle(string absolutePath, string? title) 
        => UpdateTabTitle(absolutePath, () => title);

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<PPageTabsProvider>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<PPageTabsProvider>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<PPageTabsProvider>.IsFixed), true);
        builder.AddAttribute(3, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
