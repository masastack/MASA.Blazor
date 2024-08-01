using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using Masa.Blazor.Components.Form;
using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public partial class MForm : MasaComponentBase
{
    [Parameter] public RenderFragment<FormContext>? ChildContent { get; set; }

    [Parameter] public EventCallback<EventArgs> OnSubmit { get; set; }

    [Parameter] public object? Model { get; set; }

    [Parameter]
    [Obsolete("This parameter is not required when the Model parameter has been set.")]
    public bool EnableValidation { get; set; }

    [Parameter] public bool EnableI18n { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Value { get; set; } = true;

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public EventCallback OnValidSubmit { get; set; }

    [Parameter] public EventCallback OnInvalidSubmit { get; set; }

    [Parameter]
    [MasaApiParameter(true, ReleasedOn = "v1.7.0")]
    public bool AutoLabel { get; set; } = true;

    internal ConcurrentDictionary<string, string> AutoLabelMap { get; } = new();

    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> s_modelPropertiesMap = new();

    private object? _oldModel;

    public EditContext? EditContext { get; protected set; }

    public FormContext? FormContext { get; private set; }

    internal ValidationMessageStore? ValidationMessageStore { get; private set; }

    public List<IValidatable> Validatables { get; } = new();

    /// <summary>
    /// The type of property attribute used to generate the label,
    /// the default value is same as <see cref="FFormAutoLabelOptionsAttributeType"/>.
    /// </summary>
    internal Type? LabelAttributeType { get; set; } = typeof(DisplayNameAttribute);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Model != null && _oldModel != Model)
        {
            EditContext = new EditContext(Model);
            FormContext = new FormContext(EditContext, this);
            ValidationMessageStore = new ValidationMessageStore(EditContext);

            _oldModel = Model;
        }
        else if (FormContext == null)
        {
            FormContext = new FormContext(this);
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-form";
    }

    internal void UpdateValidValue()
    {
        var hasError = Validatables.Any(v => v.HasError);
        var valid = !hasError;
        _ = UpdateValue(valid);
    }

    /// <summary>
    /// Register a validatable component to the form
    /// </summary>
    /// <param name="validatable"></param>
    internal void Register(IValidatable validatable)
    {
        Validatables.Add(validatable);
        OnValidatableChanged?.Invoke(this,
            new ValidatableChangedEventArgs(validatable, ValidatableChangedType.Register));
    }

    /// <summary>
    /// Unregister a validatable component from the form
    /// </summary>
    /// <param name="validatable"></param>
    internal void Remove(IValidatable validatable)
    {
        Validatables.Remove(validatable);
        OnValidatableChanged?.Invoke(this,
            new ValidatableChangedEventArgs(validatable, ValidatableChangedType.Unregister));
    }

    public event EventHandler<ValidatableChangedEventArgs>? OnValidatableChanged;

    public enum ValidatableChangedType
    {
        Register,
        Unregister
    }

    public class ValidatableChangedEventArgs(IValidatable validatable, ValidatableChangedType type) : EventArgs
    {
        public IValidatable Validatable { get; } = validatable;

        public ValidatableChangedType Type { get; } = type;
    }

    private async Task HandleOnSubmitAsync(EventArgs args)
    {
        var valid = Validate();

        if (OnSubmit.HasDelegate)
        {
            await OnSubmit.InvokeAsync(args);
        }

        if (valid)
        {
            if (OnValidSubmit.HasDelegate)
            {
                await OnValidSubmit.InvokeAsync();
            }
        }
        else
        {
            if (OnInvalidSubmit.HasDelegate)
            {
                await OnInvalidSubmit.InvokeAsync();
            }
        }
    }

    /// <summary>
    /// Validate the all fields in the form
    /// </summary>
    /// <returns></returns>
    [MasaApiPublicMethod]
    public bool Validate()
    {
        var valid = true;

        foreach (var validatable in Validatables)
        {
            var success = validatable.Validate();
            if (!success)
            {
                valid = false;
            }
        }

        if (EditContext != null)
        {
            var success = EditContext.Validate();

            valid = valid && success;
        }

        _ = UpdateValue(valid);

        return valid;
    }

    /// <summary>
    /// Validate the specified field in the form
    /// </summary>
    /// <param name="validatable"></param>
    /// <returns></returns>
    [MasaApiParameter]
    public bool Validate(IValidatable validatable)
    {
        var valid = validatable.Validate();

        var valueIdentifier = validatable.ValueIdentifier;

        if (valid && EditContext is not null && valueIdentifier.HasValue)
        {
            EditContext.NotifyFieldChanged(valueIdentifier.Value);
            valid = valid && !EditContext.GetValidationMessages(valueIdentifier.Value).Any();
        }

        return valid;
    }

    /// <summary>
    /// Validate the specified field in the form
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [MasaApiParameter]
    public bool Validate(FieldIdentifier fieldIdentifier)
    {
        var index = Validatables.FindIndex(item => item.ValueIdentifier.Equals(fieldIdentifier));
        if (index == -1)
        {
            throw new ArgumentException($"Field {fieldIdentifier.FieldName} not found in form.");
        }

        var validatable = Validatables[index];
        return Validate(validatable);
    }

    /// <summary>
    /// parse form validation result,if parse faield throw exception
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see deatils https://blazor.masastack.com/components/forms
    /// </param>
    [MasaApiPublicMethod]
    public void ParseFormValidation(string validationResult)
    {
        if (TryParseFormValidation(validationResult) is false)
            throw new Exception(validationResult);
    }

    /// <summary>
    /// parse form validation result,if parse failed return false
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see deatils https://blazor.masastack.com/components/forms
    /// </param>
    /// <returns></returns>
    [MasaApiPublicMethod]
    public bool TryParseFormValidation(string validationResult)
    {
        if (string.IsNullOrEmpty(validationResult)) return false;
        var resultStrs = validationResult.Split(Environment.NewLine).ToList();
        if (resultStrs.Count < 1 || resultStrs[0].StartsWith("Validation failed:") is false) return false;
        resultStrs.RemoveAt(0);
        var validationResults = new List<ValidationResult>();
        foreach (var resultStr in resultStrs)
        {
            int startIndex = resultStr.IndexOf(" -- ", StringComparison.Ordinal) + 4;
            if (startIndex < 4) continue;
            int colonIndex = resultStr.IndexOf(": ", StringComparison.Ordinal);
            var field = resultStr.Substring(startIndex, colonIndex - startIndex);
            int severityIndex = resultStr.IndexOf("Severity: ", StringComparison.Ordinal);
            colonIndex += 2;
            var msg = resultStr.Substring(colonIndex, severityIndex - colonIndex);
            Enum.TryParse<ValidationResultTypes>(resultStr.Substring(severityIndex + 10), out var type);
            validationResults.Add(new ValidationResult(field, msg, type));
        }

        ParseFormValidation(validationResults.ToArray());

        return true;
    }

    [MasaApiPublicMethod]
    public void ParseFormValidation(IEnumerable<ValidationResult> validationResults)
    {
        if (Model == null) return;

        foreach (var validationResult in validationResults.Where(item =>
                     item.ValidationResultType == ValidationResultTypes.Error))
        {
            var model = Model;
            var field = validationResult.Field;
            if (validationResult.Field?.Contains('.') is true)
            {
                var fieldChunks = validationResult.Field.Split('.');
                field = fieldChunks.Last();
                foreach (var fieldChunk in fieldChunks)
                {
                    if (fieldChunk != field)
                        model = GetModelValue(model!, fieldChunk,
                            () => throw new Exception(
                                $"{validationResult.Field} is error,can not read {fieldChunk}"));
                }
            }

            if (model is null) return;

            var fieldIdentifier = new FieldIdentifier(model, field);
            var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(fieldIdentifier));
            if (validatable is not null)
            {
                ValidationMessageStore?.Clear(fieldIdentifier);
                ValidationMessageStore?.Add(fieldIdentifier, validationResult.Message);
            }
        }

        EditContext?.NotifyValidationStateChanged();

        _ = UpdateValue(false);

        object? GetModelValue(object model, string fieldChunk, Action whenError)
        {
            var type = model.GetType();
            if (s_modelPropertiesMap.TryGetValue(type, out var propertyInfos) is false)
            {
                propertyInfos = type.GetProperties();
                s_modelPropertiesMap[type] = propertyInfos;
            }

            if (fieldChunk.Contains('['))
            {
                var leftBracketsIndex = fieldChunk.IndexOf('[') + 1;
                var rightBracketsIndex = fieldChunk.IndexOf(']');
                var filedName = fieldChunk.Substring(0, leftBracketsIndex - 1);
                var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == filedName);
                if (propertyInfo is null)
                {
                    whenError.Invoke();
                }
                else
                {
                    model = propertyInfo.GetValue(model);
                    var enumerable = model as System.Collections.IEnumerable;
                    var index = Convert.ToInt32(fieldChunk.Substring(leftBracketsIndex,
                        rightBracketsIndex - leftBracketsIndex));
                    var i = 0;
                    foreach (var item in enumerable)
                    {
                        if (i == index)
                        {
                            model = item;
                            break;
                        }

                        i++;
                    }
                }
            }
            else
            {
                var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == fieldChunk);
                if (propertyInfo is null)
                {
                    whenError.Invoke();
                }
                else
                {
                    model = propertyInfo.GetValue(model);
                }
            }

            return model;
        }
    }

    [MasaApiPublicMethod]
    public void Reset()
    {
        EditContext?.MarkAsUnmodified();

        for (int i = 0; i < Validatables.Count; i++)
        {
            Validatables[i].Reset();
        }

        _ = UpdateValue(true);
    }

    [MasaApiPublicMethod]
    public void Reset(FieldIdentifier fieldIdentifier)
    {
        var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(fieldIdentifier));
        if (validatable is null)
        {
            throw new ArgumentException($"Field {fieldIdentifier.FieldName} not found in form.");
        }
        
        Reset(validatable);
    }

    [MasaApiPublicMethod]
    public void Reset(IValidatable validatable)
    {
        if (validatable.ValueIdentifier.HasValue)
        {
            EditContext?.MarkAsUnmodified(validatable.ValueIdentifier.Value);
        }
        
        validatable.Reset();

        UpdateValidValue();
    }

    [MasaApiPublicMethod]
    public void ResetValidation()
    {
        EditContext?.MarkAsUnmodified();

        for (int i = 0; i < Validatables.Count; i++)
        {
            Validatables[i].ResetValidation();
        }

        _ = UpdateValue(true);
    }
    
    [MasaApiPublicMethod]
    public void ResetValidation(FieldIdentifier fieldIdentifier)
    {
        var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(fieldIdentifier));
        if (validatable is null)
        {
            throw new ArgumentException($"Field {fieldIdentifier.FieldName} not found in form.");
        }
        
        ResetValidation(validatable);
    }
    
    [MasaApiPublicMethod]
    public void ResetValidation(IValidatable validatable)
    {
        if (validatable.ValueIdentifier.HasValue)
        {
            EditContext?.MarkAsUnmodified(validatable.ValueIdentifier.Value);
        }
        
        validatable.ResetValidation();

        UpdateValidValue();
    }

    private async Task UpdateValue(bool val)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(val);
        }
        else
        {
            Value = val;
            StateHasChanged();
        }
    }
}