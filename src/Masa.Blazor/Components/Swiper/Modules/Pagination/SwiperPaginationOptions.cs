using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class SwiperPaginationOptions
{
    public SwiperPaginationOptions(
        string el,
        SwiperPaginationType type,
        bool dynamicBullets,
        int dynamicMainBullets)
    {
        El = el;
        Type = type;
        DynamicBullets = dynamicBullets;
        DynamicMainBullets = dynamicMainBullets;
    }

    public string El { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
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
