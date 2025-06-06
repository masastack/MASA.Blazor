namespace Masa.Blazor;

public class SelectSelectionProps<TItem>(
    TItem? item,
    int index,
    bool selected,
    bool disabled,
    string? text = null)
{
    /// <summary>
    /// The selected item.
    /// May be null if the context is from a custom input in a <see cref="MCombobox{TItem,TValue}"/> component.
    /// For custom inputs, use the <see cref="Text"/> property to get the user input.
    /// </summary>
    public TItem? Item { get; } = item;

    public int Index { get; } = index;

    public bool Selected { get; } = selected;

    public bool Disabled { get; } = disabled;

    /// <summary>
    /// The text of selected item or user input from the <see cref="MCombobox{TItem,TValue}"/> component.
    /// </summary>
    public string? Text { get; } = text;
}