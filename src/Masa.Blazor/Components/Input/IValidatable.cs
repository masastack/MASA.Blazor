namespace Masa.Blazor.Components.Input;

public interface IValidatable
{
    FieldIdentifier? ValueIdentifier { get; }

    bool Validate();

    void Reset();

    void ResetValidation();

    bool HasError { get; }
}