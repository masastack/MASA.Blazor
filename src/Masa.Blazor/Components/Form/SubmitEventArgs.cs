namespace Masa.Blazor.Components.Form;

public class SubmitEventArgs(List<FieldValidationResult> results, FormContext formContext) : EventArgs
{
    public bool Valid { get; init; } = formContext.IsValid;

    public List<FieldValidationResult> Results { get; init; } = results;

    public FormContext FormContext { get; init; } = formContext;
}