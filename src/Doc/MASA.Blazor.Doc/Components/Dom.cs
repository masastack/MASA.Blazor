using BlazorComponent;
using System.Text.Json.Serialization;

namespace MASA.Blazor.Doc.Components
{
    public class Dom
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("rect")]
        public BoundingClientRect Rect { get; set; }
    }
}
