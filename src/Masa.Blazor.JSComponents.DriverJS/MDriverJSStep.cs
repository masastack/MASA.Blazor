using Masa.Blazor.Attributes;
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

    PopoverButton[] ShowButtons { get; }

    PopoverButton[] DisableButtons { get; }

    string? NextBtnText { get; }

    string? PrevBtnText { get; }

    string? DoneBtnText { get; }
}

public class MDriverJSStep : Popover, IComponent, IDriverJSStep
{
    [CascadingParameter] public MDriverJS? DriverJS { get; set; }

    /// <summary>
    /// The target element to highlight.
    /// If not specified, the popover will be displayed in the center of the screen.
    /// </summary>
    [Parameter] public string? Element { get; set; }

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