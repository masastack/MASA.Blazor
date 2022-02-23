using BlazorComponent;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Doc.Components
{
    public class Dom
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("rect")]
        public BoundingClientRect Rect { get; set; }
    }
}
