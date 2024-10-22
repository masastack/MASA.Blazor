namespace Masa.Blazor.Mixins.ScrollStrategy;

public class ScrollStrategyOptions(Blazor.ScrollStrategy strategy, bool contained = false)
{
    [JsonConverter(typeof(JsonCamelStringEnumConverter))]
    public Blazor.ScrollStrategy Strategy { get; set; } = strategy;

    public bool Contained { get; set; } = contained;
}