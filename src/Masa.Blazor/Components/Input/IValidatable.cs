using Masa.Blazor.Components.Form;

namespace Masa.Blazor.Components.Input;

public interface IValidatable
{
    FieldIdentifier? ValueIdentifier { get; }

    bool Validate(bool force = false);
    
    bool Validate(out FieldValidationResult result, bool force = false);

    void Reset();

    void ResetValidation();

    bool HasError { get; }
}