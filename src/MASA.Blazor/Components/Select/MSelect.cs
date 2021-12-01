using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MSelect<TItem, TItemValue, TValue> : MTextField<TValue>, ISelect<TItem, TItemValue, TValue>
    {
        private bool _visible;
        protected bool Visible
        {
            get => MenuProps == null ? _visible : MenuProps.Visible;
            set
            {
                if (MenuProps == null)
                    _visible = value;
                else
                    MenuProps.Visible = value;
            }
        }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public override string AppendIcon { get; set; } = "mdi-menu-down";

        [Parameter]
        public bool Chips { get; set; }

        [Parameter]
        public bool SmallChips { get; set; }

        [Parameter]
        public bool DeletableChips { get; set; }

        [Parameter]
        public BMenuProps MenuProps { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, TItemValue> ItemValue { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemDisabled { get; set; } = item => false;

        [EditorRequired]
        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public RenderFragment PrependItemContent { get; set; }

        [Parameter]
        public RenderFragment AppendItemContent { get; set; }

        [Parameter]
        public RenderFragment<SelectSelectionProps<TItem>> SelectionContent { get; set; }

        [Parameter]
        public RenderFragment<SelectListItemProps<TItem>> ItemContent { get; set; }

        [Parameter]
        public EventCallback<TItem> OnSelectedItemUpdate { get; set; }

        [Parameter]
        public bool HideSelected { get; set; }

        [Parameter]
        public bool HideNoData { get; set; }

        [Parameter]
        public RenderFragment NoDataContent { get; set; }

        public virtual List<string> Text
        {
            get
            {
                if (Multiple)
                {
                    return FormatText(Values);
                }

                if (InternalValue is TValue value)
                {
                    return FormatText(value);
                }

                return new List<string>();
            }
        }

        public override bool IsDirty => SelectedItems.Count > 0;

        public int HighlightIndex { get; set; } = -1;

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            { "value", null },
            { "readonly", true }
        };

        public virtual IReadOnlyList<TItem> ComputedItems => Items;

        public string QueryText { get; set; }

        public IList<TItemValue> Values
        {
            get
            {
                if (Value is IList<TItemValue> values)
                {
                    return values;
                }

                if (Value is TItemValue value)
                {
                    return new List<TItemValue>
                    {
                        value
                    };
                }

                return new List<TItemValue>();
            }
        }

        public IList<TItem> SelectedItems => Items
                .Where(u => Values.Contains(ItemValue(u))).ToList();

        public object Menu { get; set; }

        protected virtual List<string> FormatText(TValue value)
        {
            //TODO:set default expression
            if (ItemValue == null || ItemText == null)
            {
                return Text;
            }

            //why list?
            return Items
                .Where(u => ItemValue(u).Equals(value))
                .Select(ItemText).ToList();
        }

        protected virtual List<string> FormatText(IEnumerable<TItemValue> values)
        {
            return Items
                .Where(u => values.Contains(ItemValue(u)))
                .Select(ItemText).ToList();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.PreventDefaultOnArrowUpDown, InputElement);

                await (Menu as MMenu)?.UpdateActivator(InputSlotElement);
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-select")
                        .AddIf("m-select--is-menu-active", () => Visible)
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
                .Apply("selector", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-select__selections");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("m-select__selection--comma");
                })
                .Apply("select-arrow", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__append-inner");
                })
                .Apply("select-arrow-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon m-input__icon--append");
                })
                .Apply("selected", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-select__selection")
                        .Add("m-select__selection--comma");
                });

            AbstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BSelectDefaultSlot<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectHiddenInput<,,,>), typeof(BSelectHiddenInput<TItem, TItemValue, TValue, MSelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectMenu<,,,>), typeof(BSelectMenu<TItem, TItemValue, TValue, MSelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectSelections<,,,>), typeof(BSelectSelections<TItem, TItemValue, TValue, MSelect<TItem, TItemValue, TValue>>))
                .Apply<BMenu, MMenu>(props =>
                {
                    props[nameof(MMenu.Value)] = Visible;
                    props[nameof(MMenu.ValueChanged)] = EventCallback.Factory.Create<bool>(this, async (v) =>
                     {
                         Visible = v;

                         if (v)
                         {
                             await InputElement.FocusAsync();
                         }
                     });
                    props[nameof(MMenu.Disabled)] = Disabled || Readonly;
                    props[nameof(MMenu.OffsetY)] = MenuProps?.OffsetY;
                    props[nameof(MMenu.OffsetX)] = MenuProps?.OffsetX;
                    props[nameof(MMenu.CloseOnContentClick)] = false;
                    props[nameof(MMenu.Top)] = MenuProps?.Top;
                    props[nameof(MMenu.Right)] = MenuProps?.Right;
                    props[nameof(MMenu.Bottom)] = MenuProps?.Bottom;
                    props[nameof(MMenu.Left)] = MenuProps?.Left;
                    props[nameof(MMenu.NudgeTop)] = MenuProps?.NudgeTop;
                    props[nameof(MMenu.NudgeRight)] = MenuProps?.NudgeRight;
                    props[nameof(MMenu.NudgeBottom)] = MenuProps?.NudgeBottom;
                    props[nameof(MMenu.NudgeLeft)] = MenuProps?.NudgeLeft;
                    props[nameof(MMenu.NudgeWidth)] = MenuProps?.NudgeWidth;
                    props[nameof(MMenu.MaxHeight)] = MenuProps?.MaxHeight ?? 400;
                    props[nameof(MMenu.MinWidth)] = MenuProps?.MinWidth;
                })
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Dense)] = Dense;
                })
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Dense)] = Dense;
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply(typeof(BSelectList<,,>), typeof(MSelectList<TItem, TItemValue, TValue>), props =>
                {
                    props[nameof(MSelectList<TItem, TItemValue, TValue>.ItemContent)] = ItemContent;
                })
                .Apply<BChip, MChip>(props =>
                {
                    props[nameof(MChip.Close)] = DeletableChips && (!IsDisabled && !IsReadonly);
                    props[nameof(MChip.Disabled)] = IsDisabled;
                    props[nameof(MChip.Class)] = "m-chip--select";
                    props[nameof(MChip.Small)] = SmallChips;
                });
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            switch (args.Code)
            {
                case "ArrowUp":
                    if (HighlightIndex > 0)
                    {
                        HighlightIndex--;
                    }
                    else
                    {
                        HighlightIndex = ComputedItems.Count - 1;
                    }
                    break;
                case "ArrowDown":
                    if (HighlightIndex < ComputedItems.Count - 1)
                    {
                        HighlightIndex++;
                    }
                    else
                    {
                        HighlightIndex = 0;
                    }
                    break;
                case "Enter":
                    if (HighlightIndex > -1 && HighlightIndex < ComputedItems.Count)
                    {
                        var item = ComputedItems[HighlightIndex];

                        var label = ItemText(item);
                        var val = ItemValue(item);

                        if (!Multiple)
                        {
                            Visible = false;
                            await SetSelectedAsync(label, val);
                        }
                        else
                        {
                            if (Values.Contains(val))
                            {
                                await RemoveSelectedAsync(label, val);
                            }
                            else
                            {
                                await SetSelectedAsync(label, val);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            //TODO:try focus
            await InputElement.FocusAsync();
            await base.HandleOnClickAsync(args);
        }

        public void SetVisible(bool visible)
        {
            Visible = visible;
            InvokeStateHasChanged();
        }

        public async Task SetSelectedAsync(string text, TItemValue value)
        {
            if (Multiple)
            {
                IList<TItemValue> values = Values;
                if (!values.Contains(value))
                {
                    values.Add(value);
                    InternalValue = (TValue)values;
                }
            }
            else
            {
                if (value is TValue val)
                {
                    InternalValue = val;
                }
            }

            if (OnSelectedItemUpdate.HasDelegate)
            {
                var selectedItem = Items.FirstOrDefault(item => EqualityComparer<TItemValue>.Default.Equals(ItemValue(item), value));
                await OnSelectedItemUpdate.InvokeAsync(selectedItem);
            }

            //TODO: Refactor MSelectList
            StateHasChanged();
        }

        public Task RemoveSelectedAsync(string text, TItemValue value)
        {
            var values = Values;
            values.Remove(value);

            Text.Remove(text);
            InternalValue = (TValue)values;

            //TODO: Refactor MSelectList
            StateHasChanged();

            return Task.CompletedTask;
        }

        public override async Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            if (Multiple)
            {
                await InputElement.FocusAsync();

                IList<TItemValue> values = new List<TItemValue>();
                InternalValue = (TValue)values;
            }
            else
            {
                await base.HandleOnClearClickAsync(args);
            }
        }
    }
}
