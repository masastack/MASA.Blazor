using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MPopover : BPopover
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BPopover>(styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add(() => "z-index: 1000;")
                         .Add(() => $"top: {ClientY?.TryGetNumber().number ?? 0}px")
                         .Add(() => $"left: {ClientX?.TryGetNumber().number ?? 0}px")
                         .AddIf(() => $"min-width: {MinWidth?.TryGetNumber().number ?? 0}px", () => MinWidth != null)
                         .AddIf(() => $"max-height: {MaxHeight?.TryGetNumber().number}px", () => MaxHeight != null)
                         .AddFirstIf(
                             ("position: absolute", () => Visible),
                             ("display: none", () => !Visible)
                         );
                 });
        }
    }
}
