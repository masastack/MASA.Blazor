using BlazorComponent;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MToolbar : BToolbar
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-sheet theme--light m-toolbar")
                                .AddIf("m-toolbar--prominent", () => Prominent)
                                .AddIf("m-toolbar--flat", () => Flat)
                                .AddIf("m-toolbar--dense", () => Dense)
                                .AddBackgroundColor(Color, () => true);
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
