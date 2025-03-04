using Masa.Blazor.Components.Cascader;

namespace Masa.Blazor;

public partial class MCascader<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>,
    ICascader<TItem, TItemValue>
{
    [Parameter] public bool ChangeOnSelect { get; set; }

    [Parameter, MasaApiParameter(true)] public bool ShowAllLevels { get; set; } = true;

    [Parameter, EditorRequired] public Func<TItem, List<TItem>>? ItemChildren { get; set; }

    [Parameter] public Func<TItem, Task>? LoadChildren { get; set; }

    [Parameter] public override bool Outlined { get; set; }

    [Parameter]
    [MasaApiParameter("/", ReleasedOn = "v1.5.0")]
    public string Delimiter { get; set; } = "/";

    private TValue? _prevValue;
    private double _right;
    private List<TItem> _selectedItems = [];
    private List<TItem> _displaySelectedItems = [];
    private List<MCascaderColumn<TItem, TItemValue>> _cascaderLists = [];

    public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

    protected override IList<TItem> SelectedItems => FindSelectedItems(Items).ToList();

    protected override BMenuProps GetDefaultMenuProps()
    {
        var props = base.GetDefaultMenuProps();
        props.OffsetY = true;
        props.MinWidth = (StringNumber)(Dense ? 120 : 180);
        props.CloseOnContentClick = false;

        if (MasaBlazor.RTL)
        {
            props.ContentStyle = $"right: {_right}px; left: unset;";
        }

        return props;
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ItemChildren.ThrowIfNull(ComponentName);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (typeof(TValue) == typeof(TItemValue))
        {
            if (!EqualityComparer<TValue>.Default.Equals(_prevValue, Value))
            {
                _prevValue = Value;
                _selectedItems.Clear();

                var valueItem = SelectedItems.FirstOrDefault();
                if (valueItem is not null)
                {
                    _selectedItems.Clear();
                    FindAllLevelItems(valueItem, ComputedItems, ref _selectedItems);
                    _selectedItems.Reverse();
                    _displaySelectedItems = [.._selectedItems];
                }
            }
        }
        else
        {
            if (Value is IList<TItemValue> value)
            {
                var notEqual = _prevValue is null ||
                               _prevValue is IList<TItemValue> prevValue && !prevValue.SequenceEqual(value);
                if (notEqual)
                {
                    _prevValue = Value;
                    _selectedItems.Clear();
                    TItem? targetItem = default;

                    foreach (var itemValue in value)
                    {
                        if (targetItem is null)
                        {
                            targetItem = ComputedItems.FirstOrDefault(i =>
                                EqualityComparer<TItemValue>.Default.Equals(ItemValue(i), itemValue));
                        }
                        else
                        {
                            if (ItemChildren is not null)
                            {
                                targetItem = ItemChildren(targetItem).FirstOrDefault(i =>
                                    EqualityComparer<TItemValue>.Default.Equals(ItemValue(i), itemValue));
                            }
                        }

                        if (targetItem is null)
                        {
                            break;
                        }

                        _selectedItems.Add(targetItem);
                    }

                    _displaySelectedItems = [.._selectedItems];
                }
            }
        }
    }

    private static Block _block = new("m-cascader");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat([_block.Name]);
    }

    public void Register(MCascaderColumn<TItem, TItemValue> cascaderColumn)
    {
        _cascaderLists.Add(cascaderColumn);
    }

    private async Task HandleOnSelect((TItem item, bool isLast, int columnIndex) args)
    {
        _displaySelectedItems.RemoveRange(args.columnIndex, _displaySelectedItems.Count - args.columnIndex);
        _displaySelectedItems.Add(args.item);

        if (ChangeOnSelect)
        {
            await SelectItem(args.item, closeOnSelect: args.isLast);
        }
        else if (args.isLast)
        {
            await SelectItem(args.item);
        }

        NextTick(async () =>
        {
            var selector =
                $"{MMenu.ContentElement.GetSelector()} .m-cascader__column:nth-child({args.columnIndex + 1})";
            await Js.ScrollIntoParentView(selector, true, true);
        });
    }

    protected override async Task SelectItem(TItem item, bool closeOnSelect = true)
    {
        _selectedItems = _displaySelectedItems.ToList();

        if (typeof(TValue) == typeof(TItemValue))
        {
            var value = ItemValue(item);
            if (value is TValue val)
            {
                // await ValueChanged.InvokeAsync(val);
                await SetValue(val);
                _prevValue = val;
            }
        }
        else
        {
            var values = _selectedItems.Select(ItemValue).ToList();
            if (values is TValue val)
            {
                // await ValueChanged.InvokeAsync(val);
                await SetValue(val);
                _prevValue = val;
            }
        }

        if (closeOnSelect)
        {
            IsMenuActive = false;
        }

        _ = OnSelect.InvokeAsync((item, true));
    }

    protected override async Task OnMenuBeforeShowContent()
    {
        await base.OnMenuBeforeShowContent();

        _cascaderLists.ForEach(cascaderList => cascaderList.ActiveSelectedOrNot());
    }

    protected override async Task OnMenuAfterShowContent(bool isLazyContent)
    {
        await base.OnMenuAfterShowContent(isLazyContent);

        if (MasaBlazor.RTL && MMenu is not null)
        {
            var right = MMenu.DocumentClientWidth - MMenu.Dimensions.Activator.Right;

            if (MMenu.NudgeLeft is not null)
            {
                var (_, number) = MMenu.NudgeLeft.TryGetNumber();
                right -= number;
            }

            if (MMenu.NudgeRight != null)
            {
                var (_, number) = MMenu.NudgeRight.TryGetNumber();
                right += number;
            }

            if (Math.Abs(_right - right) > 0.1)
            {
                _right = right;
                StateHasChanged();
            }
        }

        await ScrollToInlineStartAsync();

        foreach (var cascaderList in _cascaderLists)
        {
            await cascaderList.ScrollToSelected();
        }
    }

    private async Task ScrollToInlineStartAsync()
    {
        if (MMenu.ContentElement.Context is not null)
        {
            await Js.ScrollTo($"{MMenu.ContentElement.GetSelector()} > .m-cascader__columns", top: null, left: 0);
        }
    }

    private IEnumerable<TItem> FindSelectedItems(IList<TItem> items)
    {
        var selectedItems = items.Where(item => InternalValues.Contains(ItemValue(item))).ToList();
        if (selectedItems.Any())
        {
            return selectedItems;
        }

        foreach (var item in items)
        {
            var children = ItemChildren?.Invoke(item);
            if (children is not { Count: > 0 }) continue;

            var childrenSelectedItems = FindSelectedItems(children).ToList();
            if (childrenSelectedItems.Count > 0)
            {
                return childrenSelectedItems;
            }
        }

        return Array.Empty<TItem>();
    }

    protected override string? GetText(TItem item)
    {
        return !ShowAllLevels
            ? base.GetText(item)
            : string.Join($" {Delimiter} ", _selectedItems.Select(base.GetText));
    }

    private bool FindAllLevelItems(TItem item, IList<TItem> searchItems, ref List<TItem> allLevelItems)
    {
        if (searchItems.Contains(item))
        {
            allLevelItems.Add(item);
            return true;
        }

        foreach (var searchItem in searchItems)
        {
            var children = ItemChildren?.Invoke(searchItem);
            if (children is { Count: > 0 })
            {
                var find = FindAllLevelItems(item, children, ref allLevelItems);
                if (find)
                {
                    allLevelItems.Add(searchItem);
                    return true;
                }
            }
        }

        return false;
    }
}