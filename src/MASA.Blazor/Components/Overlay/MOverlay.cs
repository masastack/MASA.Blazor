using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MOverlay : BOverlay
    {
        [Parameter]
        public bool Dark { get; set; } = true;

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BOverlay>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay")
                        .AddIf("m-overlay--active", () => Value)
                        .AddIf("m-overlay--absolute", () => Absolute)
                        .AddTheme(Dark);
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
