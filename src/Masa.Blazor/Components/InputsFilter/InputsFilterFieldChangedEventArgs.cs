namespace Masa.Blazor;

public class InputsFilterFieldChangedEventArgs(string? fieldName, bool isClear) : EventArgs
{
    public string? FieldName { get; } = fieldName;

    public bool IsClear { get; } = isClear;
}
