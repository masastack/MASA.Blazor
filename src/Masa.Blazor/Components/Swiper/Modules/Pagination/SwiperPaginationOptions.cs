using System.Text.Json.Serialization;

namespace Masa.Blazor.Swiper.Modules;

public class SwiperPaginationOptions
{
    public SwiperPaginationOptions(string el)
    {
        El = el;
    }

    public SwiperPaginationOptions(string el, MSwiperPagination pagination) : this(el)
    {
        Clickable = pagination.Clickable;
        DynamicBullets = pagination.DynamicBullets;
        DynamicMainBullets = pagination.DynamicMainBullets;
        HideOnClick = pagination.HideOnClick;
        Type = pagination.Type;
    }

    public string El { get; }

    public bool Clickable { get; set; }

    public bool DynamicBullets { get; }

    public int DynamicMainBullets { get; }

    public bool HideOnClick { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SwiperPaginationType Type { get; }
}