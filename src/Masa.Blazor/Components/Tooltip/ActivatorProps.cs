namespace Masa.Blazor;

public class ActivatorProps(Dictionary<string, object> attrs)
{
    public Dictionary<string, object> Attrs { get; set; } = attrs;

    /// <summary>
    /// Merges the current trigger attributes with another set of attributes.
    /// </summary>
    /// <param name="other">The other set of attributes to merge with.</param>
    /// <returns>Returns a new dictionary containing the merged attributes.</returns>
    public Dictionary<string, object> MergeAttrs(Dictionary<string, object> other)
    {
        return new Dictionary<string, object>()
            .Concat(Attrs)
            .Concat(other)
            .GroupBy(p => p.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);
    }
}