using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MImage : BImage
    {
        private Dimensions _dimensions;

        [Parameter]
        public bool Dark { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dimensions = await JsInvokeAsync<Dimensions>(JsInteropConstants.GetImageDimensions, Src);

                RespSizerStyle = GenerateRespSizerStyle(_dimensions, AspectRatio);
                await InvokeStateHasChangedAsync();
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BImage>()
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-image")
                        .Add("m-responsive")
                        .AddTheme(Dark);
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
                .Apply("resp_sizer", cssBuilder =>
                {
                    cssBuilder.Add("m-responsive__sizer");
                })
                .Apply("image", cssBuilder =>
                {
                    cssBuilder.Add("m-image__image")
                        .AddFirstIf(
                            ("m-image__image--contain", () => Contain),
                            ("m-image__image--cover", () => true)
                        );
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"background-image: url(\"{Src}\")", () => !string.IsNullOrEmpty(Src))
                        .Add("background-position: center center");
                })
                .Apply("resp_content", cssBuilder =>
                {
                    cssBuilder.Add("m-responsive__content");
                });
        }

        private static string GenerateRespSizerStyle(Dimensions dimensions, StringNumber aspectRatio)
        {
            double ratio = 2;
            if (aspectRatio == null)
            {
                if (dimensions != null && dimensions.Height != 0 && dimensions.Width != 0)
                {
                    ratio = dimensions.Width / dimensions.Height;
                }
            }
            else if (aspectRatio.TryGetNumber().isNumber)
            {
                ratio = aspectRatio.TryGetNumber().number;
            }

            var paddingBottom = (1 / ratio).ToString("P2");

            return $"padding-bottom: {paddingBottom}";
        }
    }
}