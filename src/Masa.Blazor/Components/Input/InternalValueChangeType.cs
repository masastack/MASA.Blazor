namespace Masa.Blazor.Components.Input;

public enum InternalValueChangeType
{
    /// <summary>
    /// Triggered by button click or something else 
    /// </summary>
    InternalOperation,

    /// <summary>
    /// Triggered by original input or change event
    /// </summary>
    Input,
}