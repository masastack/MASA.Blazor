using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MSelect<TItem> : BSelect<TItem>
    {
        private BoundingClientRect _rect;

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public int MinWidth { get; set; }

        protected override Task OnInitializedAsync()
        {
            _icon = "mdi-menu-down";

            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _rect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
            }
        }

        protected override void SetComponentClass()
        {
            CssBuilder
                .Add("m-input m-text-field m-text-field--is-booted m-select")
                .AddIf("m-input--is-disabled", () => Disabled)
                .AddIf("m-input--dense", () => Dense)
                .AddIf("m-text-field--enclosed m-text-field--filled", () => Filled)
                .AddIf("m-text-field--enclosed m-text-field--outlined", () => Outlined)
                .AddIf("m-text-field--enclosed m-text-field--single-line m-text-field--solo", () => Solo)
                .AddIf("m-input--is-focused primary--text", () => _focused)
                .AddIf("m-select--is-menu-active", () => _visible)
                .AddTheme(Dark);

            StyleBuilder
                .AddIf(() => $"min-width:{MinWidth}px", () => MinWidth != 0);

            ControlCssBuilder
                .Add("m-input__control");

            SlotCssBuilder
                .Add("m-input__slot");

            SelectSlotCssBuilder
                .Add("m-select__slot");

            LabelCssBuilder
                .Add("m-label")
                .AddIf("m-label--active", () =>
                {
                    return Solo ? false : _visible || Value != null;
                })
                .AddIf("primary--text", () =>
                {
                    return Solo ? false : _focused;
                })
                .AddTheme(Dark);

            LabelStyleCssBuilder
                .Add("left: 0px; right: auto; position: absolute");

            SelectorCssBuilder
                .Add("m-select__selections");

            SelectedCssBuilder
                .Add("m-select__selection--comma");

            SelectArrowCssBuilder
                .Add("m-input__append-inner");

            SelectArrowIconCssBuilder
                .Add("m-input__icon m-input__icon--append");

            HitCssBuilder
                .Add("m-text--field__details");

            var value = true;
            SlotProvider
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.OriginalClass)] = _visible ? "primary--text" : "";
                })
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.OriginalClass)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = value;
                    props[nameof(MPopover.MinWidth)] = (StringOrNumber)_rect.Width;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = value;
                    props[nameof(MOverlay.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, () => { _visible = false; });
                    props[nameof(MOverlay.Opacity)] = (StringOrNumber)0;
                })
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Dense)] = Dense;
                })
                .Apply<BListItemGroup, MListItemGroup>()
                .Apply<BSelectOption<TItem>, MSelectOption<TItem>>()
                .Apply<BHitMessage, MHitMessage>();
        }
    }
}
