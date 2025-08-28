namespace Masa.Blazor.Components.DateTimePicker;

/// <summary>
/// Shortcut context for date/time picker shortcuts
/// </summary>
/// <param name="Update">An action to update the selected date/time</param>
public record ShortcutContext(Action<DateTime> Update);