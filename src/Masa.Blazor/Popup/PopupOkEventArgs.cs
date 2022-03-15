namespace Masa.Blazor;

public class PopupOkEventArgs
{
    protected PopupOkEventArgs()
    {
        Cancelled = false;
    }

    public bool Cancelled { get; private set; }

    public void Cancel()
    {
        Cancelled = true;
    }
}

public class PopupOkEventArgs<T> : PopupOkEventArgs
{
    public PopupOkEventArgs(T value)
    {
        Value = value;
    }

    public T Value { get; }
}