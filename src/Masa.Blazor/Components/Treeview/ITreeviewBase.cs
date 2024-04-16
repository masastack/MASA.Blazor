namespace Masa.Blazor;

public interface ITreeviewBase<TItem, TKey>
{
    bool Activatable { get; set; }
    string? ActiveClass { get; set; }
    bool Selectable { get; set; }
    string? SelectedColor { get; set; }
    string? Color { get; set; }
    string? ExpandIcon { get; set; }
    string? IndeterminateIcon { get; set; }
    string? OffIcon { get; set; }
    string? OnIcon { get; set; }
    string? LoadingIcon { get; set; }
    Func<TItem, TKey> ItemKey { get; set; }
    Func<TItem, string> ItemText { get; set; }
    Func<TItem, bool>? ItemDisabled { get; set; }
    Func<TItem, List<TItem>> ItemChildren { get; set; }
    EventCallback<TItem> LoadChildren { get; set; }
    bool OpenOnClick { get; set; }
    bool Rounded { get; set; }
    bool Shaped { get; set; }
    SelectionType SelectionType { get; set; }
    RenderFragment<TreeviewItem<TItem>>? AppendContent { get; set; }
    RenderFragment<TreeviewItem<TItem>>? LabelContent { get; set; }
    RenderFragment<TreeviewItem<TItem>>? PrependContent { get; set; }
}