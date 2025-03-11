namespace Masa.Blazor.Components.Form;

public record FormValidationResult(bool Valid, List<FieldValidationResult> Results);