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
    public class MSelect<TItem, TValue> : MTextField<TValue>, ISelect<TItem, TValue>
    {
        private bool _shouldReformatText = true;

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
        public BMenuProps MenuProps { get; set; }

        [Parameter]
        public override TValue Value
        {
            get => base.Value;
            set
            {
                base.Value = value;

                if (_shouldReformatText && ItemValue != null)
                    Text = FormatText(value);

                _shouldReformatText = true;
            }
        }

        private List<TValue> _values = new();

        [Parameter]
        public List<TValue> Values
        {
            get => _values;
            set
            {
                _values = value ?? new List<TValue>();

                if (_shouldReformatText && ItemValue != null)
                    Text = FormatText(_values);

                _shouldReformatText = true;
                NotifyValuesChanged();
            }
        }

        [Parameter]
        public EventCallback<List<TValue>> ValuesChanged { get; set; }

        [Parameter]
        public Expression<Func<List<TValue>>> ValuesExpression { get; set; }

        [Parameter]
        public Func<TItem, string> ItemText { get; set; } = null!;

        [Parameter]
        public Func<TItem, TValue> ItemValue { get; set; } = null!;

        [Parameter]
        public Func<TItem, bool> ItemDisabled { get; set; } = (item) => false;

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        public List<string> Text { get; set; } = new();

        //TODO:menu will change
        public Func<MouseEventArgs, Task> OnExtraClick { get; set; }

        public override bool IsDirty => Text.Count > 0;

        public FieldIdentifier ValuesFieldIdentifier { get; set; }

        public int HighlightIndex { get; set; } = -1;

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type },
            //TODO:this may change
            { "value", Chips ? "" : string.Join(',', FormatText(Value)) },
            { "readonly", true }
        };

        public virtual IReadOnlyList<TItem> ComputedItems => Items;

        public string QueryText { get; set; }

        protected virtual List<string> FormatText(TValue value)
        {
            //TODO:set default expression
            if (ItemValue == null || ItemText == null || EqualityComparer<TValue>.Default.Equals(Value, default))
            {
                return new List<string>();
            }

            //why list?
            return Items
                .Where(u => ItemValue(u).Equals(value))
                .Select(ItemText).ToList();
        }

        protected virtual List<string> FormatText(IEnumerable<TValue> values)
        {
            return Items
                .Where(u => values.Contains(ItemValue(u)))
                .Select(ItemText).ToList();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.PreventDefaultOnArrowUpDown, InputRef);
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
                .Merge(typeof(BInputDefaultSlot<>), typeof(BSelectDefaultSlot<TItem, TValue>))
                .Apply(typeof(BSelectHiddenInput<,,>), typeof(BSelectHiddenInput<TItem, TValue, MSelect<TItem, TValue>>))
                .Apply(typeof(BSelectMenu<,,>), typeof(BSelectMenu<TItem, TValue, MSelect<TItem, TValue>>))
                .Apply(typeof(BSelectSelections<,,>), typeof(BSelectSelections<TItem, TValue, MSelect<TItem, TValue>>))
                .Apply<BMenu, MMenu>(props =>
                {
                    props[nameof(MMenu.Visible)] = Visible;
                    props[nameof(MMenu.VisibleChanged)] = EventCallback.Factory.Create<bool>(this, (v) =>
                    {
                        Visible = v;
                    });
                    props[nameof(MMenu.Disabled)] = Disabled;
                    props[nameof(MMenu.OffsetY)] = MenuProps?.OffsetY;
                    props[nameof(MMenu.OffsetX)] = MenuProps?.OffsetX;
                    props[nameof(MMenu.Block)] = MenuProps?.Block ?? true;
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
                    props[nameof(MMenu.Input)] = true;
                    props[nameof(MMenu.ActivatorRef)] = InputSlotRef;
                })
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Dense)] = Dense;
                })
                .Apply<BListItem, MListItem>()
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BSelectOption<TItem, TValue>, MSelectOption<TItem, TValue>>()
                .Apply<BChip, MChip>();
        }

        public void SetOnExtraClick(Func<MouseEventArgs, Task> onExtraClick)
        {
            OnExtraClick = onExtraClick;
        }

        protected void NotifyValuesChanged()
        {
            if (EditContext != null && ValuesExpression != null)
            {
                EditContext.NotifyFieldChanged(ValuesFieldIdentifier);
            }
        }

        protected override void OnParametersSet()
        {
            if (EditContextChanged)
            {
                if (ValuesExpression != null)
                {
                    ValuesFieldIdentifier = FieldIdentifier.Create(ValuesExpression);
                }
            }

            base.OnParametersSet();
        }

        public override Task HandleOnBlur(FocusEventArgs args)
        {
            SetVisible(false);
            return base.HandleOnBlur(args);
        }

        public override async Task HandleOnKeyDown(KeyboardEventArgs args)
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
                            if (_values.Contains(val))
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

        public override async Task HandleOnClick(MouseEventArgs args)
        {
            //TODO:menu will change
            if (OnExtraClick != null && !Readonly)
            {
                await OnExtraClick(args);
            }

            //TODO:try focus
            await InputRef.FocusAsync();
            await base.HandleOnClick(args);
        }

        public void SetVisible(bool visible)
        {
            Visible = visible;
            InvokeStateHasChanged();
        }

        public async Task SetSelectedAsync(string text, TValue value)
        {
            _shouldReformatText = false;

            if (Multiple)
            {
                if (!_values.Contains(value))
                {
                    _values.Add(value);
                    Text.Add(text);
                }
            }
            else
            {
                Value = value;
                Text.Clear();
                Text.Add(text);
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
        }

        public async Task RemoveSelectedAsync(string text, TValue value)
        {
            _shouldReformatText = false;

            _values.Remove(value);
            Text.Remove(text);

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
        }
    }
}
