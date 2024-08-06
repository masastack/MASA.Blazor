using Masa.Blazor.Components.Form;
using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public class FormContext
{
    public FormContext(MForm form)
    {
        Form = form;
    }

    public FormContext(EditContext editContext, MForm form)
    {
        EditContext = editContext;
        Form = form;
    }

    public EditContext? EditContext { get; }

    private MForm Form { get; }

    public bool Validate() => Form.Validate();

    public bool Validate(FieldIdentifier fieldIdentifier) => Form.Validate(fieldIdentifier);

    public bool Validate(IValidatable validatable) => Form.Validate(validatable);

    public void Reset() => Form.Reset();

    public void Reset(FieldIdentifier fieldIdentifier) => Form.Reset(fieldIdentifier);

    public void Reset(IValidatable validatable) => Form.Reset(validatable);

    public void ResetValidation() => Form.ResetValidation();

    public void ResetValidation(FieldIdentifier fieldIdentifier) => Form.ResetValidation(fieldIdentifier);

    public void ResetValidation(IValidatable validatable) => Form.ResetValidation(validatable);

    public bool IsValid => Form.Value;

    /// <summary>
    /// parse form validation result,if parse failed throw exception
    /// </summary>
    /// <param name="validationResult"></param>
    public void ParseFormValidation(string validationResult) => Form.ParseFormValidation(validationResult);

    /// <summary>
    /// parse form validation result,if parse failed return false
    /// </summary>
    /// <param name="validationResult"></param>
    public bool TryParseFormValidation(string validationResult) => Form.TryParseFormValidation(validationResult);

    public void ParseFormValidation(IEnumerable<ValidationResult> validationResults) =>
        Form.ParseFormValidation(validationResults);
}