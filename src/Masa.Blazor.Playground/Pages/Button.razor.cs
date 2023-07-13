using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Pages;

public class Button : MTransitionElement
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Tag = "button";

        await base.SetParametersAsync(parameters);
    }
}
