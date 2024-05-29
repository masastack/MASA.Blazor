namespace Masa.Blazor.JSInterop;

public class Element
{
    [JsonPropertyName("absoluteTop")] public double AbsoluteTop { get; set; }

    [JsonPropertyName("absoluteLeft")] public double AbsoluteLeft { get; set; }

    [JsonPropertyName("relativeTop")] public double RelativeTop { get; set; }

    [JsonPropertyName("relativeBottom")] public double RelativeBottom { get; set; }

    [JsonPropertyName("relativeLeft")] public double RelativeLeft { get; set; }

    [JsonPropertyName("relativeRight")] public double RelativeRight { get; set; }

    [JsonPropertyName("offsetTop")] public double OffsetTop { get; set; }

    [JsonPropertyName("offsetLeft")] public double OffsetLeft { get; set; }

    [JsonPropertyName("offsetWidth")] public double OffsetWidth { get; set; }

    [JsonPropertyName("offsetHeight")] public double OffsetHeight { get; set; }

    [JsonPropertyName("scrollHeight")] public double ScrollHeight { get; set; }

    [JsonPropertyName("scrollWidth")] public double ScrollWidth { get; set; }

    [JsonPropertyName("scrollLeft")] public double ScrollLeft { get; set; }

    [JsonPropertyName("scrollTop")] public double ScrollTop { get; set; }

    [JsonPropertyName("clientTop")] public double ClientTop { get; set; }

    [JsonPropertyName("clientLeft")] public double ClientLeft { get; set; }

    [JsonPropertyName("clientHeight")] public double ClientHeight { get; set; }

    [JsonPropertyName("clientWidth")] public double ClientWidth { get; set; }

    [JsonPropertyName("selectionStart")] public double SelectionStart { get; set; }
}