namespace Masa.Blazor
{
    public class MResponsive : BResponsive
    {
        public Dimensions Dimensions { get; set; }

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
                    var respSizerStyle = GenerateRespSizerStyle(AspectRatio, Dimensions);
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

        protected string GenerateRespSizerStyle(StringNumber aspectRatio, Dimensions dimensions)
        {
            double ratio = 2;
            if (aspectRatio == null)
            {
                if (dimensions == null)
                {
                    return "";
                }

                if (dimensions.Height != 0 && dimensions.Width != 0)
                {
                    ratio = dimensions.Width / dimensions.Height;
                }
            }
            else if (aspectRatio.TryGetNumber().isNumber)
            {
                ratio = aspectRatio.TryGetNumber().number;
            }

            var paddingBottom = (1 / ratio).ToString("P2");

            return $"padding-bottom: {paddingBottom.Replace(" ", "")}";
        }
    }
}