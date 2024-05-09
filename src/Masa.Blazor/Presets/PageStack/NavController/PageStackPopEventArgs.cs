namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPopEventArgs : EventArgs
{
    public PageStackPopEventArgs(int delta, object? state = null)
    {
        if (delta < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(delta), "The delta must be greater than or equal to 1.");
        }

        Delta = delta;
        State = state;
    }

    public int Delta { get; }

    public object? State { get; }
}