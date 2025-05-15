using Masa.Blazor.Components.DataTable;

namespace Masa.Blazor;

public partial class MDataIterator<TItem> : MData<TItem>
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    [Parameter]
    public RenderFragment<(int PageStart, int PageStop, int ItemsLength)>? PageTextContent { get; set; }

    [Parameter]
    public Action<IDataFooterParameters>? FooterProps { get; set; }

    [Parameter]
    public Func<TItem, string>? ItemKey { get; set; }

    [Parameter]
    public string? Color { get; set; }

    [Parameter]
    public bool SingleSelect { get; set; }

    [Parameter]
    public List<TItem>? Expanded { get; set; }

    [Parameter]
    public EventCallback<List<TItem>> ExpandedChanged { get; set; }

    [Parameter]
    public bool SingleExpand { get; set; }

    [Parameter]
    public StringBoolean? Loading { get; set; }

    [Parameter]
    public string? NoResultsText { get; set; }

    [Parameter]
    public string? NoDataText { get; set; }

    [Parameter]
    public string? LoadingText { get; set; }

    [Parameter]
    public bool HideDefaultFooter { get; set; }

    [Parameter]
    public Func<TItem, bool>? SelectableKey { get; set; }

    [Parameter]
    public RenderFragment<(IEnumerable<TItem> Items, Func<TItem, bool> IsExpanded, Action<TItem, bool> Expand)>? ChildContent { get; set; }

    [Parameter]
    public RenderFragment<ItemProps<TItem>>? ItemContent { get; set; }

    [Parameter]
    public RenderFragment? LoadingContent { get; set; }

    [Parameter]
    public RenderFragment? NoDataContent { get; set; }

    [Parameter]
    public RenderFragment? NoResultsContent { get; set; }

    [Parameter]
    public RenderFragment? HeaderContent { get; set; }

    [Parameter]
    public RenderFragment? FooterContent { get; set; }

    [Parameter]
    public StringNumber LoaderHeight { get; set; } = 4;

    [Parameter]
    public RenderFragment? ProgressContent { get; set; }

    // NEXT: This parameter should be the keys of selected items. not selected items.
    /// <summary>
    /// Gets or sets the selected items.
    /// </summary>
    [Parameter, Obsolete("Use Selected instead.")] 
    public IEnumerable<TItem>? Value { get; set; }

    [Parameter, Obsolete("Use SelectedChanged instead.")]
    public EventCallback<IEnumerable<TItem>> ValueChanged { get; set; }

    // NEXT: This parameter should be Value, but it's not possible to have two parameters with the same name.
    // This will be changed in next major version.  
    // And the type of Selected should be generic type(TKey) instead of string.
    /// <summary>
    /// Gets or sets the selected items by their keys.
    /// </summary>
    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.7.0")]
    public IEnumerable<string>? Selected { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.7.0")]
    public EventCallback<IEnumerable<string>> SelectedChanged { get; set; }

    [Parameter]
    public EventCallback<(TItem, bool)> OnItemSelect { get; set; }

    [Parameter]
    public EventCallback<(TItem, bool)> OnItemExpand { get; set; }

    [Parameter]
    public EventCallback<(IEnumerable<TItem>, bool)> OnToggleSelectAll { get; set; }

    private IEnumerable<TItem>? _prevValue;
    private IEnumerable<string>? _prevSelected;
    private IEnumerable<TItem>? _prevExpanded;

    public bool EveryItem => SelectableItems.Any() && SelectableItems.All(IsSelected);

    public bool SomeItems => SelectableItems.Any(IsSelected);

    public IEnumerable<TItem> SelectableItems => ComputedItems.Where(IsSelectable);

    private Dictionary<string, bool> Expansion { get; set; } = new();

    private Dictionary<SelectionKey<TItem>, bool> Selection { get; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateSelection(init: true);
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        NoResultsText = I18n.T("$masaBlazor.dataIterator.noResultsText");
        NoDataText = I18n.T("$masaBlazor.noDataText");
        LoadingText = I18n.T("$masaBlazor.dataIterator.loadingText");

        return base.SetParametersAsync(parameters);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UpdateSelection();
    }

    private void UpdateSelection(bool init = false)
    {
        if (!init && ReferenceEquals(_prevSelected, Selected) && ReferenceEquals(_prevValue, Value))
        {
            return;
        }

        _prevSelected = Selected;
        _prevValue = Value;

        List<string>? keys = null;

        if (Value is not null)
        {
            keys = [];
            foreach (var item in Value)
            {
                var key = ItemKey?.Invoke(item);
                if (key is null) continue;

                SelectionKey<TItem> selectionKeyKey = new(key, item);
                Selection[selectionKeyKey] = true;
                keys.Add(key);
            }
        }
        else if (Selected is not null)
        {
            keys = [];
            foreach (var key in Selected)
            {
                SelectionKey<TItem> selectionKeyKey = new(key, default);
                Selection[selectionKeyKey] = true;
                keys.Add(key);
            }
        }

        if (keys is null)
        {
            return;
        }

        // Unselect those in selection but not in Value when updating Value
        foreach (var (key, _) in Selection)
        {
            Selection[key] = keys.Contains(key.Key);
        }
    }

    private static Block _block = new("m-data-iterator");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }

    protected bool IsExpanded(TItem item)
    {
        var key = ItemKey?.Invoke(item);
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        if (!Equals(_prevExpanded, Expanded))
        {
            _prevExpanded = Expanded;

            if (Expanded is null)
            {
                Expansion.Clear();
            }
            else
            {
                Expansion = Expanded.ToDictionary(ItemKey!, _ => true);
            }
        }

        return Expansion.TryGetValue(key, out var expanded) && expanded;
    }

    protected void Expand(TItem item, bool value = true)
    {
        if (SingleExpand)
        {
            Expansion.Clear();
            Expanded?.Clear();
        }

        var key = ItemKey?.Invoke(item);
        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("ItemKey or key should not be null");
        }

        Expansion[key] = value;

        OnItemExpand.InvokeAsync((item, value));

        var expanded = Expanded ?? new List<TItem>();
        if (value)
        {
            expanded.Add(item);
        }
        else
        {
            expanded.Remove(item);
        }

        ExpandedChanged.InvokeAsync(expanded);
    }

    protected bool IsSelected(TItem item)
    {
        var key = ItemKey?.Invoke(item);
        if (key == null)
        {
            return false;
        }

        SelectionKey<TItem> selectionKey = new(key, item);
        return Selection.GetValueOrDefault(selectionKey, false);
    }

    protected void Select(TItem item, bool value = true)
    {
        if (!IsSelectable(item))
        {
            return;
        }

        if (SingleSelect)
        {
            Selection.Clear();
        }

        var key = ItemKey?.Invoke(item);
        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("ItemKey or key should not be null");
        }

        SelectionKey<TItem> selectionKey = new(key, item);

        Selection[selectionKey] = value;

        UpdateSelectedItemsAsValue();

        OnItemSelect.InvokeAsync((item, value));
    }

    protected bool IsSelectable(TItem item)
    {
        return SelectableKey?.Invoke(item) is null or true;
    }

    protected void ToggleSelectAll(bool value)
    {
        foreach (var item in SelectableItems)
        {
            var key = ItemKey?.Invoke(item);
            if (key == null) continue;
            SelectionKey<TItem> selectionKey = new(key, item);
            Selection[selectionKey] = value;
        }

        UpdateSelectedItemsAsValue();

        OnToggleSelectAll.InvokeAsync((SelectableItems, value));
    }

    private void UpdateSelectedItemsAsValue()
    {
        var selected = Selection.Where(u => u.Value).ToList();

        if (ValueChanged.HasDelegate)
        {
            _ = ValueChanged.InvokeAsync(selected.Select(u => u.Key.Item)!);
        }
        else if (SelectedChanged.HasDelegate)
        {
            _ = SelectedChanged.InvokeAsync(selected.Select(u => u.Key.Key));
        }
    }
}
