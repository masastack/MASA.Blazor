using Masa.Blazor.Presets;

namespace Masa.Blazor.Docs.Examples.components.enqueued_snackbars;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => nameof(PEnqueuedSnackbars);

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(PEnqueuedSnackbars.Outlined), false },
        { nameof(PEnqueuedSnackbars.Text), false }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        {
            nameof(PEnqueuedSnackbars.Position), new SelectParameter(new List<string>()
            {
                nameof(SnackPosition.TopLeft),
                nameof(SnackPosition.TopRight),
                nameof(SnackPosition.TopCenter),
                nameof(SnackPosition.BottomLeft),
                nameof(SnackPosition.BottomRight),
                nameof(SnackPosition.BottomCenter),
            }, nameof(SnackPosition.BottomCenter))
        }
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(PEnqueuedSnackbars.Closeable), new CheckboxParameter(true) },
        { nameof(PEnqueuedSnackbars.Shaped), new CheckboxParameter(false) },
        { nameof(PEnqueuedSnackbars.Rounded), new CheckboxParameter("xl", false) }
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(PEnqueuedSnackbars.MaxCount), new SliderParameter(5, 1, 10, 1) },
        { nameof(PEnqueuedSnackbars.Timeout), new SliderParameter(5000, 0, 9000, 1000) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        return parameter.Key switch
        {
            nameof(PEnqueuedSnackbars.Position) => GetPosition(parameter.Value?.ToString()),
            nameof(PEnqueuedSnackbars.MaxCount) => int.Parse(parameter.Value!.ToString()!),
            nameof(PEnqueuedSnackbars.Timeout)  => int.Parse(parameter.Value!.ToString()!),
            nameof(PEnqueuedSnackbars.Rounded)  => (StringBoolean)(parameter.Value is null ? false : "xl"),
            _                                   => parameter.Value
        };
    }

    protected override string FormatValue(string key, object value)
    {
        if (key == nameof(PEnqueuedSnackbars.Position))
        {
            return value.ToString() switch
            {
                nameof(SnackPosition.TopLeft)      => "@SnackPosition.TopLeft",
                nameof(SnackPosition.TopRight)     => "@SnackPosition.TopRight",
                nameof(SnackPosition.TopCenter)    => "@SnackPosition.TopCenter",
                nameof(SnackPosition.BottomLeft)   => "@SnackPosition.BottomLeft",
                nameof(SnackPosition.BottomRight)  => "@SnackPosition.BottomRight",
                nameof(SnackPosition.BottomCenter) => "@SnackPosition.BottomCenter",
                _                                  => base.FormatValue(key, value)
            };
        }

        return base.FormatValue(key, value);
    }

    private static SnackPosition GetPosition(string? name)
    {
        return name switch
        {
            nameof(SnackPosition.TopLeft)     => SnackPosition.TopLeft,
            nameof(SnackPosition.TopRight)    => SnackPosition.TopRight,
            nameof(SnackPosition.TopCenter)   => SnackPosition.TopCenter,
            nameof(SnackPosition.BottomLeft)  => SnackPosition.BottomLeft,
            nameof(SnackPosition.BottomRight) => SnackPosition.BottomRight,
            _                                 => SnackPosition.BottomCenter
        };
    }
}
