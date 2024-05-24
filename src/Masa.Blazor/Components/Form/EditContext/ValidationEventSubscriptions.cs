using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Util.Reflection.Expressions;
using Util.Reflection.Expressions.Abstractions;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Masa.Blazor;

internal sealed class ValidationEventSubscriptions : IDisposable
{
    private static readonly ConcurrentDictionary<Type, IValidator> ModelFluentValidatorMap = new();
    private static readonly ConcurrentDictionary<Type, Func<object, Dictionary<string, object>>> ModelPropertiesMap = new();
    private static readonly Dictionary<Type, Type> FluentValidationTypeMap = new();

    static ValidationEventSubscriptions()
    {
        try
        {
            var referenceAssembles = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var referenceAssembly in referenceAssembles)
            {
                if (referenceAssembly!.FullName!.StartsWith("Microsoft.") || referenceAssembly.FullName.StartsWith("System."))
                    continue;

                var types = referenceAssembly
                            .GetTypes()
                            .Where(t => t.IsClass)
                            .Where(t => !t.IsAbstract)
                            .Where(t => typeof(IValidator).IsAssignableFrom(t))
                            .ToArray();

                foreach (var type in types)
                {
                    var modelType = type!.BaseType!.GenericTypeArguments[0];
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

    private readonly EditContext _editContext;
    private readonly ValidationMessageStore _messageStore;
    private readonly IServiceProvider _serviceProvider;
    private BlazorComponent.I18n.I18n? _i18n;

    [MemberNotNullWhen(true, nameof(_i18n))]
    private bool EnableI18n { get; set; }

    public ValidationEventSubscriptions(EditContext editContext, ValidationMessageStore messageStore, IServiceProvider serviceProvider,
        bool enableI18n)
    {
        _serviceProvider = serviceProvider;
        _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
        _messageStore = messageStore;

        _editContext.OnFieldChanged += OnFieldChanged;
        _editContext.OnValidationRequested += OnValidationRequested;
        EnableI18n = enableI18n;
        if (EnableI18n)
        {
            _i18n = _serviceProvider.GetService<BlazorComponent.I18n.I18n>();
        }
    }

    private void OnFieldChanged(object? sender, FieldChangedEventArgs eventArgs)
    {
        Validate(eventArgs.FieldIdentifier);
    }

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        Validate(new FieldIdentifier(new(), ""));
    }

    private void Validate(FieldIdentifier field)
    {
        if (FluentValidationTypeMap.ContainsKey(_editContext.Model.GetType()))
        {
            FluentValidate(_editContext.Model, _messageStore, field);
        }
        else
        {
            DataAnnotationsValidate(_editContext.Model, _messageStore, field);
        }

        _editContext.NotifyValidationStateChanged();
    }

    private void DataAnnotationsValidate(object model, ValidationMessageStore messageStore, FieldIdentifier field)
    {
        var validationResults = new List<ValidationResult>();
        if (field.FieldName == "")
        {
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            messageStore.Clear();

            foreach (var validationResult in validationResults)
            {
                if (validationResult is EnumerableValidationResult enumerableValidationResult)
                {
                    foreach (var descriptor in enumerableValidationResult.Descriptors)
                    {
                        foreach (var result in descriptor.Results)
                        {
                            foreach (var memberName in result.MemberNames)
                            {
                                AddValidationMessage(new FieldIdentifier(descriptor.ObjectInstance, memberName), result.ErrorMessage!);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        AddValidationMessage(new FieldIdentifier(model, memberName), validationResult.ErrorMessage);
                    }
                }
            }
        }
        else
        {
            var validationContext = new ValidationContext(field.Model);
            Validator.TryValidateObject(field.Model, validationContext, validationResults, true);
            messageStore.Clear(field);
            foreach (var validationResult in validationResults)
            {
                if (validationResult.MemberNames.Contains(field.FieldName))
                {
                    AddValidationMessage(field, validationResult.ErrorMessage);
                    return;
                }
            }
        }
    }

    private void FluentValidate(object model, ValidationMessageStore messageStore, FieldIdentifier field)
    {
        var validationResult = GetValidationResult(model);
        if (validationResult is null) return;

        if (field.FieldName == "")
        {
            messageStore.Clear();
            var propertyMap = GetPropertyMap(model);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName.Contains("."))
                {
                    var propertyName = error.PropertyName.Substring(0, error.PropertyName.LastIndexOf('.'));
                    if (propertyMap.TryGetValue(propertyName, out var modelItem))
                    {
                        var modelItemPropertyName = error.PropertyName.Split('.').Last();
                        AddValidationMessage(new FieldIdentifier(modelItem, modelItemPropertyName), error.ErrorMessage);
                    }
                }
                else
                {
                    AddValidationMessage(new FieldIdentifier(model, error.PropertyName), error.ErrorMessage);
                }
            }
        }
        else
        {
            messageStore.Clear(field);
            if (field.Model == model)
            {
                var error = validationResult.Errors.FirstOrDefault(e => e.PropertyName == field.FieldName);
                if (error is not null)
                {
                    AddValidationMessage(field, error.ErrorMessage);
                }
            }
            else
            {
                var propertyMap = GetPropertyMap(model);
                var key = propertyMap.FirstOrDefault(pm => pm.Value == field.Model).Key;
                var errorMessage = validationResult.Errors.FirstOrDefault(e => e.PropertyName == ($"{key}.{field.FieldName}"))?.ErrorMessage;
                if (errorMessage is not null)
                {
                    AddValidationMessage(field, errorMessage);
                }
            }
        }
    }

    private FluentValidationResult? GetValidationResult(object model)
    {
        var type = model.GetType();

        if (FluentValidationTypeMap.TryGetValue(type, out var validatorType))
        {
            var validationContext = new ValidationContext<object>(model);
            if (!ModelFluentValidatorMap.TryGetValue(type, out var validator))
            {
                validator = (IValidator?)_serviceProvider.GetService(validatorType);
                if (validator is not null)
                {
                    ModelFluentValidatorMap.TryAdd(type, validator);
                }
            }

            return validator?.Validate(validationContext);
        }

        throw new NotImplementedException($"Validator for {type} does not exists.");
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

    private void AddValidationMessage(in FieldIdentifier fieldIdentifier, string? message)
    {
        if (message == null) return;

        if (EnableI18n)
        {
            message = _i18n.T(message, args: fieldIdentifier.FieldName);
        }

        _messageStore.Add(fieldIdentifier, message);
    }

    public void Dispose()
    {
        _messageStore.Clear();
        _editContext.OnFieldChanged -= OnFieldChanged;
        _editContext.OnValidationRequested -= OnValidationRequested;
        _editContext.NotifyValidationStateChanged();
    }
}
