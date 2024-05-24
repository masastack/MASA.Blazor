using System.Collections.Concurrent;
using System.Reflection;
using BlazorComponent.Form;
using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public partial class MForm : MasaComponentBase
{
    [Inject] public IServiceProvider ServiceProvider { get; set; } = null!;

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

    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> s_modelPropertiesMap = new();

    private object? _oldModel;
    private IDisposable? _editContextValidation;

    public EditContext? EditContext { get; protected set; }

    public FormContext? FormContext { get; private set; }

    private ValidationMessageStore? ValidationMessageStore { get; set; }

    private List<IValidatable> Validatables { get; } = new();
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Model != null && _oldModel != Model)
        {
            EditContext = new EditContext(Model);
            FormContext = new FormContext(EditContext, this);

            ValidationMessageStore = new ValidationMessageStore(EditContext);
            _editContextValidation = EditContext.EnableValidation(ValidationMessageStore, ServiceProvider, EnableI18n);

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

    public void Register(IValidatable validatable)
    {
        Validatables.Add(validatable);
    }

    internal void Remove(IValidatable validatable)
    {
        Validatables.Remove(validatable);
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
    public void ResetValidation()
    {
        EditContext?.MarkAsUnmodified();

        for (int i = 0; i < Validatables.Count; i++)
        {
            Validatables[i].ResetValidation();
        }

        _ = UpdateValue(true);
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

    protected override async ValueTask DisposeAsyncCore()
    {
        _editContextValidation?.Dispose();

        await base.DisposeAsyncCore();
    }
}