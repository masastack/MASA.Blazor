using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;

namespace Masa.Blazor
{
    public class MButton : BButton, IButton, IThemeable
    {
        public bool Default { get; set; } = true;

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Depressed { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Icon { get; set; }

        [Parameter]
        public bool Fab { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Plain { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Type
        {
            get => TypeAttribute;
            set => TypeAttribute = value;
        }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        protected virtual bool HasBackground => !(Icon || Plain || Outlined || Text);

        protected bool IsRound => Icon || Fab;

        protected bool IsElevated => !(Icon || Text || Outlined || Depressed || Disabled || Plain) && (Elevation == null || Elevation.TryGetNumber().number > 0);

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn")
                        .AddIf("m-btn--absolute", () => Absolute)
                        .AddIf("m-btn--block", () => Block)
                        .AddIf("m-btn--bottom", () => Bottom)
                        .AddIf("m-btn--disabled", () => Disabled)
                        .AddIf("m-btn--is-elevated", () => IsElevated)
                        .AddIf("m-btn--fab", () => Fab)
                        .AddIf("m-btn--fixed", () => Fixed)
                        .AddIf("m-btn--has-bg", () => HasBackground)
                        .AddIf("m-btn--icon", () => Icon)
                        .AddIf("m-btn--left", () => Left)
                        .AddIf("m-btn--loading", () => Loading)
                        .AddIf("m-btn--outlined", () => Outlined)
                        .AddIf("m-btn--plain", () => Plain)
                        .AddIf("m-btn--right", () => Right)
                        .AddIf("m-btn--round", () => IsRound)
                        .AddIf("m-btn--rounded", () => Rounded)
                        .AddIf("m-btn--text", () => Text)
                        .AddIf("m-btn--tile", () => Tile)
                        .AddIf("m-btn--top", () => Top)
                        .AddIf($"m-btn--active {ComputedActiveClass}", () => InternalIsActive)
                        .AddFirstIf(
                            ("m-size--x-large", () => XLarge),
                            ("m-size--large", () => Large),
                            ("m-size--small", () => Small),
                            ("m-size--x-small", () => XSmall),
                            ("m-size--default", () => Default)
                        )
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color, () => HasBackground)
                        .AddTextColor(Color, () => !HasBackground);
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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            HasLoader = true;

            AbstractProvider
                .Apply(typeof(BButtonLoader<>), typeof(BButtonLoader<MButton>))
                .Apply<BProgressCircular, MProgressCircular>(prop =>
                {
                    prop[nameof(MProgressCircular.Size)] = (StringNumber)23;
                    prop[nameof(MProgressCircular.Width)] = (StringNumber)2;
                    prop[nameof(MProgressCircular.Indeterminate)] = true;
                })
                .Apply(typeof(BButtonContent<>), typeof(BButtonContent<MButton>));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Attributes["ripple"] = Ripple;
        }

        protected override async Task HandleOnClick(MouseEventArgs args)
        {
            if (!Fab && args.Detail > 0)
            {
                await JsInvokeAsync(JsInteropConstants.Blur, Ref);
            }

            await base.HandleOnClick(args);
        }
    }
}