using System.Globalization;
using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Controls;
using Masa.Blazor.Components.Xgplayer.Plugins.CssFullscreen;
using Masa.Blazor.Components.Xgplayer.Plugins.DynamicBg;
using Masa.Blazor.Components.Xgplayer.Plugins.Mobile;
using Masa.Blazor.Components.Xgplayer.Plugins.Play;
using Masa.Blazor.Components.Xgplayer.Plugins.Start;
using Masa.Blazor.Components.Xgplayer.Plugins.Time;
using Masa.Blazor.Core;
using Masa.Blazor.Extensions;
using Masa.Blazor.JSComponents.Xgplayer;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public class MXgMusicPlayer : MasaComponentBase, IXgplayer
{
    [Inject] private I18n I18n { get; set; } = null!;

    /// <summary>
    /// Media resource URL, when the URL is in the form of an array,
    /// [Source tag](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/source) will be used for playback.
    /// </summary>
    [Parameter] [EditorRequired] public XgplayerUrl Url { get; set; } = null!;

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    /// <summary>
    /// If set with value <see langword="true"/> , player would invoke video.play() after enough media data loaded.
    /// </summary>
    /// <remarks>
    /// Notice In many cases, autoplay action was limited by browser policy, for more details see https://h5player.bytedance.com/guide/extends/aautoplay.html
    /// </remarks>
    [Parameter] public bool Autoplay { get; set; }

    /// <summary>
    /// Mute when autoplay
    /// </summary>
    [Parameter] public bool AutoplayMuted { get; set; }

    /// <summary>
    /// Default playback rate for a media element, reference values: 0.5, 0.75, 1, 1.5, 2
    /// </summary>
    [Parameter] [MasaApiParameter(1)] public double DefaultPlaybackRate { get; set; } = 1;

    /// <summary>
    /// Default volume for media element, reference values: 0 ~ 1
    /// </summary>
    [Parameter] [MasaApiParameter(0.6)] public double Volume { get; set; } = 0.6;

    /// <summary>
    /// Determine whether to play in a loop
    /// </summary>
    [Parameter] public bool Loop { get; set; }

    /// <summary>
    /// The second of the video to start playing
    /// </summary>
    [Parameter] public double StartTime { get; set; }

    /// <summary>
    /// Player language
    /// </summary>
    [Parameter] public string? Lang { get; set; }

    /// <summary>
    /// Player status after seeking
    /// </summary>
    [Parameter] public SeekedStatus SeekedStatus { get; set; }

    /// <summary>
    /// Marks for progress bar
    /// </summary>
    [Parameter] public IEnumerable<ProgressDot>? ProgressDots { get; set; }

    /// <summary>
    /// Whether to enable the screen and control bar separation mode,
    /// set to <see langword="false"/>, the control bar will be resident
    /// and will not overlap with the video screen.
    /// </summary>
    [Parameter] public bool MarginControls { get; set; }

    /// <summary>
    /// The list of plugins to be ignored.
    /// You can find all built-in plugins in <see cref="BuiltInPlugin"/>
    /// </summary>
    [Parameter] public IEnumerable<string>? Ignores { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback<bool> OnFullscreenChange { get; set; }

    [Parameter] public EventCallback<bool> OnCssFullscreenChange { get; set; }

    /// <summary>
    /// For most scenarios, you can use <see cref="OnFullscreenChange"/>.
    /// For scenarios where you need to click the full-screen button to enter full screen
    /// when using this component in Webview, you may need to use this event.
    /// </summary>
    [Parameter] public EventCallback OnFullscreenTouchend { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback<VideoMetadata> OnVideoMetadataLoaded { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback<VideoSize> OnVideoResize { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnPlay { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnPause { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnError { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnEnded { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnReady { get; set; }

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public string? ProgressColor { get; set; }

    [Parameter] [MasaApiParameter("#FFFFFF", ReleasedIn = "v1.11.0")]
    public string? PlayedColor { get; set; } = "#FFFFFF";

    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public string? CachedColor { get; set; }

    [Parameter] [MasaApiParameter("#FFFFFF", ReleasedIn = "v1.11.0")]
    public string? VolumeColor { get; set; } = "#FFFFFF";

    [Parameter] [MasaApiParameter("#FFFFFF", ReleasedIn = "v1.11.0")]
    public string? SliderBtnBackground { get; set; } = "#FFFFFF";

    [Parameter] [MasaApiParameter("#FFFFFF", ReleasedIn = "v1.11.0")]
    public string? SliderBtnShadow { get; set; } = "#FFFFFF";

    private CancellationTokenSource? _cancellationTokenSource;
    private bool _hasFirstRender;
    private XgplayerUrl? _prevUrl;
    private IJSObjectReference? _importJSObjectReference;
    private DotNetObjectReference<XgplayerJSInteropHandle>? _jsInteropHandle;

    private IXgplayerControls? _controlsPlugin;
    private IXgplayerPlay? _playPlugin;
    private IXgplayerTime? _timePlugin;
    private IXgplayerStart? _startPlugin;
    private IXgplayerMobile? _mobilePlugin;
    private IXgplayerCssFullscreen? _cssFullscreenPlugin;
    private IXgplayerFullscreen? _fullscreenPlugin;
    private IXgplayerVolume? _volumePlugin;
    private IXgplayerDynamicBg? _dynamicBgPlugin;
    private IXgplayerDownload? _downloadPlugin;

    private string ComputedLang
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Lang))
            {
                return Lang;
            }

            var culture = I18n.Culture;

            if (culture.TwoLetterISOLanguageName == "zh")
            {
                return IsZhHant(culture) ? "zh-hk" : "zh-cn";
            }

            return culture.TwoLetterISOLanguageName;
        }
    }

    protected XgplayerJSObjectReference? XgplayerJSObjectReference { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Url.ThrowIfNull(ComponentName);
        _prevUrl = Url;

        _jsInteropHandle = DotNetObjectReference.Create(new XgplayerJSInteropHandle(this));
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (XgplayerJSObjectReference == null)
        {
            return;
        }

        if (_prevUrl != Url)
        {
            _prevUrl = Url;

            await XgplayerJSObjectReference.UpdateUrlAsync(Url);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _hasFirstRender = true;
            await InitAsync();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddAttribute(2, "class", GetClass());
        builder.AddAttribute(3, "style", GetStyle());
        builder.AddElementReferenceCapture(4, elementReference => Ref = elementReference);
        builder.AddContent(5, RenderChildContent);
        builder.CloseElement();

        void RenderChildContent(RenderTreeBuilder nextBuilder)
        {
            nextBuilder.OpenComponent<CascadingValue<IXgplayer>>(0);
            nextBuilder.AddAttribute(1, "Value", this);
            nextBuilder.AddAttribute(2, "IsFixed", true);
            nextBuilder.AddAttribute(3, "ChildContent", ChildContent);
            nextBuilder.CloseComponent();
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-xgplayer-music";
    }

    private async Task InitAsync()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();

        if (_jsInteropHandle is null)
        {
            return;
        }

        await RunTaskInMicrosecondsAsync(CreateXgplayerAsync, 100, _cancellationTokenSource.Token);
    }

    private async Task CreateXgplayerAsync()
    {
        _importJSObjectReference = await Js
            .InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor.JSComponents.Xgplayer/xgplayer.js")
            .ConfigureAwait(false);

        var jsObjectReference =
            await _importJSObjectReference.InvokeAsync<IJSObjectReference>("init", Ref.GetSelector(), Url, GenOptions(),
                _jsInteropHandle);
        XgplayerJSObjectReference = new XgplayerJSObjectReference(jsObjectReference);
    }

    protected virtual XgplayerOptions GenOptions()
    {
        return new XgplayerOptions()
        {
            Width = Width.ToUnitOrNull(),
            Height = Height.ToUnitOrNull(),
            Autoplay = Autoplay,
            AutoplayMuted = AutoplayMuted,
            DefaultPlaybackRate = DefaultPlaybackRate,
            Loop = Loop,
            StartTime = StartTime,
            Lang = ComputedLang,
            SeekedStatus = SeekedStatus,
            ProgressDots = ProgressDots,
            MarginControls = MarginControls,
            Ignores = Ignores,
            CommonStyle = new CommonStyle(ProgressColor, PlayedColor, CachedColor, VolumeColor, SliderBtnBackground,
                SliderBtnShadow),
            Music = new XgplayerMusic(), // indicate that this is a music player
            Controls = _controlsPlugin,
            Play = _playPlugin,
            Time = _timePlugin,
            Start = _startPlugin,
            Mobile = _mobilePlugin,
            CssFullscreen = _cssFullscreenPlugin,
            Fullscreen = _fullscreenPlugin,
            Volume = _volumePlugin ?? new MXgplayerVolume { Default = Volume },
            DynamicBg = _dynamicBgPlugin,
            Download = _downloadPlugin
        };
    }

    public async Task ConfigPluginAsync(object plugin)
    {
        switch (plugin)
        {
            case IXgplayerControls controls:
                _controlsPlugin = controls;
                break;
            case IXgplayerPlay play:
                _playPlugin = play;
                break;
            case IXgplayerTime time:
                _timePlugin = time;
                break;
            case IXgplayerStart start:
                _startPlugin = start;
                break;
            case IXgplayerMobile mobile:
                _mobilePlugin = mobile;
                break;
            case IXgplayerCssFullscreen cssFullscreen:
                _cssFullscreenPlugin = cssFullscreen;
                break;
            case IXgplayerFullscreen fullscreen:
                _fullscreenPlugin = fullscreen;
                break;
            case IXgplayerVolume volume:
                _volumePlugin = volume;
                break;
            case IXgplayerDynamicBg dynamicBg:
                _dynamicBgPlugin = dynamicBg;
                break;
            case IXgplayerDownload download:
                _downloadPlugin = download;
                break;
        }

        ConfigPluginCore(plugin);

        if (_hasFirstRender)
        {
            await InitAsync();
        }
    }

    protected virtual void ConfigPluginCore(object plugin)
    {
    }

    [MasaApiPublicMethod]
    public async Task<XgplayerPropsAndStates?> GetPropsAndStatesAsync()
    {
        if (XgplayerJSObjectReference == null)
        {
            return null;
        }

        return await XgplayerJSObjectReference.GetPropsAndStatesAsync();
    }

    /// <summary>
    /// Invoke a method of xgplayer js object instance.
    /// </summary>
    /// <param name="identity">The name of method</param>
    /// <param name="args">The arguments of method</param>
    /// <example>
    /// player.InvokeVoidAsync("play");
    /// </example>
    [MasaApiPublicMethod]
    public async Task InvokeVoidAsync(string identity, params object[] args)
    {
        if (XgplayerJSObjectReference != null)
            await XgplayerJSObjectReference.InvokeInstanceVoidAsync(identity, args);
    }

    [MasaApiPublicMethod]
    public async Task SetPropAsync(string prop, object value)
    {
        if (XgplayerJSObjectReference != null)
            await XgplayerJSObjectReference.SetPropAsync(prop, value);
    }

    /// <summary>
    /// Toggle play state of the player.
    /// </summary>
    /// <param name="force">Force set the play state.</param>
    [MasaApiPublicMethod]
    public async Task TogglePlayAsync(bool? force = null)
    {
        if (XgplayerJSObjectReference != null)
            await XgplayerJSObjectReference.TogglePlayAsync(force);
    }

    /// <summary>
    /// Toggle the muted state of the player.
    /// </summary>
    /// <param name="force">Force set a muted state.</param>
    [MasaApiPublicMethod]
    public async Task ToggleMutedAsync(bool? force = null)
    {
        if (XgplayerJSObjectReference != null)
            await XgplayerJSObjectReference.ToggleMutedAsync(force);
    }

    private bool IsZhHant(CultureInfo culture)
    {
        if (culture.Parent.Name == "zh")
        {
            return culture.Name == "zh-Hant";
        }

        return IsZhHant(culture.Parent);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_importJSObjectReference != null)
        {
            await _importJSObjectReference.DisposeAsync();
            _importJSObjectReference = null;
        }

        if (XgplayerJSObjectReference != null)
        {
            try
            {
                await XgplayerJSObjectReference.DestroyAsync();
                await XgplayerJSObjectReference.DisposeAsync();
                XgplayerJSObjectReference = null;
            }
            catch (JSDisconnectedException)
            {
                // ignore
            }
        }
    }
}