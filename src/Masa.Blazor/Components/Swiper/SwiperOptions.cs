using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class SwiperOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SwiperPaginationOptions? Pagination { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SwiperNavigationOptions? Navigation { get; set; }
}
