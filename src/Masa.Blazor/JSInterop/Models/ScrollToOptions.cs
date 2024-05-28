using System.Text.Json.Serialization;

namespace BlazorComponent.JSInterop;

public record ScrollToOptions
{
    public double? Left { get; }

    public double? Top { get; }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public ScrollBehavior Behavior { get; }

    public ScrollToOptions()
    {
    }

    public ScrollToOptions(double top)
    {
        Top = top;
    }

    public ScrollToOptions(double top, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        Top = top;
        Behavior = behavior;
    }

    public ScrollToOptions(double? left, double? top, ScrollBehavior behavior = ScrollBehavior.Smooth)
    {
        Left = left;
        Top = top;
        Behavior = behavior;
    }
}
