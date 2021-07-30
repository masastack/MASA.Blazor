using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSelect<TItem, TValue> : BSelect<TItem, TValue>
    {
        protected HtmlElement _activatorRect = new();

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        private int Width => Visible || _text.Any() ? ComputeLabelLength() * 6 : 0;

        protected override string LegendStyle => $"width: {Width}px";

        protected override Task OnInitializedAsync()
        {
            _icon = "mdi-menu-down";

            return base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BSelect<TItem, TValue>>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input m-text-field m-text-field--is-booted m-select")
                        .AddIf("m-input--is-disabled", () => Disabled)
                        .AddIf("m-input--dense", () => Dense)
                        .AddIf("m-input--hide-details", () => string.IsNullOrEmpty(Hint))
                        .AddIf("m-text-field--enclosed m-text-field--filled", () => Filled)
                        .AddIf("m-text-field--enclosed m-text-field--outlined", () => Outlined)
                        .AddIf("m-text-field--enclosed m-text-field--single-line m-text-field--solo", () => Solo)
                        .AddIf("m-input--is-focused primary--text", () => _focused && !Loading)
                        .AddIf("m-select--is-menu-active", () => Visible)
                        .AddIf("m-select--is-multi", () => Multiple)
                        .AddIf("m-select--chips", () => Chips)
                        .AddTheme(Dark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddMinWidth(MinWidth);
                })
                .Apply("control", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__control");
                })
                .Apply("slot", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__slot");
                })
                .Apply("select-slot", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-select__slot");
                })
                .Apply("label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-label")
                        .AddIf("m-label--active", () => { return Solo ? false : Visible || _text.Any(); })
                        .AddIf("primary--text", () => { return Solo ? false : _focused; })
                        .AddTheme(Dark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("left: 0px; right: auto; position: absolute");
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
                .Apply("hint", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text--field__details");
                })
                .Apply("selected", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-select__selection")
                        .Add("m-select__selection--comma");
                });

            AbstractProvider
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
                    props[nameof(MMenu.CloseOnContentClick)] = !HasBody && !Multiple;
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
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Class)] = _focused ? "primary--text" : "";
                })
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Dense)] = Dense;
                })
                .Apply<BSelectOption<TItem, TValue>, MSelectOption<TItem, TValue>>()
                .Apply<BChip, MChip>()
                .Apply<BHintMessage, MHintMessage>();
        }

        private int ComputeLabelLength()
        {
            if (string.IsNullOrEmpty(Label))
            {
                return 0;
            }

            var length = 0;
            for (int i = 0; i < Label.Length; i++)
            {
                if (Label[i] > 127)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }

            return length + 1;
        }
    }
}