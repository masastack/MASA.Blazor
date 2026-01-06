namespace Masa.Blazor.Components.Drawflow;

public class MouseUpEventArgs : MouseEventArgs
{
    public ConnectionStartEventArgs? ConnectionStart { get; set; }
}