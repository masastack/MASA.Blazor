namespace Masa.Blazor;

public partial class MSelectList<TItem, TItemValue, TValue> : MasaComponentBase
{
    [Parameter] public bool Action { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool NoFilter { get; set; }

    [Parameter] public string? SearchInput { get; set; }

    [Parameter] public EventCallback<TItem> OnSelect { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public RenderFragment<SelectListItemProps<TItem>>? ItemContent { get; set; }

    [Parameter] public bool HideSelected { get; set; }

    [Parameter, EditorRequired] public IList<TItem> Items { get; set; } = null!;

    [Parameter] public Func<TItem, bool>? ItemDisabled { get; set; }

    [Parameter, EditorRequired] public Func<TItem, string> ItemText { get; set; } = null!;

    [Parameter, EditorRequired] public Func<TItem, TItemValue?> ItemValue { get; set; } = null!;

    [Parameter] public Func<TItem, bool>? ItemDivider { get; set; }

    [Parameter] public Func<TItem, string>? ItemHeader { get; set; }

    [Parameter] public string? NoDataText { get; set; }

    [Parameter] public IEnumerable<TItem> SelectedItems { get; set; } = null!;

    [Parameter] public RenderFragment? NoDataContent { get; set; }

    [Parameter] public RenderFragment? PrependItemContent { get; set; }

    [Parameter] public RenderFragment? AppendItemContent { get; set; }

    [Parameter] public int SelectedIndex { get; set; }

    private IList<TItemValue?> ParsedItems => SelectedItems.Select(item => ItemValue(item)).ToList();

    protected bool HasItem(TItem item)
    {
        return ParsedItems.IndexOf(ItemValue.Invoke(item)) > -1;
    }

    protected string? TileActiveClass => new CssBuilder().AddTextColor(Color).Class;

    public bool GetDisabled(TItem item)
    {
        return ItemDisabled != null && ItemDisabled(item);
    }

    public string GetFilteredText(TItem item)
    {
        var text = ItemText(item);

        if (SearchInput == null || NoFilter)
        {
            return text;
        }

        var (start, middle, end) = GetMaskedCharacters(text);
        return $"{start}{GetHighlight(middle)}{end}";
    }

    protected static string GetHighlight(string middle)
    {
        return $"<span class=\"m-list-item__mask\">{middle}</span>";
    }

    protected (string, string, string) GetMaskedCharacters(string text)
    {
        var searchInput = (SearchInput ?? "").ToLowerInvariant();
        var index = text.ToLowerInvariant().IndexOf(searchInput, StringComparison.Ordinal);

        if (index == -1)
        {
            return (text, "", "");
        }

        var start = text[..index];
        var middle = text.Substring(index, searchInput.Length);
        var end = text[(index + searchInput.Length)..];

        return (start, middle, end);
    }
}