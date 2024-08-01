using FluentValidation;
using Util.Reflection.Expressions;
using Util.Reflection.Expressions.Abstractions;

namespace Masa.Blazor.Components.Form;

/// <summary>
/// Provides a way to validate a form using FluentValidation. Internal use only.
/// </summary>
public class FluentValidationValidator : ComponentBase, IDisposable
{
    private static readonly Dictionary<Type, Type> FluentValidationTypeMap = new();
    private static readonly Dictionary<Type, IValidator> ModelFluentValidatorMap = new();
    private static readonly Dictionary<Type, Func<object, Dictionary<string, object>>> ModelPropertiesMap = new();

    static FluentValidationValidator()
    {
        try
        {
            var referenceAssembles = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var referenceAssembly in referenceAssembles)
            {
                if (referenceAssembly.FullName!.StartsWith("Microsoft.") ||
                    referenceAssembly.FullName.StartsWith("System."))
                    continue;

                var types = referenceAssembly
                    .GetTypes()
                    .Where(t => t.IsClass)
                    .Where(t => !t.IsAbstract)
                    .Where(t => typeof(IValidator).IsAssignableFrom(t))
                    .ToArray();

                foreach (var type in types)
                {
                    var modelType = type.BaseType!.GenericTypeArguments[0];
                    var validatorType = typeof(IValidator<>).MakeGenericType(modelType);
                    FluentValidationTypeMap.Add(modelType, validatorType);
                }
            }
        }
        catch
        {
            // ignored
        }
    }

    [Inject] private IServiceProvider ServiceProvider { get; set; } = null!;

    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter] internal EditContext EditContext { get; set; } = null!;

    [CascadingParameter] internal MForm Form { get; set; } = null!;

    private bool EnableI18n => Form.EnableI18n;

    protected override void OnInitialized()
    {
        if (!FluentValidationTypeMap.ContainsKey(EditContext.Model.GetType()))
        {
            return;
        }

        EditContext.OnFieldChanged += EditContextOnOnFieldChanged;
        EditContext.OnValidationRequested += EditContextOnOnValidationRequested;
    }

    private void EditContextOnOnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        ValidateField(new FieldIdentifier(new(), string.Empty));
    }

    private void EditContextOnOnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        ValidateField(e.FieldIdentifier);
    }

    private void ValidateField(FieldIdentifier fieldIdentifier)
    {
        var validationResult = GetValidationResult();
        if (validationResult is null)
        {
            return;
        }

        if (fieldIdentifier.FieldName == "")
        {
            Form.ValidationMessageStore?.Clear();
            var propertyMap = GetPropertyMap(EditContext.Model);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName.Contains("."))
                {
                    var propertyName = error.PropertyName.Substring(0, error.PropertyName.LastIndexOf('.'));
                    if (propertyMap.TryGetValue(propertyName, out var modelItem))
                    {
                        var modelItemPropertyName = error.PropertyName.Split('.').Last();
                        AddToMessageStore(new FieldIdentifier(modelItem, modelItemPropertyName), error.ErrorMessage);
                    }
                }
                else
                {
                    AddToMessageStore(new FieldIdentifier(EditContext.Model, error.PropertyName), error.ErrorMessage);
                }
            }
        }
        else
        {
            Form.ValidationMessageStore!.Clear(fieldIdentifier);
            if (fieldIdentifier.Model == EditContext.Model)
            {
                var error = validationResult.Errors.FirstOrDefault(e => e.PropertyName == fieldIdentifier.FieldName);
                if (error is not null)
                {
                    AddToMessageStore(fieldIdentifier, error.ErrorMessage);
                }
            }
            else
            {
                var propertyMap = GetPropertyMap(EditContext.Model);
                var key = propertyMap.FirstOrDefault(pm => pm.Value == fieldIdentifier.Model).Key;
                var error = validationResult.Errors.FirstOrDefault(e =>
                    e.PropertyName == ($"{key}.{fieldIdentifier.FieldName}"));
                if (error is not null)
                {
                    AddToMessageStore(fieldIdentifier, error.ErrorMessage);
                }
            }
        }

        EditContext.NotifyValidationStateChanged();
    }

    private FluentValidation.Results.ValidationResult? GetValidationResult()
    {
        var type = EditContext.Model.GetType();

        if (FluentValidationTypeMap.TryGetValue(type, out var validatorType))
        {
            var validationContext = new ValidationContext<object>(EditContext.Model);
            if (!ModelFluentValidatorMap.TryGetValue(type, out var validator))
            {
                validator = (IValidator?)ServiceProvider.GetService(validatorType);
                if (validator is not null)
                {
                    ModelFluentValidatorMap.TryAdd(type, validator);
                }
            }

            return validator?.Validate(validationContext);
        }

        throw new NotImplementedException($"Validator for {type} does not exists.");
    }

    private void AddToMessageStore(FieldIdentifier fieldIdentifier, string errorMessage)
    {
        Form.ValidationMessageStore!.Add(fieldIdentifier, EnableI18n ? I18n.T(errorMessage) : errorMessage);
    }

    private Dictionary<string, object> GetPropertyMap(object model)
    {
        var type = model.GetType();
        if (ModelPropertiesMap.TryGetValue(type, out var func) is false)
        {
            var modelParameter = Expr.BlockParam<object>().Convert(type);
            Var map = Expr.New<Dictionary<string, object>>();
            BuildPropertyMap(modelParameter, map);

            func = map.BuildDelegate<Func<object, Dictionary<string, object>>>();
            ModelPropertiesMap[type] = func;
        }

        return func(model);

        void BuildPropertyMap(CommonValueExpression value, Var map, CommonValueExpression? basePropertyPath = null)
        {
            basePropertyPath ??= Expr.Constant("");
            var properties = value.Type.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    continue;

                if (property.PropertyType.GetInterfaces().Any(gt => gt == typeof(System.Collections.IEnumerable)))
                {
                    Var index = -1;
                    Expr.Foreach(value[property.Name], (item, _, _) =>
                    {
                        index++;
                        var propertyPath = basePropertyPath + property.Name + "[" + index + "]";
                        map[propertyPath] = item.Convert<object>();
                        BuildPropertyMap(item, map, propertyPath + ".");
                    });
                }
                else
                {
                    var propertyPath = basePropertyPath + property.Name;
                    map[propertyPath] = value[property.Name].Convert<object>();
                    BuildPropertyMap(value[property.Name], map, $"{propertyPath}.");
                }
            }
        }
    }

    public void Dispose()
    {
        EditContext.OnValidationRequested += EditContextOnOnValidationRequested;
        EditContext.OnFieldChanged += EditContextOnOnFieldChanged;
    }
}