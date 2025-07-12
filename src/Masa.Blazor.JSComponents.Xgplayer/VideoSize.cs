namespace Masa.Blazor.JSComponents.Xgplayer;

public record VideoSize(
    [property: JsonPropertyName("cWidth")]
    double ContentWidth,
    [property: JsonPropertyName("cHeight")]
    double ContentHeight,
    [property: JsonPropertyName("vWidth")]
    double VideoWidth,
    [property: JsonPropertyName("vHeight")]
    double VideoHeight,
    [property: JsonPropertyName("videoScale")]
    double VideoScale)
{
}