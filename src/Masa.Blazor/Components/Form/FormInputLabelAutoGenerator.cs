using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Masa.Blazor.Components.Input;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace Masa.Blazor.Components.Form;

public class FormInputLabelAutoGenerator : IComponent, IDisposable
{
    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter] private MForm Form { get; set; } = null!;

    private bool _init;
    private Dictionary<object, string> _fullNameMap = new();

    private bool EnableI18n => Form.EnableI18n;

    public void Attach(RenderHandle renderHandle)
    {
        // Do nothing
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        if (_init)
        {
            return Task.CompletedTask;
        }

        _init = true;
        I18n.CultureChanged += I18nOnCultureChanged;

        Form = parameters.GetValueOrDefault<MForm>(nameof(Form)) ??
               throw new InvalidOperationException("Form is null.");

        if (Form.Model is null)
        {
            return Task.CompletedTask;
        }

        Form.OnValidatableChanged += OnValidatableChanged;

        foreach (var input in Form.Validatables)
        {
            if (input.ValueIdentifier.HasValue)
            {
                SetLabel(input);
            }
        }

        return Task.CompletedTask;
    }

    private void I18nOnCultureChanged(object? sender, EventArgs e)
    {
        foreach (var input in Form.Validatables)
        {
            if (input.ValueIdentifier.HasValue)
            {
                SetLabel(input);
            }
        }
    }

    private void OnValidatableChanged(object? sender, MForm.ValidatableChangedEventArgs e)
    {
        if (e.Type == MForm.ValidatableChangedType.Register)
        {
            SetLabel(e.Validatable);
        }
    }

    private void SetLabel(IValidatable validatable)
    {
        var attributeType = Form.LabelAttributeType;

        if (attributeType is null || !validatable.ValueIdentifier.HasValue)
        {
            return;
        }

        var model = validatable.ValueIdentifier.Value.Model;
        var fieldName = validatable.ValueIdentifier.Value.FieldName;
        var attribute = model.GetType().GetProperty(fieldName)?.GetCustomAttribute(attributeType);

        string? displayName = null;
        switch (attribute)
        {
            case DisplayAttribute displayAttribute:
                displayName = displayAttribute.Name;
                break;
            case DisplayNameAttribute displayNameAttribute:
                displayName = displayNameAttribute.DisplayName;
                break;
        }

        if (displayName is null)
        {
            return;
        }

        if (validatable is IInput stringInput)
        {
            var label = EnableI18n ? I18n.T(displayName) : displayName;
            stringInput.SetFormLabel(label);

            var modelFullName = GetOrSetFullName(model);
            var key = $"{modelFullName}.{fieldName}";
            Form.AutoLabelMap[key] = label;
        }
    }
    
    private string GetOrSetFullName(object value)
    {
        if (!_fullNameMap.TryGetValue(value, out var fullName))
        {
            fullName = value.GetType().FullName;
            _fullNameMap[value] = fullName;
        }

        return fullName;
    }

    public void Dispose()
    {
        I18n.CultureChanged -= I18nOnCultureChanged;
        Form.OnValidatableChanged -= OnValidatableChanged;
    }
}