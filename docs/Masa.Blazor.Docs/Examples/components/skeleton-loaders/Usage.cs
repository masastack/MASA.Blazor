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

    protected override Dictionary<string, object>? GenAdditionalParameters() => new()
    {
        { nameof(MSkeletonLoader.MinWidth), (StringNumber)300 }
    };

    protected override IEnumerable<string> AdditionalParameters => ["MinWidth=\"300\""];
}