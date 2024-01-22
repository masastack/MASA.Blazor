namespace Masa.Blazor;

public class MScrollToTargetTrigger : BDomComponentBase
{
    [CascadingParameter] private MScrollToTarget? ScrollToTarget { get; set; }

    [Parameter] [MasaApiParameter("div")] public string Tag { get; set; } = "div";

    [Parameter] [EditorRequired] public string Target { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool PreventDefaultOnClick { get; set; }

    [Parameter] public bool StopPropagationOnClick { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ScrollToTarget?.RegisterTarget(Target);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Target.ThrowIfNull(ComponentName);

        Tag ??= "div";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, Tag);
        builder.AddAttribute(1, "class", CssProvider.GetClass());
        builder.AddAttribute(2, "style", CssProvider.GetStyle());
        builder.AddAttribute(3, "id", Id);

        if (ScrollToTarget is not null)
        {
            builder.AddAttribute(4, "onclick", () => ScrollToTarget.ScrollToTarget(Target));
            builder.AddAttribute(5, "__internal_preventDefault_onclick", PreventDefaultOnClick);
            builder.AddAttribute(6, "__internal_stopPropagation_onclick", StopPropagationOnClick);
        }

        builder.AddMultipleAttributes(7, Attributes);
        builder.AddContent(8, ChildContent);
        builder.CloseElement();
    }

    protected override void SetComponentCss()
    {
        base.SetComponentCss();

        CssProvider.UseBem("m-scroll-to-target-trigger",
            css => { css.AddIf(ScrollToTarget?.ActiveClass, () => ScrollToTarget != null && ScrollToTarget.ActiveTarget == Target); });
    }

    protected override ValueTask DisposeAsync(bool disposing)
    {
        ScrollToTarget?.UnregisterTarget(Target);
        return base.DisposeAsync(disposing);
    }
}
