namespace Masa.Blazor;

public class MScrollToTargetItem : BDomComponentBase
{
    [CascadingParameter] private MScrollToTarget? ScrollToTarget { get; set; }

    [Parameter] [EditorRequired] public override string Id { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override bool GenerateComponentId => false;

    protected override async Task OnInitializedAsync()
    {
        ScrollToTarget?.RegisterTarget(Id);

        await base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Id.ThrowIfNull(ComponentName);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", CssProvider.GetClass());
        builder.AddAttribute(2, "style", CssProvider.GetStyle());
        builder.AddAttribute(3, "id", Id);
        builder.AddContent(4, ChildContent);
        builder.CloseElement();
    }

    protected override void SetComponentCss()
    {
        base.SetComponentCss();

        CssProvider.UseBem("m-scroll-to-target-item");
    }

    protected override ValueTask DisposeAsync(bool disposing)
    {
        ScrollToTarget?.UnregisterTarget(Id);
        return base.DisposeAsync(disposing);
    }
}
