using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MOverlay : BOverlay
    {
        [Parameter]
        public bool Dark { get; set; } = true;

        protected override string OverlayCss =>
            CssBuilder.Clear()
                .Add("m-overlay")
                .AddIf("m-overlay--active", () => Value)
                .AddIf("m-overlay--absolute", () => Absolute)
                .AddTheme(Dark)
                .ToString();

        protected override string ScrimStyle =>
            ScrimStyleBuilder.Clear()
                .Add($"background-color: {Color}; border-color: {Color}")
                .Add(() => Value ? $"opacity: {Opacity.TryGetNumber().number}" : "opacity: 0")
                .ToString();

        protected override void SetComponentClass()
        {
            StyleBuilder
                .Add(() => $"z-index: {ZIndex.TryGetNumber().number}");

            ScrimCssBuilder
                .Add("m-overlay__scrim");

            ContentCssBuilder
                .Add("m-overlay__content");
        }
    }
}
