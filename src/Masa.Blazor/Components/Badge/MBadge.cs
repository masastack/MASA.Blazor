﻿namespace Masa.Blazor
{
    public partial class MBadge : BBadge, IBadge
    {
        [Inject]
        private MasaBlazor MasaBlazor { get; set; } = null!;
        
        [Parameter]
        public bool Avatar { get; set; }

        [Parameter]
        public bool Bordered { get; set; }

        [Parameter]
        [MasaApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public StringNumber? Content { get; set; }

        [Parameter]
        public bool Dot { get; set; }

        [Parameter]
        public string? Icon { get; set; }

        [Parameter]
        public StringNumber? OffsetX { get; set; }

        [Parameter]
        public StringNumber? OffsetY { get; set; }

        [Parameter]
        public bool OverLap { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        [MasaApiParameter("scale-rotate-transition")]
        public string Transition { get; set; } = "scale-rotate-transition";

        [Parameter]
        public bool Value { get; set; } = true;

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public RenderFragment? BadgeContent { get; set; }

        private int Offset => OverLap ?
            (Dot ? 8 : 12) :
            (Dot ? 2 : 4);

        private const string AUTO = "auto";

        private string CalcPosition(StringNumber? offset)
        {
            var obj = offset != null ? offset.ToUnit() : $"{Offset}px";

            return $"calc(100% - {obj})";
        }

        protected string ComputedXOffset => CalcPosition(OffsetX);

        protected string ComputedYOffset => CalcPosition(OffsetY);

        protected string ComputedBottom => Bottom ? AUTO : ComputedYOffset;

        protected string ComputedTop => Bottom ? ComputedYOffset : AUTO;

        protected string ComputedLeft => IsRtl ?
            (Left ? ComputedXOffset : AUTO) :
            (Left ? AUTO : ComputedXOffset);

        protected string ComputedRight => IsRtl ?
            (Left ? AUTO : ComputedXOffset) :
            (!Left ? AUTO : ComputedXOffset);

        protected bool IsRtl => MasaBlazor.RTL;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-badge")
                        .AddIf("m-badge--avatar", () => Avatar)
                        .AddIf("m-badge--bordered", () => Bordered)
                        .AddIf("m-badge--bottom", () => Bottom)
                        .AddIf("m-badge--dot", () => Dot)
                        .AddIf("m-badge--icon", () => !string.IsNullOrWhiteSpace(Icon))
                        .AddIf("m-badge--inline", () => Inline)
                        .AddIf("m-badge--left", () => Left)
                        .AddIf("m-badge--overlap", () => OverLap)
                        .AddIf("m-badge--tile", () => Tile)
                        .AddTheme(IsDark, IndependentTheme);
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
                        .AddIf(() => $"bottom:{ComputedBottom}", () => !Inline)
                        .AddIf(() => $"left:{ComputedLeft}", () => !Inline)
                        .AddIf(() => $"right:{ComputedRight}", () => !Inline)
                        .AddIf(() => $"top:{ComputedTop}", () => !Inline)
                        .AddBackgroundColor(Color)
                        .AddIf("display:none", () => Transition == null && !Value);
                });

            AbstractProvider
                .ApplyBadgeDefault()
                .Apply<BIcon, MIcon>();
        }
    }
}
