using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Presets;

public class PModal : ModalBase
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Persistent = true;

        await base.SetParametersAsync(parameters);
    }
}