namespace Masa.Blazor.Popup.Components;

internal record ToastOptions(
    string? Content,
    string? Icon,
    ToastType Type = ToastType.Default,
    bool DisableOutsideClick = false);

public enum ToastType
{
    Default,
    Success,
    Error,
    Loading
}