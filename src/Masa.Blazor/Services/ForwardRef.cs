namespace BlazorComponent;

public class ForwardRef
{
    public ElementReference Current { get; private set; }

    public void Set(ElementReference value)
    {
        Current = value;
    }
}
