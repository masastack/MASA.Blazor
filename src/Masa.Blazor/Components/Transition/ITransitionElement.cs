namespace Masa.Blazor.Components.Transition;

public interface ITransitionElement
{
    Task OnTransitionEnd(string referenceId, LeaveEnter transition);
    
    Task OnTransitionCancel(string referenceId, LeaveEnter transition);
}