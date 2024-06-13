namespace Masa.Blazor.Components.Transition;

public class TransitionJsInteropHandle
{
    private readonly ITransitionElement _transition;

    public TransitionJsInteropHandle(ITransitionElement transition)
    {
        _transition = transition;
    }

    [JSInvokable]
    public async Task OnTransitionEnd(string referenceId, LeaveEnter leaveEnter)
    {
        await _transition.OnTransitionEnd(referenceId, leaveEnter);
    }

    [JSInvokable]
    public async Task OnTransitionCancel(string referenceId, LeaveEnter leaveEnter)
    {
        await _transition.OnTransitionCancel(referenceId, leaveEnter);
    }
}