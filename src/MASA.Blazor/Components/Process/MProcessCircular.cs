using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MProcessCircular : BProcessCircular
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular")
                        .AddIf("m-progress-circular--indeterminate", () => Indeterminate)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Size)
                        .AddWidth(Size)
                        .AddTextColor(Color);
                });
        }
    }
}
