using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace Masa.Blazor.Components.Form;

/// <summary>
/// The settings for automatically generating the label of the form input.
/// Should be placed in the <see cref="MForm"/> component.
/// </summary>
public class AutoLabelOptions : IComponent
{
    [CascadingParameter] private MForm? Form { get; set; }

    /// <summary>
    /// The attribute type to get the display name.
    /// Supported types are <see cref="DisplayAttribute"/> and <see cref="System.ComponentModel.DisplayNameAttribute"/>.
    /// </summary>
    [Parameter]
    public Type? AttributeType { get; set; } = typeof(DisplayNameAttribute);

    private bool _init;

    public void Attach(RenderHandle renderHandle)
    {
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        if (_init)
        {
            return Task.CompletedTask;
        }

        _init = true;

        if (parameters.TryGetValue(nameof(Form), out MForm? form))
        {
            Form = form;
        }

        if (Form is null)
        {
            return Task.CompletedTask;
        }

        if (parameters.TryGetValue(nameof(AttributeType), out Type? attributeType))
        {
            AttributeType = attributeType;
        }

        Form.LabelAttributeType = AttributeType;

        return Task.CompletedTask;
    }
}