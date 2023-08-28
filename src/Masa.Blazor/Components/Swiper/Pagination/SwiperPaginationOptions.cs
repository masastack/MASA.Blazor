using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class SwiperPaginationOptions
{
    public SwiperPaginationOptions(
        ElementReference elementReference,
        SwiperPaginationType type,
        bool dynamicBullets,
        int dynamicMainBullets)
    {
        ElementReference = elementReference;
        Selector = elementReference.GetSelector();
        Type = type;
        DynamicBullets = dynamicBullets;
        DynamicMainBullets = dynamicMainBullets;
    }

    [JsonIgnore]
    // [JsonPropertyName("el")]
    public ElementReference ElementReference { get; }

    [JsonPropertyName("el")]
    public string Selector { get; }

    public SwiperPaginationType Type { get; }

    public bool DynamicBullets { get; }

    public int DynamicMainBullets { get; }
}

public enum SwiperPaginationType
{
    Bullets,
    Fraction,
    Progressbar,
    Custom
}
