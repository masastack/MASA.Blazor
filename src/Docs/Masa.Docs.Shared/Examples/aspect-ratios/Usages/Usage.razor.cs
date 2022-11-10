
namespace Masa.Docs.Shared.Examples.aspect_ratios;

public class Usage : Components.Usage
{
    protected override Type UsageWrapperType => typeof(UsageWrapper);

    public Usage() : base(typeof(MResponsive))
    {
    }

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MResponsive.AspectRatio), new SelectParameter(new List<string>() { "16/9D", "4/3D" },"16/9D") },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MResponsive.ContentClass), new CheckboxParameter("false", false) },
    };

    protected override RenderFragment? GenChildContent() => builder =>
    {
        builder.OpenComponent<MCardText>(0);
        builder.AddChildContent(1, "\r\nLorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus! Eaque cupiditate minima, at placeat totam, magni doloremque veniam neque porro libero rerum unde voluptatem!");
        builder.CloseComponent();
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MResponsive.AspectRatio) => (StringNumber)(string)parameter.Value,
            _ => parameter.Value
        };
    }
}
