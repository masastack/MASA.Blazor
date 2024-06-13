namespace Masa.Blazor;

public class ItemContext
{
    public bool Active { get; init; }

    public string? ActiveClass { get; set; }

    public Func<Task> Toggle { get; init; }

    public ForwardRef Ref { get; set; }

    public StringNumber? Value { get; set; }

    public ItemContext(bool active, string? activeClass, Func<Task> toggle, ForwardRef @ref, StringNumber? value)
    {
        Active = active;
        ActiveClass = activeClass;
        Toggle = toggle;
        Ref = @ref;
        Value = value;
    }
}