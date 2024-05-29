namespace Masa.Blazor.Components.Form;

public class ValidationResult
{
    public ValidationResult(string field, string message, ValidationResultTypes validationResultType)
    {
        Field = field;
        Message = message;
        ValidationResultType = validationResultType;
    }

    public string Field { get; init; }

    public string Message { get; init; }

    public ValidationResultTypes ValidationResultType { get; init; }
}
