using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MProcessCircular : BProcessCircular
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BProcessCircular>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular")
                        .AddIf("m-progress-circular--indeterminate", () => Indeterminate)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => Size.Value.Match(
                                str => $"height: {str}; width: {str}",
                                num => $"height: {num}px; width: {num}px"),
                            () => Size.HasValue)
                        .AddTextColor(Color);
                });
        }
    }
}
