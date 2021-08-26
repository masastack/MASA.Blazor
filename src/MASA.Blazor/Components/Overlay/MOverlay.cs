using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MOverlay : BOverlay, IThemeable
    {

        [Parameter]
        public bool Dark { get; set; } = true;

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
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

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay")
                        .AddIf("m-overlay--active", () => Value)
                        .AddIf("m-overlay--absolute", () => Absolute)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"z-index: {ZIndex}")
                        .AddIf("visibility:hidden", () => !Value);
                })
                .Apply("scrim", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay__scrim");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color)
                        .Add(() => Value ? $"opacity: {Opacity.TryGetNumber().number}" : "opacity: 0");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay__content");
                });
        }
    }
}
