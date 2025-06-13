namespace Masa.Blazor;

public class ActivatorProps(Dictionary<string, object> attrs)
{
    public Dictionary<string, object> Attrs { get; internal set; } = attrs;

    /// <summary>
    /// Merges the current trigger attributes with another set of attributes.
    /// </summary>
    /// <param name="other">The other set of attributes to merge with.</param>
    /// <returns>Returns a new dictionary containing the merged attributes.</returns>
    public Dictionary<string, object> MergeAttrs(Dictionary<string, object> other)
    {
        var merged = new Dictionary<string, object>(Attrs);

        foreach (var kvp in other)
        {
            merged[kvp.Key] = kvp.Value;
        }

        return merged;
    }
}