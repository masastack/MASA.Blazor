using System.ComponentModel.DataAnnotations;

namespace Masa.Blazor.Components.Form;

// TODO: The properties in the form may not be displayed on the UI.
// In this case, should only the properties displayed on the UI be validated,
// or should all properties be validated?
// Validating all properties is simpler.

/// <summary>
/// Provides a way to validate a form using DataAnnotations. Internal use only.
/// Source: https://github.com/dotnet/aspnetcore/blob/b89535c2cf78268e3b099ab92ff5b96c2d8b6f4f/src/Components/Blazor/Validation/src/ObjectGraphDataAnnotationsValidator.cs
/// </summary>
public class ObjectGraphDataAnnotationsValidator : ComponentBase, IDisposable
{
    private static readonly object ValidationContextValidatorKey = new object();
    private static readonly object ValidatedObjectsKey = new object();
    private static Dictionary<object, string> valueFullNameMap = new();

    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter] internal EditContext EditContext { get; set; } = null!;

    [CascadingParameter] internal MForm Form { get; set; } = null!;

    private bool EnableI18n => Form.EnableI18n;

    protected override void OnInitialized()
    {
        // Perform object-level validation (starting from the root model) on request
        EditContext.OnValidationRequested += EditContextOnOnValidationRequested;

        // Perform per-field validation on each field edit
        EditContext.OnFieldChanged += EditContextOnOnFieldChanged;
    }

    private void EditContextOnOnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        ValidateField(EditContext, Form.ValidationMessageStore!, e.FieldIdentifier);
    }

    private void EditContextOnOnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        Form.ValidationMessageStore?.Clear();
        ValidateObject(EditContext.Model, new HashSet<object>());
        EditContext.NotifyValidationStateChanged();
    }


    internal void ValidateObject(object value, HashSet<object> visited)
    {
        if (value is null)
        {
            return;
        }

        if (!visited.Add(value))
        {
            // Already visited this object.
            return;
        }

        if (value is IEnumerable<object> enumerable)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                ValidateObject(item, visited);
                index++;
            }

            return;
        }

        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        ValidateObject(value, visited, validationResults);

        // Transfer results to the ValidationMessageStore
        foreach (var validationResult in validationResults)
        {
            if (!validationResult.MemberNames.Any())
            {
                Form.ValidationMessageStore!.Add(new FieldIdentifier(value, string.Empty),
                    GetFormatedErrorMessage(value, validationResult));
                continue;
            }

            foreach (var memberName in validationResult.MemberNames)
            {
                var fieldIdentifier = new FieldIdentifier(value, memberName);
                Form.ValidationMessageStore!.Add(fieldIdentifier, GetFormatedErrorMessage(value, validationResult));
            }
        }
    }

    private string? GetFormatedErrorMessage(object value,
        System.ComponentModel.DataAnnotations.ValidationResult validationResult)
    {
        if (!EnableI18n)
        {
            return validationResult.ErrorMessage;
        }

        if (!valueFullNameMap.TryGetValue(value, out var instanceFullName))
        {
            instanceFullName = value.GetType().FullName;
            valueFullNameMap[value] = instanceFullName;
        }

        var memberName = validationResult.MemberNames.LastOrDefault();
        var autoLabelKey = $"{instanceFullName}.{memberName}";
        var label = memberName;

        if (Form?.AutoLabelMap.TryGetValue(autoLabelKey, out var autoLabel) is true)
        {
            label = autoLabel;
        }

        return I18n.T(validationResult.ErrorMessage, args: label);
    }

    private void ValidateObject(object value, HashSet<object> visited,
        List<System.ComponentModel.DataAnnotations.ValidationResult> validationResults)
    {
        var validationContext = new ValidationContext(value);
        validationContext.Items.Add(ValidationContextValidatorKey, this);
        validationContext.Items.Add(ValidatedObjectsKey, visited);
        Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
    }

    internal static bool TryValidateRecursive(object value, ValidationContext validationContext)
    {
        if (validationContext.Items.TryGetValue(ValidationContextValidatorKey, out var result) &&
            result is ObjectGraphDataAnnotationsValidator validator)
        {
            var visited = (HashSet<object>)validationContext.Items[ValidatedObjectsKey];
            validator.ValidateObject(value, visited);

            return true;
        }

        return false;
    }

    private void ValidateField(EditContext editContext, ValidationMessageStore messages,
        in FieldIdentifier fieldIdentifier)
    {
        // DataAnnotations only validates public properties, so that's all we'll look for
        var propertyInfo = fieldIdentifier.Model.GetType().GetProperty(fieldIdentifier.FieldName);
        if (propertyInfo != null)
        {
            var model = fieldIdentifier.Model;
            var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
            var validationContext = new ValidationContext(fieldIdentifier.Model)
            {
                MemberName = propertyInfo.Name
            };
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateProperty(propertyValue, validationContext, results);
            messages.Clear(fieldIdentifier);
            messages.Add(fieldIdentifier, results.Select(result => GetFormatedErrorMessage(model, result)));

            // We have to notify even if there were no messages before and are still no messages now,
            // because the "state" that changed might be the completion of some async validation task
            editContext.NotifyValidationStateChanged();
        }
    }

    public void Dispose()
    {
        EditContext.OnValidationRequested -= EditContextOnOnValidationRequested;
        EditContext.OnFieldChanged -= EditContextOnOnFieldChanged;
    }
}