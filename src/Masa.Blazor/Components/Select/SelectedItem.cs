namespace Masa.Blazor.Components.Select;

/// <summary>
/// Represents a selected item in a select-based component.
/// </summary>
/// <param name="Item">The selected item, it may be null in <see cref="MCombobox{TItem,TValue}"/>.</param>
/// <param name="InputText">Optional user input text, used in <see cref="MCombobox{TItem,TValue}"/> when no item is matched.</param>
/// <typeparam name="TItem"></typeparam>
public record SelectedItem<TItem>(TItem? Item, string? InputText)
{
    public SelectedItem(TItem item) : this(item, null)
    {
    }
}