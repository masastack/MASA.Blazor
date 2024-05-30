namespace Masa.Blazor;

public class ForwardRef
{
    public ElementReference Current { get; private set; }

    public void Set(ElementReference value)
    {
        Current = value;
    }
}
