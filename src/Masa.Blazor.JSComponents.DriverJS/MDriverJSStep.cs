using Masa.Blazor.JSComponents.DriverJS;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor;

internal interface IDriverJSStep
{
    string? Element { get; }

    string? Title { get; }

    string? Description { get; }

    PopoverSide Side { get; }

    PopoverAlign Align { get; }

    string? PopoverClass { get; }
}

public class MDriverJSStep : IComponent, IDriverJSStep
{
    [CascadingParameter] public MDriverJS? DriverJS { get; set; }

    /// <summary>
    /// The target element to highlight.
    /// If not specified, the popover will be displayed in the center of the screen.
    /// </summary>
    [Parameter] public string? Element { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Description { get; set; }

    [Parameter] public PopoverSide Side { get; set; }

    [Parameter] public PopoverAlign Align { get; set; }

    [Parameter] public string? PopoverClass { get; set; }

    private bool _init;

    public void Attach(RenderHandle renderHandle)
    {
    }

    public async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (_init) return;

        _init = true;

        if (DriverJS is null)
        {
            return;
        }

        await DriverJS.AddStep(this);
    }
}