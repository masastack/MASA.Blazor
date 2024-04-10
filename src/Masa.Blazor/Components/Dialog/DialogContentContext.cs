namespace Masa.Blazor;

public class DialogContentContext
{
    public Action Close { get; init; }

    public DialogContentContext(Action close)
    {
        Close = close;
    }
}