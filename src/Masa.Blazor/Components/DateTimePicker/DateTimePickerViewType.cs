namespace Masa.Blazor.Presets;

public enum DateTimePickerViewType
{
    /// <summary>
    /// Using menu and default view on desktop, dialog and compact view on mobile
    /// </summary>
    Auto,

    /// <summary>
    /// Always using compact view, but menu on desktop and dialog on mobile
    /// </summary>
    Compact,

    /// <summary>
    /// Always using dialog, but default view on desktop and compact view on mobile
    /// </summary>
    Dialog,

    /// <summary>
    /// Always using menu and default view
    /// </summary>
    Desktop,

    /// <summary>
    /// Always using dialog and compact view
    /// </summary>
    Mobile
}
