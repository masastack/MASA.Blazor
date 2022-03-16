namespace Masa.Blazor;

public class PopupOkEventArgs
{
    protected PopupOkEventArgs()
    {
        IsCanceled = false;
    }

    public bool IsCanceled { get; private set; }

    public void Cancel()
    {
        IsCanceled = true;
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