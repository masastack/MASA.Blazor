namespace Masa.Blazor.JSComponents.DriverJS;

public record Config(
    DriverStep[]? Steps,
    bool Animate,
    string? OverlayColor,
    bool SmoothScroll,
    bool AllowClose,
    float OverlayOpacity,
    int StagePadding,
    int StageRadius,
    bool AllowKeyboardControl,
    bool DisableActiveInteraction,
    string? PopoverClass,
    int PopoverOffset,
    string[]? ShowButtons,
    // string[]? DisableButtons,
    bool ShowProgress,
    string? ProgressText,
    string? NextBtnText,
    string? PrevBtnText,
    string? DoneBtnText);