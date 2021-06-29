using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MToolbar : BToolbar
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Collapse { get; set; }

        public bool IsCollapsed => Collapse;

        protected bool IsExtended { get; set; }

        [Parameter]
        public bool Floating { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-sheet")
                        .Add("m-toolbar")
                        .AddIf("m-toolbar--absolute", () => Absolute)
                        .AddIf("m-toolbar--bottom", () => Bottom)
                        .AddIf("m-toolbar--collapse", () => Collapse)
                        .AddIf("m-toolbar--collapsed", () => IsCollapsed)
                        .AddIf("m-toolbar--dense", () => Dense)
                        .AddIf("m-toolbar--extended", () => IsExtended)
                        .AddIf("m-toolbar--flat", () => Flat)
                        .AddIf("m-toolbar--floating", () => Floating)
                        .AddIf("m-toolbar--prominent", () => Prominent)
                        .AddBackgroundColor(Color)
                        .AddTheme(Dark);
                }, styleBuilder =>
                {
                    styleBuilder
                       .AddFirstIf(
                          ("height: 96px", () => Dense && Prominent),
                          ("height: 128px", () => Prominent),
                          ("height: 48px", () => Dense),
                          ("height: 64px", () => true)
                          )
                       .AddMinWidth(MinWidth)
                       .AddMaxWidth(MaxWidth)
                       .AddMinHeight(MinHeight)
                       .AddMaxHeight(MaxHeight);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder.Add("m-toolbar__content");
                }, styleBuilder =>
                {
                    styleBuilder
                       .AddFirstIf(
                          ("height: 96px", () => Dense && Prominent),
                          ("height: 128px", () => Prominent),
                          ("height: 48px", () => Dense),
                          ("height: 64px", () => true)
                          )
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight);
                });
        }
    }
}
