using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Controls;
using Masa.Blazor.Components.Xgplayer.Plugins.Play;
using Masa.Blazor.Components.Xgplayer.Plugins.Start;
using Masa.Blazor.Components.Xgplayer.Plugins.Time;

namespace Masa.Blazor;

public class MXgMusicPlayer : BDomComponentBase, IXgplayer
{
    [Inject] private XgplayerJSModule XgplayerJSModule { get; set; } = default!;

    [Inject] private I18n I18n { get; set; } = default!;

    /// <summary>
    /// Media resource URL, when the URL is in the form of an array,
    /// [Source tag](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/source) will be used for playback.
    /// </summary>
    [Parameter] [EditorRequired] public XgplayerUrl Url { get; set; } = default!;

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
    /// Default playback rate for media element, reference values: 0.5, 0.75, 1, 1.5, 2
    /// </summary>
    [Parameter] [MasaApiParameter(1)] public float DefaultPlaybackRate { get; set; } = 1;

    /// <summary>
    /// Default volume for media element, reference values: 0 ~ 1
    /// </summary>
    [Parameter] [MasaApiParameter(0.6)] public float Volume { get; set; } = 0.6f;

    /// <summary>
    /// Determine whether to play in a loop
    /// </summary>
    [Parameter] public bool Loop { get; set; }

    /// <summary>
    /// The second of video to start playing
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
    /// For scenarios where you need to click the full screen button to enter full screen
    /// when using this component in Webview, you may need to use this event.
    /// </summary>
    [Parameter] public EventCallback OnFullscreenTouchend { get; set; }

    private CancellationTokenSource? _cancellationTokenSource;
    private bool _hasFirstRender;
    private XgplayerUrl? _prevUrl;
    private DotNetObjectReference<XgplayerJSInteropHandle>? _jsInteropHandle;

    private IXgplayerControls? _controls;
    private IXgplayerPlay? _play;
    private IXgplayerTime? _time;
    private IXgplayerStart? _start;

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
        builder.AddAttribute(1, "class", CssProvider.GetClass());
        builder.AddAttribute(2, "style", CssProvider.GetStyle());
        builder.AddElementReferenceCapture(3, elementReference => Ref = elementReference);
        builder.AddContent(4, RenderChildContent);
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

    protected override void SetComponentCss()
    {
        base.SetComponentCss();

        CssProvider.UseBem("m-xgplayer-music");
    }

    private async Task InitAsync()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();

        if (_jsInteropHandle is null)
        {
            return;
        }

        await RunTaskInMicrosecondsAsync(
            async () => XgplayerJSObjectReference = await XgplayerJSModule.InitAsync(Ref.GetSelector(), Url, GenOptions(), _jsInteropHandle),
            100,
            _cancellationTokenSource.Token);
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
            Volume = Volume,
            Loop = Loop,
            StartTime = StartTime,
            Lang = ComputedLang,
            SeekedStatus = SeekedStatus,
            ProgressDots = ProgressDots,
            MarginControls = MarginControls,
            Ignores = Ignores,
            Music = new XgplayerMusic(), // indicate that this is a music player
            Controls = _controls,
            Play = _play,
            Time = _time,
            Start = _start,
        };
    }

    public async Task ConfigPluginAsync(object plugin)
    {
        switch (plugin)
        {
            case IXgplayerControls controls:
                _controls = controls;
                break;
            case IXgplayerPlay play:
                _play = play;
                break;
            case IXgplayerTime time:
                _time = time;
                break;
            case IXgplayerStart start:
                _start = start;
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
    public async Task<XgplayerPropsAndStates> GetPropsAndStatesAsync()
    {
        return await XgplayerJSObjectReference!.GetPropsAndStatesAsync();
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
        await XgplayerJSObjectReference.InvokeVoidAsync(identity, args);
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
        if (XgplayerJSObjectReference != null)
        {
            try
            {
                await XgplayerJSObjectReference.DestroyAsync();
                await XgplayerJSObjectReference.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // ignore
            }
        }
    }
}
