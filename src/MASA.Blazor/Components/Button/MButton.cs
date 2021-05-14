using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MButton : BButton
    {
        [Parameter]
        public bool Depressed { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BButton>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn")
                        .AddIf("m-btn--disabled", () => Disabled)
                        .AddIf("m-btn--has-bg", () => !(Icon || Plain || Outlined || Text))
                        .AddIf("m-btn--is-elevated", () => !(Depressed || Icon || Plain || Outlined || Text))
                        .AddIf("m-btn--round", () => Fab || Icon)
                        .AddIf("m-btn--rounded", () => Rounded)
                        .AddIf("m-btn--block", () => Block)
                        .AddIf("m-btn--loading", () => Loading)
                        .AddIf("m-btn--fab", () => Fab)
                        .AddIf("m-btn--icon", () => Icon)
                        .AddIf("m-btn--outlined", () => Outlined)
                        .AddIf("m-btn--plain", () => Plain)
                        .AddIf("m-btn--text", () => Text)
                        .AddIf("m-btn--tile", () => Tile)
                        .AddFirstIf(
                            ("m-size--x-large", () => XLarge),
                            ("m-size--large", () => Large),
                            ("m-size--small", () => Small),
                            ("m-size--x-small", () => XSmall),
                            ("m-size--default", () => true)
                            )
                        .AddTheme(Dark)
                        .AddColor(Color, Icon || Outlined || Plain || Text);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddColor(Color, Icon || Outlined || Plain || Text);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn__content");
                })
                .Apply("loader", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn__loader");
                });
        }
    }
}
