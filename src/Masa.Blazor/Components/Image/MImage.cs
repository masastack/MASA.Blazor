using System.Text;
using Masa.Blazor.Components.Image;

namespace Masa.Blazor
{
    public partial class MImage : MResponsive, IImage, IThemeable, IAsyncDisposable
    {
        [Inject]
        private IntersectJSModule IntersectJSModule { get; set; } = null!;

        [Parameter]
        public bool Contain { get; set; }

        [Parameter]
        public bool Eager { get; set; }

        // TODO: support for string | SrcObject
        [Parameter, EditorRequired]
        public string? Src
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        // TODO: support for SrcSet and Sizes
        // [Parameter]
        // public string? Srcset { get; set; }

        // [Parameter]
        // public string Sizes { get; set; }

        [Parameter]
        public string? LazySrc { get; set; }

        [Parameter]
        public string? Gradient { get; set; }

        [Parameter]
        public RenderFragment? PlaceholderContent { get; set; }

        [Parameter]
        [MassApiParameter("center center")]
        public string? Position { get; set; } = "center center";

        [Parameter]
        [MassApiParameter("fade-transition")]
        public string? Transition { get; set; } = "fade-transition";

        private string? _currentSrc;
        private bool _isError = false;

        private StringNumber? _calculatedLazySrcAspectRatio;
        private ImageDimensions? _dimensions;

        public bool IsLoading { get; private set; } = true;

        public override StringNumber? ComputedAspectRatio => NormalisedSrc.Aspect ?? _calculatedLazySrcAspectRatio;

        private SrcObject NormalisedSrc => new()
        {
            Src = Src,
            LazySrc = LazySrc,
            Aspect = AspectRatio
        };

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string>(nameof(Src), SrcChangeCallback);
        }

        private async void SrcChangeCallback()
        {
            if (!IsLoading)
            {
                await Init(true);
            }
            else
            {
                await LoadImageAsync();
                await InvokeStateHasChangedAsync();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await IntersectJSModule.ObserverAsync(Ref, e => Init(e.IsIntersecting), new IntersectionObserverInit(true));

                await Init();
            }
        }

        private async Task<ImageDimensions> GetImageDimensionsAsync(string src)
        {
            return await JsInvokeAsync<ImageDimensions>(JsInteropConstants.GetImageDimensions, src);
        }

        private async Task Init(bool isIntersecting = false)
        {
            if (!isIntersecting && !Eager)
            {
                return;
            }

            if (!string.IsNullOrEmpty(NormalisedSrc.LazySrc))
            {
                await PollForSize(NormalisedSrc.LazySrc, null);
                await InvokeAsync(StateHasChanged);
            }

            if (!string.IsNullOrEmpty(NormalisedSrc.Src))
            {
                await LoadImageAsync();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadImageAsync()
        {
            _isError = false;
            if (string.IsNullOrWhiteSpace(NormalisedSrc.Src))
            {
                _isError = true;
            }
            else
            {
                await PollForSize(NormalisedSrc.Src);
                _isError = _dimensions == null || _dimensions.HasError;
                if (!_isError)
                {
                    OnLoad();
                }
            }
        }

        private void OnLoad()
        {
            IsLoading = false;
            if (_dimensions.Height != 0 & _dimensions.Width != 0)
            {
                _calculatedLazySrcAspectRatio = _dimensions.Width / _dimensions.Height;
            }
            else
            {
                _calculatedLazySrcAspectRatio = 1;
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

        private async Task PollForSize(string imgSrc, int? timeOut = 100)
        {
            while (true)
            {
                var dimensions = await GetImageDimensionsAsync(imgSrc);

                if (_dimensions != null)
                {
                    _dimensions.HasError = dimensions.HasError;
                }

                if (dimensions.Width != 0 || dimensions.Height != 0)
                {
                    _dimensions = dimensions;
                    _calculatedLazySrcAspectRatio = dimensions.Width / dimensions.Height;
                }
                else if (IsLoading && !dimensions.HasError && timeOut != null)
                {
                    await Task.Delay(timeOut.Value);
                    continue;
                }

                break;
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                await IntersectJSModule.UnobserveAsync(Ref);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
