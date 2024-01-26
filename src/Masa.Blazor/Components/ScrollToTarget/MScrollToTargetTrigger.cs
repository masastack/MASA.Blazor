using Masa.Blazor.Components.ScrollToTarget;
using Masa.Blazor.ScrollToTarget;

namespace Masa.Blazor;

public class MScrollToTargetTrigger : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [CascadingParameter] private MScrollToTarget? ScrollToTarget { get; set; }

    [Parameter] [EditorRequired] public string Target { get; set; } = null!;

    [Parameter] public RenderFragment<TargetTriggerContext>? ChildContent { get; set; }

    private TargetTriggerContext _context = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            ScrollToTarget?.RegisterTarget(Target);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Target.ThrowIfNull(nameof(MScrollToTargetTrigger));

        UpdateTargetTriggerContext();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
        => builder.AddContent(0, ChildContent?.Invoke(_context));

    private void UpdateTargetTriggerContext()
    {
        _context.IsActive = ScrollToTarget?.ActiveTarget == Target;
        _context.ActiveClass = _context.IsActive ? ScrollToTarget?.ActiveClass : null;
        _context.ScrollToTarget ??= InvokeScrollToTarget;
    }

    private void InvokeScrollToTarget()
    {
        ScrollToTarget?.ScrollToTarget(Target);
    }

    ValueTask IAsyncDisposable.DisposeAsync()
    {
        ScrollToTarget?.UnregisterTarget(Target);
        return ValueTask.CompletedTask;
    }
}
