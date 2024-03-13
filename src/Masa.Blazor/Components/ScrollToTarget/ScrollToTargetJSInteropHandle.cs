namespace Masa.Blazor.Components.ScrollToTarget;

public class ScrollToTargetJSInteropHandle
{
    private readonly MScrollToTarget _component;

    public ScrollToTargetJSInteropHandle(MScrollToTarget component)
    {
        _component = component;
    }

    [JSInvokable]
    public ValueTask UpdateActiveTarget(string target) => _component.UpdateActiveTarget(target);
}
