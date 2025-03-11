namespace Masa.Blazor.Components.Form;

/// <summary>
/// Represents the result of validating a field.
/// </summary>
/// <param name="Id">The id attribute of the input element.</param>
/// <param name="Field">The name of the field. It is the same as the property name of the model.</param>
/// <param name="ErrorMessages">List of error messages.</param>
public record FieldValidationResult(string? Id, string? Field, List<string> ErrorMessages);