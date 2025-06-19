using System.Collections;
using Masa.Blazor.Components.Xgplayer;

namespace Masa.Blazor;

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
        return left.AsEnumerable().Select(u => u.Src).SequenceEqual(right.AsEnumerable().Select(u => u.Src));
    }

    public static bool operator !=(XgplayerUrl left, XgplayerUrl right)
    {
        return !(left == right);
    }
}
