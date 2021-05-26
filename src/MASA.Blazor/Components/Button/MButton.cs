using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

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

        [Parameter]
        public bool Default { get; set; } = true;

        [Parameter]
        public bool? IsActive { get; set; }

        [Parameter]
        public bool CurrentDate { get; set; }

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
                        .AddIf("m-btn--text", () => Text && IsActive == null)
                        .AddIf("m-btn--tile", () => Tile)
                        .AddIf("m-btn--active", () => IsActive ?? false)
                        .AddFirstIf(
                            ("m-size--x-large", () => XLarge),
                            ("m-size--large", () => Large),
                            ("m-size--small", () => Small),
                            ("m-size--x-small", () => XSmall),
                            ("m-size--default", () => Default)
                            )
                        .AddTheme(Dark)
                        .AddColor(Color, (IsActive == null) && (Icon || Outlined || Plain || Text), () => IsActive == null || (IsActive ?? false))
                        .AddTextColor(Color, () => IsActive != null && IsActive == false && CurrentDate);
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

        protected override Task OnInitializedAsync()
        {
            AbstractProvider
                .Apply<BProcessCircular, MProcessCircular>(prop =>
                {
                    prop[nameof(MProcessCircular.Size)] = (StringNumber)23;
                    prop[nameof(MProcessCircular.Indeterminate)] = true;
                });

            return base.OnInitializedAsync();
        }
    }
}
