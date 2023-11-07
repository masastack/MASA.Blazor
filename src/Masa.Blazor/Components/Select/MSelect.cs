using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using BlazorComponent.Attributes;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor;

public class MSelect<TItem, TItemValue, TValue> : MTextField<TValue>, ISelect<TItem, TItemValue, TValue>, IOutsideClickJsCallback
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    [Inject]
    private OutsideClickJSModule OutsideClickJSModule { get; set; } = null!;

    [Parameter]
    [MassApiParameter("$dropdown")]
    public override string? AppendIcon { get; set; } = "$dropdown";

    [Parameter]
    [MassApiParameter(false)]
    public StringBoolean? Attach { get; set; } = false;

    [Parameter]
    public bool CacheItems { get; set; }

    [Parameter]
    public bool Chips { get; set; }

    [Parameter]
    public bool DeletableChips { get; set; }

    //TODO: DisableLookup,Eager

    [Parameter]
    public bool HideSelected { get; set; }

    [Parameter, EditorRequired]
    public IList<TItem> Items
    {
        get => GetValue((IList<TItem>)new List<TItem>(), disableIListAlwaysNotifying: true)!;
        set => SetValue(value, disableIListAlwaysNotifying: true);
    }

    [Parameter]
    [MassApiParameter("primary")]
    public string ItemColor { get; set; } = "primary";

    [Parameter]
    public Func<TItem, bool>? ItemDisabled { get; set; }

    [Parameter, EditorRequired]
    public Func<TItem, string> ItemText { get; set; } = null!;

    [Parameter, EditorRequired]
    public Func<TItem, TItemValue?> ItemValue { get; set; } = null!;

    [Parameter]
    public Action<BMenuProps>? MenuProps { get; set; }

    [Parameter]
    public bool Eager { get; set; }

    [Parameter]
    public bool Multiple
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    //TODO:OpenOnClear

    [Parameter]
    public bool SmallChips { get; set; }

    //TODO:remove this
    [Parameter]
    public StringNumber? MinWidth { get; set; }

    [Parameter]
    public string? NoDataText { get; set; }

    [Parameter]
    [Obsolete("Use OnSelect instead.")]
    public EventCallback<TItem> OnSelectedItemUpdate { get; set; }

    [Parameter]
    public EventCallback<(TItem Item, bool Selected)> OnSelect { get; set; }

    [Parameter]
    public RenderFragment? AppendItemContent { get; set; }

    [Parameter]
    public RenderFragment<SelectListItemProps<TItem>>? ItemContent { get; set; }

    [Parameter]
    public RenderFragment? NoDataContent { get; set; }

    [Parameter]
    public RenderFragment? PrependItemContent { get; set; }

    [Parameter]
    public RenderFragment<SelectSelectionProps<TItem>>? SelectionContent { get; set; }

    bool ISelect<TItem, TItemValue, TValue>.HasChips => HasChips;

    IList<TItem> ISelect<TItem, TItemValue, TValue>.ComputedItems => ComputedItems;

    IList<TItemValue> ISelect<TItem, TItemValue, TValue>.InternalValues => InternalValues;

    object ISelect<TItem, TItemValue, TValue>.Menu
    {
        set => Menu = value;
    }

    IList<TItem> ISelect<TItem, TItemValue, TValue>.SelectedItems => SelectedItems;

    string? ISelect<TItem, TItemValue, TValue>.GetText(TItem item) => GetText(item);

    TItemValue? ISelect<TItem, TItemValue, TValue>.GetValue(TItem item) => GetValue(item);

    bool ISelect<TItem, TItemValue, TValue>.GetDisabled(TItem item) => GetDisabled(item);

    private static Func<TItem, string?> ItemHeader { get; } = GetFuncOrDefault<string>("Header");

    private static Func<TItem, bool> ItemDivider { get; } = GetFuncOrDefault<bool>("Divider");
    
    private bool _onClearInvoked;
    private bool _onSelectItemInvoked;

    private IList<TItem> CachedItems { get; set; } = new List<TItem>();

    protected bool IsMenuActive
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    protected int MenuListIndex { get; private set; } = -1;

    public int SelectedIndex { get; set; } = -1;

    protected object? Menu { get; set; }

    protected MMenu? MMenu => Menu as MMenu;

    protected BMenuProps ComputedMenuProps
    {
        get
        {
            var defaults = GetDefaultMenuProps();
            MenuProps?.Invoke(defaults);
            if (defaults.OffsetY && defaults.NudgeBottom is null)
            {
                defaults.NudgeBottom = 1;
            }

            return defaults;
        }
    }

    protected bool HasChips => Chips || SmallChips;

    protected override bool IsDirty => SelectedItems.Count > 0;

    public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

    protected override Dictionary<string, object?> InputAttrs => new()
    {
        { "type", Type },
        { "value", null },
        { "readonly", true }
    };

    protected List<TItem> AllItems => FilterDuplicates(CachedItems.Concat(Items)).ToList();

    protected virtual List<TItem> ComputedItems => AllItems;

    protected List<TItem> ComputedItemsIfHideSelected =>
        HideSelected ? ComputedItems.Where(item => !SelectedItems.Contains(item)).ToList() : ComputedItems;

    protected IList<TItemValue> InternalValues
    {
        get
        {
            return InternalValue switch
            {
                IList<TItemValue> values => values,
                TItemValue value => new List<TItemValue> { value },
                _ => new List<TItemValue>()
            };
        }
    }

    protected virtual IList<TItem> SelectedItems { get; set; } = new List<TItem>();

    protected string TilesSelector =>
        $"{MMenu!.ContentElement.GetSelector()} .m-list-item, {MMenu.ContentElement.GetSelector()} .m-divider, {MMenu.ContentElement.GetSelector()} .m-subheader";

    protected virtual bool MenuCanShow => true;

    protected override bool DisableSetValueByJsInterop => true;

    protected virtual BMenuProps GetDefaultMenuProps() => new()
    {
        CloseOnClick = false,
        CloseOnContentClick = false,
        DisableKeys = true,
        OpenOnClick = false,
        MaxHeight = 304
    };

    protected virtual string? GetText(TItem? item) => item is null ? null : ItemText(item);

    protected TItemValue? GetValue(TItem item) => ItemValue(item);

    protected bool GetDisabled(TItem item) => ItemDisabled?.Invoke(item) is true;

    protected virtual bool EnableSpaceKeDownPreventDefault => true;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        NoDataText = I18n.T("$masaBlazor.noDataText");

        await base.SetParametersAsync(parameters);

        Items.ThrowIfNull(ComponentName);
        ItemText.ThrowIfNull(ComponentName);
        ItemValue.ThrowIfNull(ComponentName);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        CachedItems = CacheItems ? Items : new List<TItem>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var keys = new List<string> { KeyCodes.ArrowUp, KeyCodes.ArrowDown, KeyCodes.Home, KeyCodes.End, KeyCodes.Enter, KeyCodes.Escape };

            if (EnableSpaceKeDownPreventDefault)
            {
                keys.Add(KeyCodes.Space);
            }

            await JsInvokeAsync(JsInteropConstants.EnablePreventDefaultForEvent, InputElement, "keydown", keys);
        }
        else
        {
            GenMenu();
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<IList<TItem>>(nameof(Items), _ => OnItemsChange(), immediate: true)
               .Watch<bool>(nameof(IsMenuActive), IsMenuActiveChanged);
    }

    private async void IsMenuActiveChanged(bool val)
    {
        if (val) return;

        if (_onSelectItemInvoked || _onClearInvoked)
        {
            await TryInvokeFieldChangeOfInputsFilter(isClear: _onClearInvoked);

            _onClearInvoked = false;
            _onSelectItemInvoked = false;
        }
    }

    private void OnItemsChange()
    {
        if (CacheItems)
        {
            NextTick(() =>
            {
                CachedItems = FilterDuplicates(CachedItems.Concat(Items));
                StateHasChanged();
            });
        }

        NextTickIf(SetSelectedItems, () => ItemValue is null);

        StateHasChanged();
    }

    private IList<TItem> FilterDuplicates(IEnumerable<TItem> list)
    {
        var uniqueKeys = new List<TItemValue>();
        var uniqueItems = new List<TItem>();

        foreach (var item in list)
        {
            if (item is null)
            {
                continue;
            }

            if (ItemDivider(item) || ItemHeader(item) is not null)
            {
                uniqueItems.Add(item);
                continue;
            }

            var val = GetValue(item);
            if (!uniqueKeys.Contains(val))
            {
                uniqueKeys.Add(val);
                uniqueItems.Add(item);
            }
        }

        return uniqueItems;
    }

    private void GenMenu()
    {
        if (MMenu is not null && InputSlotAttrs.Keys.Count == 0)
        {
            InputSlotAttrs = MMenu.ActivatorAttributes;
            MMenu.BeforeShowContent ??= OnMenuBeforeShowContent;
            MMenu.AfterShowContent ??= OnMenuAfterShowContent;

            if (OutsideClickJSModule != null)
            {
                _ = OutsideClickJSModule.InitializeAsync(this, InputSlotElement.GetSelector()!);
            }

            StateHasChanged();
        }
    }

    private StringBoolean? GetMenuAttach()
    {
        if (Attach is null) return null;

        if ((Attach.IsT0 && string.IsNullOrWhiteSpace(Attach.AsT0)) || (Attach.IsT1 && Attach.AsT1))
        {
            return Ref.GetSelector();
        }

        return Attach;
    }

    protected virtual Task OnMenuBeforeShowContent()
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnMenuAfterShowContent(bool isLazyContent)
    {
        if (isLazyContent)
        {
            if (OutsideClickJSModule?.Initialized is true && MMenu!.ContentElement.Context is not null)
            {
                _ = OutsideClickJSModule.UpdateDependentElements(InputSlotElement.GetSelector()!, MMenu.ContentElement.GetSelector()!);
            }
        }

        OnMenuActiveChange(true);

        return Task.CompletedTask;
    }

    protected virtual async void OnMenuActiveChange(bool val)
    {
        if ((Multiple && !val) || GetMenuIndex() > -1)
        {
            return;
        }

        if (MMenu!.ContentElement.Context is null || !IsDirty) return;

        var index = await JsInvokeAsync<int>(JsInteropConstants.GetListIndexWhereAttributeExists,
            TilesSelector,
            "aria-selected", "True");

        if (index > -1)
        {
            await SetMenuIndex(index);

            StateHasChanged();
        }
    }

    public async Task Blur()
    {
        var prevIsMenuActive = IsMenuActive;
        var prevIsFocused = IsFocused;
        var prevSelectedIndex = SelectedIndex;
        var prevMenuListIndex = MenuListIndex;

        IsMenuActive = false;
        IsFocused = false;
        SelectedIndex = -1;
        await SetMenuIndex(-1);

        if (prevIsMenuActive || prevIsFocused || prevSelectedIndex != -1 || prevMenuListIndex != -1)
        {
            StateHasChanged();
        }
    }

    public void ActivateMenu()
    {
        if (!IsInteractive || IsMenuActive)
        {
            return;
        }

        IsMenuActive = true;
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Merge(cssBuilder =>
            {
                cssBuilder
                    .Add("m-select")
                    .AddIf("m-select--is-menu-active", () => IsMenuActive)
                    .AddIf("m-select--is-multi", () => Multiple)
                    .AddIf("m-select--chips", () => Chips)
                    .AddIf("m-select--chips--small", () => SmallChips)
                    .AddTheme(IsDark);
            }, styleBuilder =>
            {
                styleBuilder
                    .AddMinWidth(MinWidth);
            })
            .Apply("select-slot", cssBuilder =>
            {
                cssBuilder
                    .Add("m-select__slot");
            })
            .Apply("selections", cssBuilder =>
            {
                cssBuilder
                    .Add("m-select__selections")
                    .AddTextColor(TextColor);
            }, styleBuilder =>
            {
                styleBuilder.AddTextColor(TextColor);
            })
            .Apply("selection-comma", cssBuilder =>
            {
                //TODO: color,disabled
                cssBuilder
                    .Add("m-select__selection")
                    .Add("m-select__selection--comma");
            })
            .Apply("input-wrapper", cssBuilder =>
            {
                cssBuilder
                    .Add("m-select__selections__input-wrapper");
            });

        AbstractProvider
            .ApplySelectDefault<TItem, TItemValue, TValue>()
            .Merge<BIcon, MIcon>("append-icon", attrs =>
            {
                var dic = new Dictionary<string, object?>();

                if (attrs.Data is not null)
                {
                    // Don't allow the dropdown icon to be focused
                    var onClick = (EventCallback<MouseEventArgs>)attrs.Data;
                    if (onClick.HasDelegate)
                    {
                        dic["tabindex"] = -1;
                    }
                }

                dic["aria-hidden"] = "true";
                dic["aria-label"] = null;

                attrs[nameof(MIcon.Attributes)] = dic;
            })
            .Apply<BMenu, MMenu>(attrs =>
            {
                attrs[nameof(MMenu.ExternalActivator)] = true;
                attrs[nameof(MMenu.Value)] = MenuCanShow && IsMenuActive;
                attrs[nameof(MMenu.ValueChanged)] = EventCallback.Factory.Create<bool>(this, v =>
                {
                    IsMenuActive = v;
                    IsFocused = v;
                });
                attrs[nameof(MMenu.Attach)] = GetMenuAttach();
                attrs[nameof(MMenu.Disabled)] = Disabled || Readonly;
                attrs[nameof(MMenu.Eager)] = Eager;
                attrs[nameof(MMenu.Auto)] = ComputedMenuProps!.Auto;
                attrs[nameof(MMenu.Bottom)] = ComputedMenuProps.Bottom;
                attrs[nameof(MMenu.CloseOnClick)] = ComputedMenuProps.CloseOnClick;
                attrs[nameof(MMenu.CloseOnContentClick)] = ComputedMenuProps.CloseOnContentClick;
                attrs[nameof(MMenu.DisableKeys)] = ComputedMenuProps.DisableKeys;
                attrs[nameof(MMenu.Left)] = ComputedMenuProps.Left;
                attrs[nameof(MMenu.MaxHeight)] = ComputedMenuProps.MaxHeight;
                attrs[nameof(MMenu.MaxWidth)] = ComputedMenuProps.MaxWidth;
                attrs[nameof(MMenu.MinWidth)] = ComputedMenuProps.MinWidth;
                attrs[nameof(MMenu.NudgeTop)] = ComputedMenuProps.NudgeTop;
                attrs[nameof(MMenu.NudgeRight)] = ComputedMenuProps.NudgeRight;
                attrs[nameof(MMenu.NudgeBottom)] = ComputedMenuProps.NudgeBottom;
                attrs[nameof(MMenu.NudgeLeft)] = ComputedMenuProps.NudgeLeft;
                attrs[nameof(MMenu.NudgeWidth)] = ComputedMenuProps.NudgeWidth;
                attrs[nameof(MMenu.OffsetX)] = ComputedMenuProps.OffsetX;
                attrs[nameof(MMenu.OffsetY)] = ComputedMenuProps.OffsetY;
                attrs[nameof(MMenu.OffsetOverflow)] = ComputedMenuProps.OffsetOverflow;
                attrs[nameof(MMenu.OpenOnClick)] = ComputedMenuProps.OpenOnClick;
                attrs[nameof(MMenu.Right)] = ComputedMenuProps.Right;
                attrs[nameof(MMenu.Top)] = ComputedMenuProps.Top;
                attrs[nameof(MMenu.Transition)] = ComputedMenuProps.Transition;
            })
            .Apply(typeof(BSelectList<,,>), typeof(MSelectList<TItem, TItemValue, TValue>), attrs =>
            {
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.Action)] = Multiple;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.Color)] = ItemColor;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.Dense)] = Dense;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.HideSelected)] = HideSelected;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.Items)] = ComputedItems;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemDisabled)] = ItemDisabled;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemText)] = ItemText;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemValue)] = ItemValue;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.NoDataText)] = NoDataText;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.SelectedItems)] = SelectedItems;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.OnSelect)] = CreateEventCallback<TItem>(item => SelectItem(item));
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemContent)] = ItemContent;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.PrependItemContent)] = PrependItemContent;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.AppendItemContent)] = AppendItemContent;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.SelectedIndex)] = MenuListIndex;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.NoDataContent)] = NoDataContent;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemDivider)] = ItemDivider;
                attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemHeader)] = ItemHeader;
            })
            .Apply<BChip, MChip>(attrs =>
            {
                var index = -1;
                TItem? item = default;
                if (attrs.Data is (TItem t, int i))
                {
                    item = t;
                    index = i;
                }

                var isDisabled = item != null && GetDisabled(item);
                var isInteractive = !isDisabled && IsInteractive;

                attrs[nameof(MChip.Close)] = DeletableChips && isInteractive;
                attrs[nameof(MChip.Disabled)] = isDisabled;
                attrs[nameof(MChip.Class)] = "m-chip--select";
                attrs[nameof(MChip.Attributes)] = new Dictionary<string, object>() { { "tabindex", -1 } };
                attrs[nameof(MChip.Small)] = SmallChips;
                attrs[nameof(MChip.IsActive)] = index == SelectedIndex;
                attrs[nameof(MChip.OnClick)] = CreateEventCallback<MouseEventArgs>(_ =>
                {
                    if (!isInteractive) return;
                    SelectedIndex = index;
                });
                attrs[nameof(MChip.OnClickStopPropagation)] = true;
                attrs[nameof(MChip.OnCloseClick)] = CreateEventCallback<MouseEventArgs>(_ => OnChipInput(item!));
            });
    }

    protected override void OnInternalValueChange(TValue val)
    {
        base.OnInternalValueChange(val);

        NextTick(SetSelectedItems);

        if (Multiple)
        {
            NextTick(() =>
            {
                // TODO: need this?
                // await MMenu.UpdateDimensionsAsync();

                return Task.CompletedTask;
            });
        }

        StateHasChanged();
    }

    protected async Task SelectItemByIndex(int index)
    {
        if (index > -1 && index < ComputedItems.Count)
        {
            var item = ComputedItems[index];
            await SelectItem(item);
        }
    }

    protected virtual async Task SelectItem(TItem item, bool closeOnSelect = true)
    {
        _onSelectItemInvoked = true;

        bool isSelected;

        var value = ItemValue(item);
        if (!Multiple)
        {
            if (value is TValue val)
            {
                await SetValue(val);
            }

            if (closeOnSelect)
            {
                IsMenuActive = false;
            }

            isSelected = true;
        }
        else
        {
            var internalValues = InternalValues.ToList();
            if (internalValues.Contains(value))
            {
                internalValues.Remove(value);
                isSelected = false;
            }
            else
            {
                internalValues.Add(value);
                isSelected = true;
            }

            if (internalValues is TValue val)
            {
                await SetValue(val);
            }
        }

        if (HideSelected)
        {
            await SetMenuIndex(-1);
        }
        else
        {
            var index = ComputedItems.IndexOf(item);
            if (index > -1)
            {
                await SetMenuIndex(index);
            }
        }

        _ = OnSelectedItemUpdate.InvokeAsync(item);

        _ = OnSelect.InvokeAsync((item, isSelected));
    }

    public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
    {
        if (IsReadonly && args.Key != KeyCodes.Tab) return;

        if (OnKeyDown.HasDelegate)
        {
            await OnKeyDown.InvokeAsync(args);
        }

        var keyCode = args.Key;

        // If menu is active, allow default
        // listIndex change from menu
        if (IsMenuActive && new[] { KeyCodes.ArrowUp, KeyCodes.ArrowDown, KeyCodes.Home, KeyCodes.End, KeyCodes.Enter }.Contains(keyCode))
        {
            NextTick(async () =>
            {
                await ChangeMenuListIndex(keyCode);
                StateHasChanged();
            });
        }

        // If enter, space, open menu
        if (new[] { KeyCodes.Enter, KeyCodes.Space }.Contains(keyCode))
        {
            ActivateMenu();
        }

        // If menu is not active, up/down/home/end can do
        // one of 2 things. If multiple, opens the
        // menu, if not, will cycle through all
        // available options
        if (!IsMenuActive && new[] { KeyCodes.ArrowUp, KeyCodes.ArrowDown, KeyCodes.Home, KeyCodes.End }.Contains(keyCode))
        {
            await OnUpDown(keyCode);
            return;
        }

        // If escape deactivate the menu
        if (keyCode == KeyCodes.Escape)
        {
            await OnEscDown();
            return;
        }

        // If tab - select item or close menu
        if (keyCode == KeyCodes.Tab)
        {
            await OnTabDown(args);
            return;
        }

        // If space preventDefault
        if (keyCode == KeyCodes.Space)
        {
            // invoke preventDefault at js interop
        }
    }

    private async Task ChangeMenuListIndex(string code)
    {
        if (code == KeyCodes.ArrowUp)
        {
            PrevTile();
        }
        else if (code == KeyCodes.ArrowDown)
        {
            NextTile();
        }
        else if (code == KeyCodes.Home)
        {
            FirstTile();
        }
        else if (code == KeyCodes.End)
        {
            LastTile();
        }
        else if (code == KeyCodes.Enter && MenuListIndex != -1)
        {
            var item = ComputedItemsIfHideSelected.ElementAtOrDefault(MenuListIndex);
            await SelectItem(item);
        }

        await SetMenuIndex(MenuListIndex);
    }

    private void NextTile()
    {
        var nextIndex = MenuListIndex + 1;

        if (nextIndex >= ComputedItemsIfHideSelected.Count)
        {
            if (ComputedItemsIfHideSelected.Count == 0) return;

            MenuListIndex = -1;
            NextTile();

            return;
        }

        var nextItem = ComputedItemsIfHideSelected.ElementAtOrDefault(nextIndex);

        MenuListIndex++;
        if (nextItem is not null && (ItemDivider(nextItem) || ItemHeader(nextItem) is not null || ItemDisabled?.Invoke(nextItem) is true))
        {
            NextTile();
        }
    }

    private void PrevTile()
    {
        var prevIndex = MenuListIndex - 1;

        if (prevIndex < 0)
        {
            if (ComputedItemsIfHideSelected.Count == 0) return;

            MenuListIndex = ComputedItemsIfHideSelected.Count;
            PrevTile();

            return;
        }

        var prevItem = ComputedItemsIfHideSelected.ElementAt(prevIndex);

        MenuListIndex--;
        if (ItemDivider(prevItem) || ItemHeader(prevItem) is not null || ItemDisabled?.Invoke(prevItem) is true)
        {
            PrevTile();
        }
    }

    private void LastTile()
    {
        var lastIndex = ComputedItemsIfHideSelected.Count - 1;

        if (lastIndex == -1) return;

        var lastItem = ComputedItemsIfHideSelected.ElementAt(lastIndex);

        MenuListIndex = lastIndex;

        if (ItemDivider(lastItem) || ItemHeader(lastItem) is not null || ItemDisabled?.Invoke(lastItem) is true)
        {
            PrevTile();
        }
    }

    private void FirstTile()
    {
        if (ComputedItemsIfHideSelected.Count == 0) return;

        var firstItem = ComputedItemsIfHideSelected.First();

        MenuListIndex = 0;

        if (ItemDivider(firstItem) || ItemHeader(firstItem) is not null || ItemDisabled?.Invoke(firstItem) is true)
        {
            NextTile();
        }
    }

    protected virtual async Task OnUpDown(string code)
    {
        // Multiple selects do not cycle their value
        // when pressing up or down, instead activate
        // the menu
        if (Multiple)
        {
            ActivateMenu();
            return;
        }

        // TODO: menu.hasClickableTiles

        await ChangeMenuListIndex(code);

        await SelectItemByIndex(MenuListIndex);
    }

    protected virtual Task OnEscDown()
    {
        if (IsMenuActive)
        {
            IsMenuActive = false;
        }

        return Task.CompletedTask;
    }

    protected virtual async Task OnTabDown(KeyboardEventArgs args)
    {
        // An item that is selected by
        // menu-index should toggled
        if (!Multiple && MenuListIndex != -1 && IsMenuActive)
        {
            // TODO: need e.preventDefault() and e.stopPropagation()?

            await SelectItemByIndex(MenuListIndex);
        }
        else
        {
            // If we make it here,
            // the user has no selected indexes
            // and is probably tabbing out
            await Blur();
        }
    }

    public override async Task HandleOnBlurAsync(FocusEventArgs args)
    {
        if (OnBlur.HasDelegate)
        {
            await OnBlur.InvokeAsync(args);
        }
    }

    public override async Task HandleOnClickAsync(ExMouseEventArgs args)
    {
        if (!IsInteractive) return;

        if (await IsAppendInner(args.Target!) is false)
        {
            IsMenuActive = true;
        }

        if (!IsFocused)
        {
            IsFocused = true;
            HasFocused = true;

            if (OnFocus.HasDelegate)
            {
                await OnFocus.InvokeAsync();
            }
        }

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    public override async Task HandleOnMouseUpAsync(ExMouseEventArgs args)
    {
        if (HasMouseDown && args.Button != 2 && IsInteractive)
        {
            // If append inner is present
            // and the target is itself
            // or inside, toggle menu
            if (await IsAppendInner(args.Target!))
            {
                IsMenuActive = !IsMenuActive;
            }
        }

        await base.HandleOnMouseUpAsync(args);
    }

    /// <summary>
    /// target is itself or inside
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    protected Task<bool> IsAppendInner(EventTarget target)
    {
        return JsInvokeAsync<bool>(JsInteropConstants.EqualsOrContains, AppendInnerElement, target.Selector);
    }

    public override async Task HandleOnClearClickAsync(MouseEventArgs args)
    {
        _onClearInvoked = true;
        
        var value = Multiple ? (TValue)(IList<TItemValue>)new List<TItemValue>() : default;

        await SetValue(value);

        if (OnClearClick.HasDelegate)
        {
            await OnClearClick.InvokeAsync(args);
        }

        if (!IsMenuActive)
        {
            // invoke the field change event of inputs filter when the menu is not active
            IsMenuActiveChanged(false);
        }

        await OnChange.InvokeAsync(value);

        await SetMenuIndex(-1);

        // whether to need NextTick?
        await InputElement.FocusAsync();

        // TODO: OpenOnClear
    }

    protected async Task SetMenuIndex(int index)
    {
        MenuListIndex = index;

        if (index == -1)
        {
            return;
        }

        var i = index;
        var menuItem = ComputedItems.ElementAtOrDefault(index);
        if (menuItem is not null)
        {
            i = ComputedItemsIfHideSelected.IndexOf(menuItem);
        }

        if (i > -1)
        {
            await JsInvokeAsync(JsInteropConstants.ScrollToTile,
                MMenu!.ContentElement.GetSelector(),
                TilesSelector,
                i);
        }
    }

    protected virtual void SetSelectedItems()
    {
        var selectedItems = new List<TItem>();

        var values = InternalValues;

        foreach (var value in values)
        {
            var index = AllItems.FindIndex(v => EqualityComparer<TItemValue>.Default.Equals(value, GetValue(v)));

            if (index > -1)
            {
                selectedItems.Add(AllItems[index]);
            }
        }

        SelectedItems = selectedItems;

        StateHasChanged();
    }

    protected int GetMenuIndex()
    {
        return MenuListIndex;
    }

    private async Task OnChipInput(TItem item)
    {
        if (Multiple)
        {
            await SelectItem(item);
        }
        else
        {
            await SetValue(default);
        }

        // if all items have been delete,
        // open menu
        IsMenuActive = SelectedItems.Count == 0;

        SelectedIndex = -1;
    }

    private static Func<TItem, T?> GetFuncOrDefault<T>(string name)
    {
        Func<TItem, T?> func;
        try
        {
            var parameterExpression = Expression.Parameter(typeof(TItem), "item");
            var propertyExpression = Expression.Property(parameterExpression, name);

            var lambdaExpression = Expression.Lambda<Func<TItem, T>>(propertyExpression, parameterExpression);
            func = lambdaExpression.Compile();
        }
        catch
        {
            func = _ => default;
        }

        return func;
    }

    protected async Task SetValue(TValue value)
    {
        if (!EqualityComparer<TValue>.Default.Equals(InternalValue, value))
        {
            InternalValue = value;
            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(value);
            }
        }
    }

    public async Task HandleOnOutsideClickAsync()
    {
        if (IsFocused || IsMenuActive)
        {
            await Blur();
        }
    }
}
