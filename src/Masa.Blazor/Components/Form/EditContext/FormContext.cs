using BlazorComponent.Form;

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

    public void Reset() => Form.Reset();

    public void ResetValidation() => Form.ResetValidation();

    public bool IsValid => Form.Value;

    /// <summary>
    /// parse form validation result,if parse failed throw exception
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see details https://blazor.masastack.com/components/forms
    /// </param>
    public void ParseFormValidation(string validationResult) => Form.ParseFormValidation(validationResult);

    /// <summary>
    /// parse form validation result,if parse failed return false
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see details https://blazor.masastack.com/components/forms
    /// </param>
    /// <returns></returns>
    public bool TryParseFormValidation(string validationResult) => Form.TryParseFormValidation(validationResult);

    public void ParseFormValidation(IEnumerable<ValidationResult> validationResults) =>
        Form.ParseFormValidation(validationResults);
}