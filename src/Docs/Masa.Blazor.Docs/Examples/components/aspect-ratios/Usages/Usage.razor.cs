namespace Masa.Blazor.Docs.Examples.components.aspect_ratios;

public class Usage : Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => nameof(MResponsive);

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MResponsive.AspectRatio), new SelectParameter(new List<string>() { "16/9", "4/3" }, "16/9") },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MResponsive.ContentClass), new CheckboxParameter("primary white--text", false) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        switch (parameter.Key)
        {
            case nameof(MResponsive.AspectRatio):
                var str = parameter.Value.ToString();
                if (str is not null && str.Contains('/'))
                {
                    var res = str.Split('/');
                    var v1 = Convert.ToDouble(res[0]);
                    var v2 = Convert.ToDouble(res[1]);
                    return (StringNumber)(v1 / v2);
                }

                return parameter.Value;
            default:
                return parameter.Value;
        }
    }
}
