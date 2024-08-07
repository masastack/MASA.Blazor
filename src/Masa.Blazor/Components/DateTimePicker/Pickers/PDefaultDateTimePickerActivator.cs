using Masa.Blazor.Components.DateTimePicker.Pickers;

namespace Masa.Blazor.Presets;

public class PDefaultDateTimePickerActivator : ComponentBase
{
    [CascadingParameter] public IDateTimePicker? DateTimePicker { get; set; }

    [Parameter] public bool Clearable { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool Filled { get; set; }

    [Parameter] public StringBoolean HideDetails { get; set; } = "true";

    [Parameter] public string? Label { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public bool Solo { get; set; }

    [Parameter] public bool SoloInverted { get; set; }

    [Parameter] public string? Format { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // All the parameters are value types, so no need to check for changes
        DateTimePicker?.UpdateActivator(this);
    }
}