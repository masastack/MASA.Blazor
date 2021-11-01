using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MSheet : BSheet, IThemeable, BlazorComponent.ISheet
    {
        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public override bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-sheet";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--shaped", () => Shaped)
                        .AddTheme(IsDark)
                        .AddElevation(Elevation)
                        .AddRounded(Rounded, Tile)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .Apply(typeof(BSheetBody<>), typeof(BSheetBody<BlazorComponent.ISheet>));
        }
    }
}