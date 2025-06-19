namespace Masa.Blazor.Components.Window;

internal record TouchValue(Dictionary<string, AddEventListenerOptions> Options, bool Parent = false);

internal class AddEventListenerOptions : EventListenerOptions
{
    public bool StopPropagation { get; set; }
}