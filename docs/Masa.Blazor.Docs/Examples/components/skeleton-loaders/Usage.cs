namespace Masa.Blazor.Docs.Examples.components.skeleton_loaders;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MSkeletonLoader))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSkeletonLoader.Boilerplate), false }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        {
            nameof(MSkeletonLoader.Type),
            new SelectParameter(["card", "paragraph", "list-item-avatar", "article", "card-avatar"], "card")
        }
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MSkeletonLoader.Elevation), new SliderParameter(1, 0, 24, false) }
    };

    protected override Dictionary<string, object>? GenAdditionalParameters() => new()
    {
        { nameof(MSkeletonLoader.MinWidth), (StringNumber)300 }
    };

    protected override IEnumerable<string> AdditionalParameters => ["MinWidth=\"300\""];

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MSkeletonLoader.Elevation) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }
}