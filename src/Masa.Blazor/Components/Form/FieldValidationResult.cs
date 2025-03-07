namespace Masa.Blazor.Components.Form;

public record FieldValidationResult(FieldIdentifier Field, List<string> ErrorMessages);