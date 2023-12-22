using System.Collections;
using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.Components.Xgplayer.Plugins;
using Masa.Blazor.Components.Xgplayer.Plugins.Controls;
using Masa.Blazor.Components.Xgplayer.Plugins.Play;

namespace Masa.Blazor;

public class MXgplayer : MXgMusicPlayer
{
    /// <summary>
    /// Loading media resource immediately after player initialized.
    /// </summary>
    [Parameter] [MasaApiParameter(true)] public bool VideoInit { get; set; } = true;

    /// <summary>
    /// Enable inline playing mode, would set playsinline DOM attribute to media element.
    /// For more details about inline playing mode see https://webkit.org/blog/6784/new-video-policies-for-ios/
    /// </summary>
    [Parameter] [MasaApiParameter(true)] public bool Playsinline { get; set; } = true;

    /// <summary>
    /// Post image of video
    /// </summary>
    [Parameter] public string? Poster { get; set; }

    /// <summary>
    /// DOM attributes for media element, for more details
    /// see https://developer.mozilla.org/en-US/docs/Web/API/HTMLMediaElement
    /// </summary>
    [Parameter] public Dictionary<string, object?>? VideoAttributes { get; set; }

    /// <summary>
    /// The fluid layout allows the player's width varies to follow the width of the parent element's change,
    /// and the height varies according to the height and width proportion of the configuration item
    /// (the player's width and height is the internal default value when width and height are not set).
    /// </summary>
    [Parameter] public bool Fluid { get; set; }

    /// <summary>
    /// When video resource was playing, fit raw height、width of video resource to player's
    /// </summary>
    [Parameter] public FitVideoSize FitVideoSize { get; set; }

    /// <summary>
    /// The fill mode of media resource
    /// </summary>
    [Parameter] public VideoFillMode VideoFillMode { get; set; }

    /// <summary>
    /// Thumbnail for user to preview unplayed video content
    /// </summary>
    [Parameter] public Thumbnail? Thumbnail { get; set; }

    private IXgplayerControls? _controls;
    private IXgplayerPlay? _play;

    protected override void SetComponentCss()
    {
        CssProvider.UseBem("m-xgplayer");
    }

    protected override XgplayerOptions GenOptions()
    {
        var options = base.GenOptions();

        // indicate that this is a video player, not a music player
        options.Music = null;

        // properties only for video player
        options.VideoInit = VideoInit;
        options.Playsinline = Playsinline;
        options.Poster = Poster;
        options.Fluid = Fluid;
        options.FitVideoSize = FitVideoSize;
        options.VideoFillMode = VideoFillMode;
        options.Thumbnail = Thumbnail;
        options.VideoAttributes = VideoAttributes;

        return options;
    }

    protected override void ConfigPluginCore(object plugin)
    {
    }

    [MasaApiPublicMethod]
    public async Task SwitchToMusicAsync(XgplayerUrl url)
    {
        await XgplayerJSObjectReference.SwitchToMusicAsync(url);
    }

    [MasaApiPublicMethod]
    public async Task SwitchToVideoAsync(XgplayerUrl url)
    {
        await XgplayerJSObjectReference.SwitchToVideoAsync(url);
    }
}

public class OneOrMore<T> : OneOfBase<T, IEnumerable<T>>, IEnumerable<T>
{
    protected OneOrMore(OneOf<T, IEnumerable<T>> input) : base(input)
    {
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (IsT0)
        {
            yield return AsT0;
        }
        else
        {
            foreach (var item in AsT1)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator OneOrMore<T>(T value) => new(value);

    public static implicit operator OneOrMore<T>(T[] value) => new(value);

    public static implicit operator OneOrMore<T>(List<T> value) => new(value);
}

public class XgplayerUrl : OneOfBase<string, MediaStreamUrl, IEnumerable<MediaStreamUrl>>, IEnumerable<MediaStreamUrl>
{
    protected XgplayerUrl(OneOf<string, MediaStreamUrl, IEnumerable<MediaStreamUrl>> input) : base(input)
    {
    }

    public IEnumerator<MediaStreamUrl> GetEnumerator()
    {
        if (IsT0)
        {
            yield return new MediaStreamUrl(AsT0);
        }
        else if (IsT1)
        {
            yield return AsT1;
        }
        else
        {
            foreach (var item in AsT2)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator XgplayerUrl(string _) => new(_);

    public static implicit operator XgplayerUrl(MediaStreamUrl _) => new(_);

    public static implicit operator XgplayerUrl(MediaStreamUrl[] _) => new(_);

    public static implicit operator XgplayerUrl(List<MediaStreamUrl> _) => new(_);

    public static bool operator ==(XgplayerUrl left, XgplayerUrl right)
    {
        return left.AsEnumerable().SequenceEqual(right.AsEnumerable());
    }

    public static bool operator !=(XgplayerUrl left, XgplayerUrl right)
    {
        return !(left == right);
    }
}
