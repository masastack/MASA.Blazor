namespace Masa.Blazor.Presets;

public partial class PTreeSelect<TItem, TKey> : MTreeview<TItem, TKey>
{
    [Parameter]
    public bool Multiple { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Activatable = true;
        Dense = true;
        Hoverable = true;
        OpenAll = true;

        return base.SetParametersAsync(parameters);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsMultiple)
        {
            ActiveKeys.Clear();
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (IsMultiple)
            {
            }
            else
            {
                ActiveKeys = Value;

                if (Value.Count > 0)
                {
                    if (Nodes.TryGetValue(Value.Last(), out var val))
                    {
                        _inputValue = ItemText(val.Item);
                    }
                }

                StateHasChanged();
            }
        }
    }

    private bool _menuValue;

    private string _inputValue;
    private string _inputSearch;
    private string _treeViewSearch;

    private List<TItem> _activeItems = new();

    private TItem _lastActiveItem;

    private List<TKey> ActiveKeys { get; set; }

    private MAutocomplete<TItem, TKey, List<TKey>> Autocomplete { get; set; }

    private bool IsMultiple => Multiple || Selectable;

    private List<TItem> FlattenedItems => FlattenItems(Items);

    private async Task HandleOnActiveUpdate(List<TItem> items)
    {
        if (items.Count == 0)
        {
            return;
        }

        Console.WriteLine($"items.count:{items.Count}");

        if (IsMultiple)
        {
        }
        else
        {
            var item = items.LastOrDefault();
            _lastActiveItem = item;
            _inputValue = ItemText(item);

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(items.Select(ItemKey).ToList());
            }
            else
            {
                Value = items.Select(ItemKey).ToList();
            }

            _activeItems = items;

            ActiveKeys = _activeItems.Select(ItemKey).ToList();

            ResetInput();
        }

        await Autocomplete.Blur();
    }

    private Func<TItem, string, Func<TItem, string>, bool> HandleOnFilter
    {
        get { return (item, search, textKey) => textKey(item).IndexOf(search) > -1; }
    }

    private void OnSearchInputUpdate(string s)
    {
        _treeViewSearch = s;
    }

    private void HandleOnInputBlur()
    {
        if (IsMultiple)
        {
        }
        else
        {
            if (_lastActiveItem is not null && _activeItems.All(item => ItemText(item) != _inputValue))
            {
                _inputValue = ItemText(_lastActiveItem);
            }
        }

        ResetInput();
    }

    private async Task HandleValueChanged(List<TKey> val)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(val);
        }
        else
        {
            Value = val;
        }

        ActiveKeys = val;

        StateHasChanged();
    }

    private void ResetInput()
    {
        _inputSearch = null;
    }

    private List<TItem> FlattenItems(List<TItem> tree)
    {
        var res = new List<TItem>();

        foreach (var nav in tree)
        {
            res.Add(nav);

            var children = ItemChildren(nav);

            if (children is not null)
            {
                res.AddRange(FlattenItems(children));
            }
        }

        return res;
    }
}
