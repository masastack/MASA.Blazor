namespace Masa.Blazor.Presets;

public class StackPageData
{
    public StackPageData(string absolutePath)
    {
        Stacked = true;
        AbsolutePath = absolutePath;
        Pattern = "^" + absolutePath.ToLower() + "$";
    }

    public StackPageData(string pattern, string absolutePath)
    {
        Stacked = true;
        AbsolutePath = absolutePath;
        IsSelf = true;
        Pattern = pattern.ToLower();
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public string AbsolutePath { get; private set; }

    public string Pattern { get; init; }

    public bool IsSelf { get; init; }

    /// <summary>
    /// Indicates whether the current page is already on the stack.
    /// From the component point of view, indicates whether the
    /// Dialog component is displayed. 
    /// </summary>
    public bool Stacked { get; set; }

    public string Selector { get; set; }

    public object? State { get; set; }

    public void UpdateState(object? state) => State = state;

    public void UpdatePath(string absolutePath) => AbsolutePath = absolutePath;
}