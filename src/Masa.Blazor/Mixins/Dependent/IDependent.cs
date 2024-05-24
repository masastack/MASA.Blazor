namespace Masa.Blazor.Mixins;

public interface IDependent
{
    void RegisterChild(IDependent dependent);

    IDependent? CascadingDependent { get; }

    public List<IDependent> CascadingDependents => CascadingDependent is not null
        ? CascadingDependent.CascadingDependents.Concat(new[] { CascadingDependent }).ToList()
        : new List<IDependent>();

    IEnumerable<string> DependentSelectors { get; }
}
