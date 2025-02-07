namespace Masa.Blazor.Mixins.Menuable;

public record IMenuable2(
    bool Top,
    bool Right,
    bool Bottom,
    bool Left,
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
    string? Origin,
    string? ZIndex,
    bool IsDefaultAttach
);