using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public interface ITransitionJSCallback
{
    string? TransitionName { get; }

    bool LeaveAbsolute { get; }

    ElementReference Reference { get; }

    Task HandleOnTransitionend();
}
