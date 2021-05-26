using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSelect<TItem, TValue> : BSelect<TItem, TValue>
    {
        private HtmlElement _activatorRect = new HtmlElement();

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        private int Width => _visible || _text.Any() ? ComputeLabelLength() * 6 : 0;

        protected override string LegendStyle => $"width: {Width}px";

        protected override Task OnInitializedAsync()
        {
            _icon = "mdi-menu-down";

            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JsInvokeAsync(JsInteropConstants.AddElementTo, PopoverRef, ".m-application");
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
                        .AddIf("m-text-field--enclosed m-text-field--filled", () => Filled)
                        .AddIf("m-text-field--enclosed m-text-field--outlined", () => Outlined)
                        .AddIf("m-text-field--enclosed m-text-field--single-line m-text-field--solo", () => Solo)
                        .AddIf("m-input--is-focused primary--text", () => _focused)
                        .AddIf("m-select--is-menu-active", () => _visible)
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
                        .AddIf("m-label--active", () => { return Solo ? false : _visible || _text.Any(); })
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
                });

            AbstractProvider
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Class)] = _visible ? "primary--text" : "";
                })
                .Apply<BPopover, MPopover>(props =>
                {
                    var css = "m-menu__content menuable__content__active";
                    var clientX = _activatorRect.AbsoluteLeft;
                    var clientY = _activatorRect.AbsoluteTop;
                    if (Fixed)
                    {
                        css += " m-menu__content--fixed";
                        clientX = _activatorRect.RelativeLeft;
                        clientY = _activatorRect.RelativeTop;
                    };

                    props[nameof(MPopover.Class)] = css;
                    props[nameof(MPopover.Visible)] = (_visible && Items != null);
                    props[nameof(MPopover.ClientX)] = (StringNumber)clientX;
                    props[nameof(MPopover.ClientY)] = (StringNumber)clientY;
                    props[nameof(MPopover.MinWidth)] = (StringNumber)_activatorRect.ClientWidth;
                    props[nameof(MPopover.MaxHeight)] = (StringNumber)400;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = _visible;
                    props[nameof(MOverlay.Click)] =
                        EventCallback.Factory.Create<MouseEventArgs>(this, () => { _visible = false; });
                    props[nameof(MOverlay.Opacity)] = (StringNumber)0;
                })
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Dense)] = Dense;
                })
                .Apply<BListItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";

                    if (Multiple)
                    {
                        props[nameof(MListItemGroup.Multiple)] = Multiple;
                        // TODO: change to TValue
                        props[nameof(MListItemGroup.Values)] = Values.Select(u => u.ToString()).ToList();
                    }
                    else
                    {
                        // TODO: change to TValue
                        props[nameof(MListItemGroup.Value)] = Value?.ToString();
                    }
                })
                .Apply<BSelectOption<TItem, TValue>, MSelectOption<TItem, TValue>>()
                .Apply<BChip, MChip>()
                .Apply<BHitMessage, MHitMessage>();
        }

        protected override async Task Click(MouseEventArgs args)
        {
            _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);

            await base.Click(args);
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