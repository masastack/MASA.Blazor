namespace Masa.Blazor.Mixins.ScrollStrategy;

public class ScrollStrategyOptions
{
    public ScrollStrategyOptions(ScrollStrategy strategy, bool contained)
    {
        Strategy = strategy;
        Contained = contained;
    }

    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public ScrollStrategy Strategy { get; set; }

    public bool Contained { get; set; }
}

public enum ScrollStrategy
{
    Block
}