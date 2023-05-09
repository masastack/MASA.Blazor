using System.Text;

namespace Masa.Blazor
{
    //Todo：Crossover mode to be perfected
    public partial class MImage : MResponsive, IImage, IThemeable
    {
        [Parameter]
        public bool Contain { get; set; }

        [Parameter]
        public string? Src { get; set; }

        [Parameter]
        public string? LazySrc { get; set; }

        [Parameter]
        public string? Gradient { get; set; }

        [Parameter]
        public RenderFragment? PlaceholderContent { get; set; }

        [Parameter]
        [ApiDefaultValue("center center")]
        public string? Position { get; set; } = "center center";

        private string? _currentSrc;
        private bool _isError = false;

        private StringNumber? _calculatedLazySrcAspectRatio;
        private Dimensions? _dimensions;

        public bool IsLoading { get; private set; } = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!string.IsNullOrEmpty(LazySrc) && IsLoading && _dimensions == null)
            {
                var dimensions = await JsInvokeAsync<Dimensions>(JsInteropConstants.GetImageDimensions, LazySrc);
                await PollForSize(dimensions);
                if (!dimensions.HasError && AspectRatio == null)
                {
                    AspectRatio = dimensions.Width / dimensions.Height;
                    _calculatedLazySrcAspectRatio = AspectRatio;
                }

                await InvokeStateHasChangedAsync();
            }

            if (!string.IsNullOrEmpty(Src) && IsLoading && !_isError)
            {
                var dimensions = await JsInvokeAsync<Dimensions>(JsInteropConstants.GetImageDimensions, Src);
                await PollForSize(dimensions);
                _isError = dimensions.HasError;
                if (!dimensions.HasError)
                {
                    IsLoading = false;
                    if (AspectRatio == null || _calculatedLazySrcAspectRatio != null)
                    {
                        AspectRatio = dimensions.Width / dimensions.Height;
                    }
                }

                await InvokeStateHasChangedAsync();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-image")
                        .AddTheme(IsDark);
                })
                .Apply("image", cssBuilder =>
                {
                    cssBuilder.Add("m-image__image")
                              .AddIf("m-image__image--preload", () => IsLoading)
                              .AddIf("m-image__image--contain", () => Contain)
                              .AddIf("m-image__image--cover", () => !Contain);
                }, styleBuilder =>
                {
                    var url = GetBackgroundImageUrl();
                    styleBuilder
                        .AddIf(GetBackgroundImage(url!), () => !string.IsNullOrEmpty(url))
                        .AddIf($"background-position: {Position}", () => !string.IsNullOrEmpty(Position));
                })
                .Apply("placeholder", cssBuilder => { cssBuilder.Add("m-image__placeholder"); });

            AbstractProvider
                .Apply(typeof(BImageContent<>), typeof(BImageContent<MImage>))
                .Apply(typeof(BPlaceholderSlot<>), typeof(BPlaceholderSlot<MImage>))
                .Merge(typeof(BResponsiveBody<>), typeof(BImageBody<MImage>))
                .Apply<BResponsive, MResponsive>(attrs =>
                {
                    attrs[nameof(_dimensions)] = _dimensions;
                    attrs[nameof(AspectRatio)] = AspectRatio;
                    attrs[nameof(ContentClass)] = ContentClass;
                    attrs[nameof(Height)] = Height;
                    attrs[nameof(MinHeight)] = MinHeight;
                    attrs[nameof(MaxHeight)] = MaxHeight;
                    attrs[nameof(MinWidth)] = MinWidth;
                    attrs[nameof(MaxWidth)] = MaxWidth;
                });
        }

        private string? GetBackgroundImageUrl()
        {
            if (string.IsNullOrEmpty(Src) && string.IsNullOrEmpty(LazySrc))
            {
                return null;
            }

            return IsLoading || _isError ? LazySrc : _currentSrc;
        }

        private string? GetBackgroundImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            StringBuilder stringBuilder = new();
            stringBuilder.Append("background-image:");

            if (!string.IsNullOrEmpty(Gradient))
            {
                stringBuilder.Append($"linear-gradient({Gradient}),");
            }

            stringBuilder.Append($"url(\"{url}\")");
            return stringBuilder.ToString();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _currentSrc = Src;
        }

        private async Task PollForSize(Dimensions dimensions, int? timeOut = 100)
        {
            if (IsLoading && !dimensions.HasError && timeOut != null)
            {
                await Task.Delay(timeOut.Value);
            }

            _dimensions = dimensions;
        }
    }
}
