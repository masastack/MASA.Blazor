namespace BlazorComponent;

public class EventListenerOptions
{
    public bool Capture { get; set; }

    public bool Once { get; set; }

    public bool Passive { get; set; }
}

public class EventListenerExtras
{
    public string? Key { get; set; }

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

    public EventListenerExtras(string key)
    {
        Key = key;
    }

    public EventListenerExtras(int debounce)
    {
        Debounce = debounce;
    }

    public EventListenerExtras(string key, int debounce)
    {
        Key = key;
        Debounce = debounce;
    }

    public EventListenerExtras(int debounce, int throttle) : this(debounce)
    {
        Throttle = throttle;
    }
}
