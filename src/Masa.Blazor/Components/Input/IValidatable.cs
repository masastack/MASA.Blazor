namespace Masa.Blazor.Components.Input;

public interface IValidatable
{
    FieldIdentifier ValueIdentifier { get; set; }

    bool Validate();

    void Reset();

    void ResetValidation();

    bool HasError { get; }
}