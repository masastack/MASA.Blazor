using YamlDotNet.Core.Tokens;

namespace Masa.Blazor.Docs.Examples.components.icons;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    private const string InternalIconParameter = "Icon";

    public Usage() : base(typeof(MIcon))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MIcon.XSmall), false },
        { nameof(MIcon.Small), false },
        { nameof(MIcon.Dense), false },
        { nameof(MIcon.Large), false },
        { nameof(MIcon.XLarge), false },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { InternalIconParameter, new SelectParameter(new List<string>() { "mdi-plus", "mdi-minus", "mdi-access-point", "mdi-antenna" }) },
        { nameof(MIcon.Color), new SelectParameter(new List<string>() { "red", "orange", "yellow", "green", "blue", "purple" }) },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MIcon.Disabled), new CheckboxParameter("false", true) },
    };

    protected override IEnumerable<ParameterItem<SelectParameter>> ActiveSelectParameters =>
        base.ActiveSelectParameters.Where(p => p.Key != InternalIconParameter);

    private string? Icon => base.ActiveSelectParameters.FirstOrDefault(p => p.Key == InternalIconParameter)?.Value.Value ?? "mdi-heart";

    protected override RenderFragment GenChildContent() => builder => { builder.AddContent(0, Icon); };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MIcon.Size) => (StringNumber)(string)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override string? ChildContentSourceCode => Icon;
}
