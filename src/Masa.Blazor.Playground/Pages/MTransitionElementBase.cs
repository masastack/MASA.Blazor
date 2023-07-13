using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public class MTransitionElementBase<TValue> : Element where TValue : notnull
{
    [CascadingParameter]
    protected MTransition? Transition { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;
}
