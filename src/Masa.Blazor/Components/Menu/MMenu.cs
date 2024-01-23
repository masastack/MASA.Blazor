﻿using BlazorComponent.Web;

namespace Masa.Blazor
{
    public class MMenu : BMenu
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        protected override string DefaultAttachSelector => Permanent ? ".m-application__permanent" : ".m-application";

        protected override bool IsRtl => MasaBlazor.RTL;

        public override IEnumerable<string> DependentSelectors
            => base.DependentSelectors.Concat(new[] { MSnackbar.ROOT_CSS_SELECTOR, PEnqueuedSnackbars.ROOT_CSS_SELECTOR }).Distinct();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Transition ??= "m-menu-transition";
            Origin ??= "top left";
        }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-menu")
                        .AddIf("m-menu--attached", () => IsAttachSelf);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder.Add("m-menu__content")
                              .AddIf("m-menu__content--auto", () => Auto)
                              .AddIf("m-menu__content--fixed", () => ActivatorFixed)
                              .AddIf("menuable__content__active", () => IsActive)
                              .AddRounded(Tile ? "0" : Rounded)
                              .Add(ContentClass)
                              .AddTheme(IsDark, IndependentTheme);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"max-height:{CalculatedMaxHeight}", () => CalculatedMaxHeight != null)
                        .AddIf($"min-width:{CalculatedMinWidth}", () => CalculatedMinWidth != null)
                        .AddIf($"max-width:{CalculatedMaxWidth}", () => CalculatedMaxWidth != null)
                        .AddIf($"top:{CalculatedTop!.ToUnit()}", () => CalculatedTop != null)
                        .AddIf($"left:{CalculatedLeft!.ToUnit()}", () => CalculatedLeft != null)
                        .Add($"transform-origin:{Origin}")
                        .Add($"z-index:{ComputedZIndex}")
                        .Add(ContentStyle);
                });
        }
    }
}
