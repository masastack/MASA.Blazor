using System.Text.Json.Serialization;
using Masa.Blazor.JSInterop;

namespace Masa.Blazor.JSComponents.DriverJS;

public record Config(
    DriverStep[]? Steps,
    bool Animate,
    string? OverlayColor,
    bool SmoothScroll,
    bool AllowClose,
    float OverlayOpacity,
    [property: JsonConverter(typeof(JsonCamelStringEnumConverter))]
    OverlayClickBehavior OverlayClickBehavior,
    int StagePadding,
    int StageRadius,
    bool AllowKeyboardControl,
    bool DisableActiveInteraction,
    string? PopoverClass,
    int PopoverOffset,
    [property: JsonIgnore]
    PopoverButton[]? ShowButtons,
    [property: JsonIgnore]
    PopoverButton[]? DisableButtons,
    bool ShowProgress,
    string? ProgressText,
    string? NextBtnText,
    string? PrevBtnText,
    string? DoneBtnText)
{
    [JsonPropertyName("showButtons")]
    public string[]? ShowButtonsInternal =>
        ShowButtons?.Select(button => button.ToString().ToLowerInvariant()).ToArray();

    [JsonPropertyName("disableButtons")]
    public string[]? DisableButtonsInternal =>
        DisableButtons?.Select(button => button.ToString().ToLowerInvariant()).ToArray();
}

public enum OverlayClickBehavior
{
    Close,
    NextStep
}