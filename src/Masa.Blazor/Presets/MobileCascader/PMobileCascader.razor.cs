using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Presets;

// TODO: add class constraints for TItem after c# 7.0 release

public partial class PMobileCascader<TItem, TItemValue> // where TItem : class
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Inject] private IJSRuntime Js { get; set; } = null!;

    [Parameter] public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter, EditorRequired] public List<TItem> Items { get; set; } = null!;

    [Parameter, EditorRequired] public Func<TItem, List<TItem>> ItemChildren { get; set; } = null!;

    [Parameter] public Func<TItem, bool>? ItemDisabled { get; set; }

    [Parameter, EditorRequired] public Func<TItem, string> ItemText { get; set; } = null!;

    [Parameter, EditorRequired] public Func<TItem, TItemValue> ItemValue { get; set; } = null!;

    [Parameter] public EventCallback<TItem> OnLoadChildren { get; set; }

    [Parameter] public EventCallback<List<TItem>> OnConfirm { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public List<TItemValue>? Value { get; set; }

    [Parameter] public EventCallback<List<TItemValue>?> ValueChanged { get; set; }

    [Parameter] public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value) return;

            OnVisibleChanged(value);
            _visible = value;
        }
    }

    [Parameter] public EventCallback<bool> VisibleChanged { get; set; }

    private int _tabIndex;
    private TItem? _loadingItem;
    private List<TItem> _loadedItems = new();
    private bool _visible;

    private List<TItem> SelectedItems { get; set; } = new();

    private List<string> Tabs { get; set; } = new();

    private string PleaseSelectText => I18n.T("$masaBlazor.mobileCascader.pleaseSelect");

    private TItem? Current => SelectedItems.Count > 0 ? SelectedItems.Last() : default;

    private List<TItem>? Children => Current == null ? null : ItemChildren(Current);

    private bool HasChildren => Children is not null && Children.Count > 0;

    private List<TItem> CurrentItems
    {
        get
        {
            if (_tabIndex == -1)
            {
                return new List<TItem>();
            }

            if (_tabIndex == 0)
            {
                return Items;
            }

            if (SelectedItems.Count == _tabIndex)
            {
                return ItemChildren(Current!);
            }

            return ItemChildren(SelectedItems[_tabIndex - 1]);
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Items.ThrowIfNull(nameof(PMobileCascader<TItem, TItemValue>));
        ItemChildren.ThrowIfNull(nameof(PMobileCascader<TItem, TItemValue>));
        ItemText.ThrowIfNull(nameof(PMobileCascader<TItem, TItemValue>));
        ItemValue.ThrowIfNull(nameof(PMobileCascader<TItem, TItemValue>));
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Value ??= new List<TItemValue>();
    }

    private async Task OnItemClick(TItem item)
    {
        if (SelectedItems.Count > _tabIndex)
        {
            // cancel the selection
            SelectedItems = SelectedItems.Take(_tabIndex).ToList();
        }

        SelectedItems.Add(item);

        _tabIndex++;

        var children = ItemChildren(item);

        if (!_loadedItems.Contains(item) && OnLoadChildren.HasDelegate && (children is null || children.Count == 0))
        {
            _loadingItem = item;
            _loadedItems.Add(item);

            FormatTabs();

            try
            {
                await OnLoadChildren.InvokeAsync(item);
            }
            catch
            {
                _loadingItem = default;
                _loadedItems.Remove(item);
            }
            finally
            {
                _loadingItem = default;
            }

            children = ItemChildren(item);
        }

        if (children == null || children.Count == 0)
        {
            _tabIndex--;
        }

        FormatTabs();
    }

    private bool IsLoading => !EqualityComparer<TItem>.Default.Equals(_loadingItem, default);

    private bool IsDisabled(TItem item)
    {
        return ItemDisabled?.Invoke(item) is true || item!.Equals(_loadingItem);
    }

    private async Task OnCancel()
    {
        if (VisibleChanged.HasDelegate)
        {
            await VisibleChanged.InvokeAsync(false);
        }
        else
        {
            Visible  = false;
        }
    }

    private async Task HandleOnConfirm()
    {
        var value = SelectedItems.Select(ItemValue).ToList();

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }

        if (OnConfirm.HasDelegate)
        {
            _ = OnConfirm.InvokeAsync(SelectedItems);
        }

        await OnCancel();
    }

    private void FormatTabs()
    {
        if (SelectedItems.Count == 0)
        {
            Tabs = new List<string>() { PleaseSelectText };
        }
        else
        {
            var tabs = SelectedItems.Select(t => ItemText(t)).ToList();
            if (HasChildren || IsLoading)
            {
                tabs.Add(PleaseSelectText);
            }

            Tabs = tabs;
        }
    }

    private async Task HandleVisibleChanged(bool val)
    {
        if (val)
        {
            await Js.InvokeVoidAsync(JsInteropConstants.AddCls, "html", "overflow-y-hidden");
        }
        else
        {
            await Js.InvokeVoidAsync(JsInteropConstants.RemoveCls, "html", "overflow-y-hidden");
        }

        if (VisibleChanged.HasDelegate)
        {
            await VisibleChanged.InvokeAsync(val);
        }
        else
        {
            Visible = val;
        }
    }

    private void OnVisibleChanged(bool visible)
    {
        if (!visible) return;

        if (Value is null || Value.Count <= 0)
        {
            FormatTabs();
            return;
        }

        SelectedItems.Clear();

        var items = Items;

        for (var index = 0; index < Value.Count; index++)
        {
            var v = Value[index];
            if (!TryGetItem(items, v, out var item))
            {
                break;
            }

            SelectedItems.Add(item);
            FormatTabs();

            _tabIndex = index;

            var children = ItemChildren(item);
            if (children is null || children.Count == 0)
            {
                break;
            }

            items = children;

            // If it is the last loop and there are child elements,
            // need to activate the next tab
            if (index == Value.Count - 1)
            {
                _tabIndex++;
            }
        }

        FormatTabs();
    }

    private bool TryGetItem(List<TItem> items, TItemValue value, [NotNullWhen(true)] out TItem? item)
    {
        item = default;

        var index = items.FindIndex(item => EqualityComparer<TItemValue>.Default.Equals(ItemValue(item), value));
        if (index == -1)
        {
            return false;
        }

        item = items[index];

        return item != null;
    }
}
