namespace Masa.Blazor;

public class EventListenerOptions
{
    public bool Capture { get; set; }

    public bool Once { get; set; }

    public bool Passive { get; set; }

    public EventListenerOptions()
    {
    }

    public EventListenerOptions(bool passive)
    {
        Passive = passive;
    }
}

public class EventListenerExtras
{
    public bool StopPropagation { get; set; }

    public bool PreventDefault { get; set; }

    public int Throttle { get; set; }

    public int Debounce { get; set; }

    public EventListenerExtras()
    {
    }

    public EventListenerExtras(bool stopPropagation, bool preventDefault)
    {
        StopPropagation = stopPropagation;
        PreventDefault = preventDefault;
    }

    public EventListenerExtras(int debounce)
    {
        Debounce = debounce;
    }

    public EventListenerExtras(int debounce, int throttle) : this(debounce)
    {
        Throttle = throttle;
    }
}
