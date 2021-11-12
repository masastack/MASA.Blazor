using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MBadge : BBadge, IBadge
    {
        [Parameter]
        public bool Avatar { get; set; }

        [Parameter]
        public bool Bordered { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        [Parameter]
        public StringNumber Content { get; set; }

        [Parameter]
        public bool Dot { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public StringNumber OffsetX { get; set; }

        [Parameter]
        public StringNumber OffsetY { get; set; }

        [Parameter]
        public bool OverLap { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Transition { get; set; } = "scale-rotate-transition";

        [Parameter]
        public bool Value { get; set; } = true;

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public RenderFragment BadgeContent { get; set; }

        public bool IsDark => Dark ? 
            true : 
            (Light ? false : Themeable != null && Themeable.IsDark);

        private int Offset => OverLap ?
            (Dot ? 8 : 12) :
            (Dot ? 2 : 4);

        private const string Auto = "auto";

        private string CalcPosition(StringNumber offset)
        {
            var obj = offset != null ? offset.ToUnit() : $"{Offset}px";

            return $"calc(100% - {obj})";
        }

        protected string ComputedXOffset => CalcPosition(OffsetX);

        protected string ComputedYOffset => CalcPosition(OffsetY);

        protected string ComputedBottom => Bottom ? Auto : ComputedYOffset;

        protected string ComputedTop => Bottom ? ComputedYOffset : Auto;

        protected string ComputedLeft => Right ? 
            (Left ? ComputedXOffset : Auto) :
            (Left ? Auto : ComputedXOffset);

        protected string ComputedRight => Right ? 
            (Left ? Auto : ComputedXOffset) : 
            (!Left ? Auto : ComputedXOffset);

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuiler =>
                {
                    cssBuiler
                        .Add("m-badge")
                        .AddIf("m-badge--avatar", () => Avatar)
                        .AddIf("m-badge--bordered", () => Bordered)
                        .AddIf("m-badge--bottom", () => Bottom)
                        .AddIf("m-badge--dot", () => Dot)
                        .AddIf("m-badge--icon", () => !string.IsNullOrWhiteSpace(Icon))
                        .AddIf("m-badge--inline", () => InLine)
                        .AddIf("m-badge--left", () => Left)
                        .AddIf("m-badge--overlap", () => OverLap)
                        .AddIf("m-badge--tile", () => Tile)
                        .AddTheme(IsDark);
                })
                .Apply("wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m--badge__wrapper");
                })
                .Apply("badge", cssBuilder => 
                {
                    cssBuilder
                        .Add("m-badge__badge")
                        .AddBackgroundColor(Color);
                }, styleBuilder => 
                {
                    styleBuilder
                        .AddIf(() => $"bottom:{ComputedBottom}", () => !InLine)
                        .AddIf(() => $"left:{ComputedLeft}", () => !InLine)
                        .AddIf(() => $"right:{ComputedRight}", () => !InLine)
                        .AddIf(() => $"top:{ComputedTop}", () => !InLine)
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .ApplyBadgeDefault()
                .Apply<BIcon, MIcon>();
        }
    } 
}
