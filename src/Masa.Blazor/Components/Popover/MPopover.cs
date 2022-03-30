namespace Masa.Blazor
{
    public partial class MPopover : BPopover
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add(() => "z-index: 1000;")
                         .Add(() => $"top: {ClientY.ToUnit()}")
                         .Add(() => $"left: {ClientX.ToUnit()}")
                         .AddMinWidth(MinWidth)
                         .AddMaxHeight(MaxHeight)
                         .AddMaxWidth(MaxWidth)
                         .AddFirstIf(
                             ("display: none", () => !Visible)
                         );
                 });
        }
    }
}
