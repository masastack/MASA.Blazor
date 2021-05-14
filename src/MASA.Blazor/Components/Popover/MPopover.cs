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
                         .Add(() => $"top: {ClientY.ToUnit()}")
                         .Add(() => $"left: {ClientX.ToUnit()}")
                         .AddMinWidth(MinWidth)
                         .AddMaxHeight(MaxHeight)
                         .AddFirstIf(
                             ("position: absolute", () => Visible),
                             ("display: none", () => !Visible)
                         );
                 });
        }
    }
}
