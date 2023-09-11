using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class IntersectInvoker : Invoker<IntersectEventArgs>
{
    public IntersectInvoker(Func<IntersectEventArgs, Task> func) : base(func)
    {
    }
}

public class IntersectEventArgs: EventArgs
{
    /// <summary>
    /// Determines whether the target element intersects with the intersection observer's root.
    /// </summary>
    public bool IsIntersecting { get; }

    [JsonConstructor]
    public IntersectEventArgs(bool isIntersecting)
    {
        IsIntersecting = isIntersecting;
    }
}
