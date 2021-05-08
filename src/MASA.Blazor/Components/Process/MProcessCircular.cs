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
                        .AddIf("m-progress-circular--indeterminate", () => Indeterminate);

                    if (!string.IsNullOrWhiteSpace(Color) && !Color.StartsWith("#"))
                    {
                        cssBuilder.Add($"{Color}--text");
                    }
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => Size.Value.Match(
                                str => $"height: {str}; width: {str}",
                                num => $"height: {num}px; width: {num}px"),
                            () => Size.HasValue);

                    if (!string.IsNullOrWhiteSpace(Color) && Color.StartsWith("#"))
                    {
                        styleBuilder
                            .Add($"color: {Color}");
                    }
                });
        }
    }
}
