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

    private bool _menuValue;

    private string _inputValue;
    private string _inputSearch;
    private string _treeViewSearch;

    private List<TItem> _activeItems = new();

    private TItem _lastActiveItem;

    private List<TKey> ActiveKeys => _activeItems.Select(ItemKey).ToList();

    private bool IsMultiple => Multiple || Selectable;

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

            ResetInput();
        }
    }

    private Func<TItem, string, Func<TItem, string>, bool> HandleOnFilter
    {
        get { return (item, search, textKey) => textKey(item).IndexOf(search) > -1; }
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

    private void InputValueChanged(string val)
    {
        _inputSearch = val;

        if (_menuValue)
        {
            _treeViewSearch = val;
        }
    }

    private void MenuValueChanged(bool val)
    {
        _menuValue = val;

        if (val)
        {
            _treeViewSearch = null;
        }
    }

    private void ResetInput()
    {
        _inputSearch = null;
    }
}
