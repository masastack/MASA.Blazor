using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MSelect<TItem, TItemValue, TValue> : MTextField<TValue>, ISelect<TItem, TItemValue, TValue>
    {
        [Parameter]
        public override string AppendIcon { get; set; } = "mdi-menu-down";

        //TODO:Attach,CacheItems

        [Parameter]
        public bool Chips { get; set; }

        [Parameter]
        public bool DeletableChips { get; set; }

        //TODO: DisableLookup,Eager

        [Parameter]
        public bool HideSelected { get; set; }

        [EditorRequired]
        [Parameter]
        public IList<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public string ItemColor { get; set; } = "primary";

        [Parameter]
        public Func<TItem, bool> ItemDisabled { get; set; } = item => false;

        [EditorRequired]
        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, TItemValue> ItemValue { get; set; }

        [Parameter]
        public Action<BMenuProps> MenuProps { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        //TODO:OpenOnClear

        [Parameter]
        public bool SmallChips { get; set; }

        //TODO:remove this
        [Parameter]
        public StringNumber MinWidth { get; set; }

        //Filterable
        [Parameter]
        public string NoDataText { get; set; } = "No data available";

        [Parameter]
        public EventCallback<TItem> OnSelectedItemUpdate { get; set; }

        [Parameter]
        public RenderFragment AppendItemContent { get; set; }

        [Parameter]
        public RenderFragment<SelectListItemProps<TItem>> ItemContent { get; set; }

        [Parameter]
        public RenderFragment NoDataContent { get; set; }

        [Parameter]
        public RenderFragment PrependItemContent { get; set; }

        [Parameter]
        public RenderFragment<SelectSelectionProps<TItem>> SelectionContent { get; set; }

        bool ISelect<TItem, TItemValue, TValue>.HasChips => HasChips;

        IList<TItem> ISelect<TItem, TItemValue, TValue>.ComputedItems => ComputedItems;

        IList<TItemValue> ISelect<TItem, TItemValue, TValue>.InternalValues => InternalValues;

        object ISelect<TItem, TItemValue, TValue>.Menu
        {
            set
            {
                Menu = value;
            }
        }

        IList<TItem> ISelect<TItem, TItemValue, TValue>.SelectedItems => SelectedItems;

        string ISelect<TItem, TItemValue, TValue>.GetText(TItem item) => GetText(item);

        TItemValue ISelect<TItem, TItemValue, TValue>.GetValue(TItem item) => GetValue(item);

        bool ISelect<TItem, TItemValue, TValue>.GetDisabled(TItem item) => GetDisabled(item);

        protected bool IsMenuActive { get; set; }

        protected int SelectedIndex { get; set; } = -1;

        public override int DebounceMilliseconds { get; set; }

        protected object Menu { get; set; }

        protected BMenuProps ComputedMenuProps { get; set; }

        protected bool HasChips => Chips || SmallChips;

        protected override bool IsDirty => SelectedItems.Count > 0;
        public override Action<TextFieldNumberProperty> NumberProps { get; set; }

        protected override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            { "value", null },
            { "readonly", true }
        };

        protected virtual IList<TItem> ComputedItems => Items;

        protected IList<TItemValue> InternalValues
        {
            get
            {
                if (InternalValue is IList<TItemValue> values)
                {
                    return values;
                }

                if (InternalValue is TItemValue value)
                {
                    return new List<TItemValue>
                    {
                        value
                    };
                }

                return new List<TItemValue>();
            }
        }

        protected virtual IList<TItem> SelectedItems
        {
            get
            {
                return Items.Where(item => InternalValues.Contains(ItemValue(item))).ToList();
            }
        }

        protected virtual bool MenuCanShow => true;

        protected override Dictionary<string, object> InputSlotAttrs => (Menu as MMenu)?.ActivatorAttributes;

        protected virtual BMenuProps GetDefaultMenuProps() => new()
        {
            CloseOnClick = true,
            CloseOnContentClick = false,
            DisableKeys = true,
            OpenOnClick = true,
            MaxHeight = 304,
        };

        protected virtual string GetText(TItem item)
        {
            return item == null ? null : ItemText(item);
        }

        protected TItemValue GetValue(TItem item)
        {
            return ItemValue(item);
        }

        protected bool GetDisabled(TItem item)
        {
            return ItemDisabled(item);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ComputedMenuProps = GetDefaultMenuProps();
            MenuProps?.Invoke(ComputedMenuProps);
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
                        .Add("m-select__selections");
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
                .Apply<BMenu, MMenu>(attrs =>
                {
                    attrs[nameof(MMenu.ExternalActivator)] = true;
                    attrs[nameof(MMenu.Value)] = MenuCanShow && IsMenuActive;
                    attrs[nameof(MMenu.ValueChanged)] = EventCallback.Factory.Create<bool>(this, async val =>
                    {
                        IsMenuActive = val;
                        if (val && !IsFocused && !IsDisabled)
                        {
                            await InputElement.FocusAsync();
                        }
                    });
                    attrs[nameof(MMenu.Disabled)] = Disabled || Readonly;
                    attrs[nameof(MMenu.Bottom)] = ComputedMenuProps.Bottom;
                    attrs[nameof(MMenu.CloseOnClick)] = ComputedMenuProps.CloseOnClick;
                    attrs[nameof(MMenu.CloseOnContentClick)] = ComputedMenuProps.CloseOnContentClick;
                    attrs[nameof(MMenu.DisableKeys)] = ComputedMenuProps.DisableKeys;
                    attrs[nameof(MMenu.Left)] = ComputedMenuProps.Left;
                    attrs[nameof(MMenu.MaxHeight)] = ComputedMenuProps.MaxHeight;
                    attrs[nameof(MMenu.MinWidth)] = ComputedMenuProps.MinWidth;
                    attrs[nameof(MMenu.NudgeTop)] = ComputedMenuProps.NudgeTop;
                    attrs[nameof(MMenu.NudgeRight)] = ComputedMenuProps.NudgeRight;
                    attrs[nameof(MMenu.NudgeBottom)] = ComputedMenuProps.NudgeBottom;
                    attrs[nameof(MMenu.NudgeLeft)] = ComputedMenuProps.NudgeLeft;
                    attrs[nameof(MMenu.NudgeWidth)] = ComputedMenuProps.NudgeWidth;
                    attrs[nameof(MMenu.OffsetX)] = ComputedMenuProps.OffsetX;
                    attrs[nameof(MMenu.OffsetY)] = ComputedMenuProps.OffsetY;
                    attrs[nameof(MMenu.OpenOnClick)] = ComputedMenuProps.OpenOnClick;
                    attrs[nameof(MMenu.Right)] = ComputedMenuProps.Right;
                    attrs[nameof(MMenu.Top)] = ComputedMenuProps.Top;
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
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.OnSelect)] = CreateEventCallback<TItem>(SelectItemsAsync);
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.ItemContent)] = ItemContent;
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.PrependItemContent)] = PrependItemContent;
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.AppendItemContent)] = AppendItemContent;
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.SelectedIndex)] = SelectedIndex;
                    attrs[nameof(MSelectList<TItem, TItemValue, TValue>.NoDataContent)] = NoDataContent;
                })
                .Apply<BChip, MChip>(attrs =>
                {
                    attrs[nameof(MChip.Close)] = DeletableChips && (!IsDisabled && !IsReadonly);
                    attrs[nameof(MChip.Disabled)] = IsDisabled;
                    attrs[nameof(MChip.Class)] = "m-chip--select";
                    attrs[nameof(MChip.Small)] = SmallChips;
                });
        }

        protected virtual async Task SelectItemsAsync(TItem item)
        {
            var value = ItemValue(item);
            if (!Multiple)
            {
                if (value is TValue val)
                {
                    await SetInternalValueAsync(val);
                }
                IsMenuActive = false;
            }
            else
            {
                var internalValues = InternalValues.ToList();
                if (internalValues.Contains(value))
                {
                    internalValues.Remove(value);
                }
                else
                {
                    internalValues.Add(value);
                }

                if (internalValues is TValue val)
                {
                    await SetInternalValueAsync(val);
                }
            }

            if (OnSelectedItemUpdate.HasDelegate)
            {
                await OnSelectedItemUpdate.InvokeAsync(item);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.PreventDefaultOnArrowUpDown, InputElement);
                //await (Menu as MMenu)?.UpdateActivatorAsync(InputSlotElement);
            }
        }

        public override Task HandleOnAppendClickAsync(MouseEventArgs args)
        {
            IsMenuActive = true;
            return base.HandleOnAppendClickAsync(args);
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            switch (args.Code)
            {
                case "ArrowUp":
                    ChangeSelectedIndex(-1);
                    break;
                case "ArrowDown":
                    ChangeSelectedIndex(+1);
                    break;
                case "Enter":
                    if (IsMenuActive)
                    {
                        if (SelectedIndex > -1 && SelectedIndex < ComputedItems.Count)
                        {
                            var item = ComputedItems[SelectedIndex];
                            await SelectItemsAsync(item);
                        }
                    }
                    else
                    {
                        IsMenuActive = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeSelectedIndex(int change)
        {
            var index = SelectedIndex + change;
            if (index > ComputedItems.Count - 1)
            {
                //Back to first
                index = 0;
            }
            else if (index < 0)
            {
                //Go to last
                index = ComputedItems.Count - 1;
            }

            SelectedIndex = index;
        }

        public override async Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            if (Multiple)
            {
                IList<TItemValue> values = new List<TItemValue>();
                await SetInternalValueAsync((TValue)values);
            }
            else
            {
                await base.HandleOnClearClickAsync(args);
            }
        }
    }
}