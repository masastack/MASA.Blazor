
namespace Masa.Docs.Shared.Examples.skeleton_loaders.Usages;

public class Usage : Components.Usage
{
    protected override Type UsageWrapperType => typeof(UsageWrapper);

    public Usage() : base(typeof(MSkeletonLoader))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MSkeletonLoader.Type), "card" },
            { nameof(MSkeletonLoader.MaxWidth), (StringNumber)300 },
            { nameof(MSkeletonLoader.Class), "mx-auto" },
        };
    }
}