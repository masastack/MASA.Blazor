using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class EChartsInitOptions
{
    private bool _useDirtyRect;

    public double? DevicePixelRatio { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EChartsRenderer Renderer { get; set; } = EChartsRenderer.SVG;

    /// <summary>
    /// Support from echarts 5.3.0
    /// </summary>
    public bool SSR { get; set; }

    /// <summary>
    /// Support from echarts 5.0.0
    /// </summary>
    public bool UseDirtyRect
    {
        get => _useDirtyRect;
        set
        {
            _useDirtyRect = value;

            if (value)
            {
                Renderer = EChartsRenderer.Canvas;
            }
        }
    }

    [JsonPropertyName("width")]
    public string? InternalWidth => Width?.ToString();

    [JsonPropertyName("height")]
    public string? InternalHeight => Height?.ToString();

    [JsonIgnore]
    public StringNumber? Width { get; set; }

    [JsonIgnore]

    public StringNumber? Height { get; set; }

    /// <summary>
    /// Support from echarts 5.0.0
    /// </summary>
    public string? Locale { get; set; }
}

public enum EChartsRenderer
{
    Canvas,
    SVG
}
