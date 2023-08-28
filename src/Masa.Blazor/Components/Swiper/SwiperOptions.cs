using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class SwiperOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Pagination { get; set; }
}
