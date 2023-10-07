namespace Masa.Blazor;

public class MResponsive : BResponsive
{
    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(cssBuilder =>
            {
                cssBuilder
                    .Add("m-responsive");
            }, styleBuilder =>
            {
                styleBuilder
                    .AddHeight(Height)
                    .AddWidth(Width)
                    .AddMinWidth(MinWidth)
                    .AddMaxWidth(MaxWidth)
                    .AddMinHeight(MinHeight)
                    .AddMaxHeight(MaxHeight);
            })
            .Apply("sizer", cssBuilder => { cssBuilder.Add("m-responsive__sizer"); }, styleBuilder =>
            {
                var respSizerStyle = GenAspectStyle(ComputedAspectRatio);
                styleBuilder
                    .AddIf(respSizerStyle, () => !string.IsNullOrEmpty(respSizerStyle));
            })
            .Apply("resp_content", cssBuilder =>
            {
                cssBuilder.Add("m-responsive__content")
                          .AddIf(ContentClass, () => !string.IsNullOrEmpty(ContentClass));
            });

        AbstractProvider
            .Apply(typeof(BResponsiveContent<>), typeof(BResponsiveContent<MResponsive>))
            .Apply(typeof(BResponsiveBody<>), typeof(BResponsiveBody<MResponsive>))
            .Apply(typeof(BResponsiveSizer<>), typeof(BResponsiveSizer<MResponsive>));
    }

    private string? GenAspectStyle(StringNumber? aspectRatio)
    {
        if (aspectRatio == null) return null;
            
        var (isNumber, ratio) = aspectRatio.TryGetNumber();

        if (!isNumber) return null;

        var paddingBottom = (1 / ratio).ToString("P2");

        return $"padding-bottom: {paddingBottom.Replace(" ", "")}";
    }
}