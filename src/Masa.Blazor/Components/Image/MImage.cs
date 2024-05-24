using Masa.Blazor.Components.Image;

namespace Masa.Blazor;

public partial class MImage : MResponsive, IThemeable
{
    [Inject]
    private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

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
    [MasaApiParameter("center center")]
    public string? Position { get; set; } = "center center";

    [Parameter]
    [MasaApiParameter("fade-transition")]
    public string? Transition { get; set; } = "fade-transition";

    private string? _currentSrc;
    private bool _isError = false;

    private StringNumber? _calculatedLazySrcAspectRatio;
    private ImageDimensions? _dimensions;

    public bool IsLoading { get; private set; } = true;

    protected override StringNumber? ComputedAspectRatio => NormalisedSrc.Aspect ?? _calculatedLazySrcAspectRatio;

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
        IsLoading = string.IsNullOrWhiteSpace(Src);

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
        return await Js.InvokeAsync<ImageDimensions>(JsInteropConstants.GetImageDimensions, src);
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

    private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
        _currentSrc = Src;
    }

    private Block _block = new("m-image");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(_block.AddTheme(IsDark, IndependentTheme).GenerateCssClasses());
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

        if (!string.IsNullOrEmpty(Gradient))
        {
            stringBuilder.Append($"linear-gradient({Gradient}),");
        }

        stringBuilder.Append($"url(\"{url}\")");
        return stringBuilder.ToString();
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

    protected override async ValueTask DisposeAsyncCore()
    {
        await IntersectJSModule.UnobserveAsync(Ref);
    }
}