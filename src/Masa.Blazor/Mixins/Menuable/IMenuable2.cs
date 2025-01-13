namespace Masa.Blazor.Mixins.Menuable;

public record IMenuable2(
    bool Absolute,
    bool Auto,
    bool OffsetY,
    string? NudgeWidth,
    string? NudgeTop,
    string? NudgeBottom,
    bool OffsetOverflow,
    bool AllowOverflow,
    string? MinWidth,
    string? MaxWidth,
    string? MaxHeight,
    bool IsRtl,
    string? ContentStyle,
    string? Origin
);