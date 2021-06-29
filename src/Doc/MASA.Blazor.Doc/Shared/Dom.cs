using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Shared
{
    public class Dom
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("rect")]
        public BoundingClientRect Rect { get; set; }
    }
}
