using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MPopover : BPopover
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BPopover>(styleAction:styleBuilder =>
                {
                    styleBuilder
                        .Add(() => "z-index: 1000;")
                        .Add(() => $"top: {ClientY?.TryGetNumber().number ?? 0}px")
                        .Add(() => $"left: {ClientX?.TryGetNumber().number ?? 0}px")
                        .Add(() => $"min-width: {MinWidth?.TryGetNumber().number ?? 0}px")
                        .Add(() => $"max-height: {MaxHeight?.TryGetNumber().number}px")
                        .AddFirstIf(
                            ("position: absolute", () => Visible),
                            ("display: none", () => !Visible)
                        );
                });
        }
    }
}
