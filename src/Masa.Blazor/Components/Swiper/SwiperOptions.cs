using System.Text.Json.Serialization;
using Masa.Blazor.Swiper.Modules;

namespace Masa.Blazor;

public class SwiperOptions
{
    public bool AllowTouchMove { get; set; }

    public bool AutoHeight { get; set; }

    public string? Direction { get; set; }

    public bool Loop { get; set; }

    public bool Parallax { get; set; }

    public int SpaceBetween { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SwiperAutoplayOptions? Autoplay { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SwiperPaginationOptions? Pagination { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SwiperNavigationOptions? Navigation { get; set; }
}
