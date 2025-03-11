using Masa.Blazor.Components.Form;
using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public class FormContext
{
    public FormContext(MForm form)
    {
        _form = form;
    }

    public FormContext(EditContext editContext, MForm form)
    {
        EditContext = editContext;
        _form = form;
    }

    private readonly MForm _form;

    public EditContext? EditContext { get; }

    public bool Validate() => _form.Validate();

    public bool Validate(FieldIdentifier fieldIdentifier) => _form.Validate(fieldIdentifier);

    public bool Validate(IValidatable validatable) => _form.Validate(validatable);

    public void Reset() => _form.Reset();

    public void Reset(FieldIdentifier fieldIdentifier) => _form.Reset(fieldIdentifier);

    public void Reset(IValidatable validatable) => _form.Reset(validatable);

    public void ResetValidation() => _form.ResetValidation();

    public void ResetValidation(FieldIdentifier fieldIdentifier) => _form.ResetValidation(fieldIdentifier);

    public void ResetValidation(IValidatable validatable) => _form.ResetValidation(validatable);

    public bool IsValid => _form.Value;

    /// <summary>
    /// parse form validation result,if parse failed throw exception
    /// </summary>
    /// <param name="validationResult"></param>
    public void ParseFormValidation(string validationResult) => _form.ParseFormValidation(validationResult);

    /// <summary>
    /// parse form validation result,if parse failed return false
    /// </summary>
    /// <param name="validationResult"></param>
    public bool TryParseFormValidation(string validationResult) => _form.TryParseFormValidation(validationResult);

    public void ParseFormValidation(IEnumerable<ValidationResult> validationResults) =>
        _form.ParseFormValidation(validationResults);
}