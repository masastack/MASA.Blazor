using Masa.Blazor.Components.Cascader;

namespace Masa.Blazor;

public partial class MCascader<TItem, TValue> : MSelect<TItem, TValue, TValue>
{
    [Parameter] public bool ChangeOnSelect { get; set; }

    [Parameter, MasaApiParameter(true)] public bool ShowAllLevels { get; set; } = true;

    [Parameter, EditorRequired] public Func<TItem, List<TItem>> ItemChildren { get; set; } = null!;

    [Parameter] public Func<TItem, Task>? LoadChildren { get; set; }

    [Parameter] public override bool Outlined { get; set; }

    [Parameter]
    [MasaApiParameter("/", ReleasedOn = "v1.5.0")]
    public string Delimiter { get; set; } = "/";

    private double _right;
    private List<TItem> _selectedCascadeItems = new();
    private List<MCascaderColumn<TItem, TValue>> _cascaderLists = new();

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

        var valueItem = SelectedItems.FirstOrDefault();
        if (valueItem is not null)
        {
            _selectedCascadeItems.Clear();
            FindAllLevelItems(valueItem, ComputedItems, ref _selectedCascadeItems);
            _selectedCascadeItems.Reverse();
        }
    }

    private Block _block = new("m-cascader");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.GenerateCssClasses()
        );
    }

    public void Register(MCascaderColumn<TItem, TValue> cascaderColumn)
    {
        _cascaderLists.Add(cascaderColumn);
    }

    private async Task HandleOnSelect((TItem item, bool isLast, int columnIndex) args)
    {
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
            var right = Document.DocumentElement.ClientWidth - MMenu.Dimensions.Activator.Right;

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

        await _cascaderLists.ForEachAsync(async cascaderList => await cascaderList.ScrollToSelected());
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
            var children = ItemChildren(item);
            if (children != null && children.Any())
            {
                var childrenSelectedItems = FindSelectedItems(children).ToList();
                if (childrenSelectedItems.Any())
                {
                    return childrenSelectedItems;
                }
            }
        }

        return Array.Empty<TItem>();
    }

    protected override string? GetText(TItem item)
    {
        return !ShowAllLevels
            ? base.GetText(item)
            : string.Join($" {Delimiter} ", _selectedCascadeItems.Select(base.GetText));
    }

    private bool FindAllLevelItems(TItem item, IList<TItem> searchItems, ref List<TItem> allLevelItems)
    {
        allLevelItems ??= new List<TItem>();

        if (searchItems.Contains(item))
        {
            allLevelItems.Add(item);
            return true;
        }

        foreach (var searchItem in searchItems)
        {
            var children = ItemChildren(searchItem);
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