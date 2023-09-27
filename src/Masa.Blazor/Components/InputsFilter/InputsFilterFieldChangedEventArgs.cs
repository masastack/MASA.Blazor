namespace Masa.Blazor;

public class InputsFilterFieldChangedEventArgs : EventArgs
{
    public InputsFilterFieldChangedEventArgs(string fieldName, bool isClear)
    {
        FieldName = fieldName;
        IsClear = isClear;
    }

    public string FieldName { get; }

    public bool IsClear { get; }
}
