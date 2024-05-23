namespace Masa.Blazor.Components.OptInput;

internal class OtpInputEventArgs<T> where T : EventArgs
{
    public T Args { get; set; }

    public int Index { get; set; }

    public OtpInputEventArgs(T args)
    {
        Args = args;
    }

    public OtpInputEventArgs(T args, int index)
    {
        Args = args;
        Index = index;
    }
}