using System.Drawing;

namespace Masa.Blazor
{
    public class MBaiduMarker : BaiduOverlayBase, IMarker
    {
        [Parameter]
        [MassApiParameter("116.403, 39.917")]
        public GeoPoint Point
        {
            get => GetValue<GeoPoint>(new(116.403f, 39.917f));
            set => SetValue(value);
        }

        [Parameter]
        public Size Offset
        {
            get => GetValue<Size>();
            set => SetValue(value);
        }

        [Parameter]
        public float Rotation
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        [Parameter]
        public string? Title
        {
            get => GetValue<string?>();
            set => SetValue(value);
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string>(nameof(Title), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setTitle", val));

            watcher.Watch<GeoPoint>(nameof(Point), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setPosition", val));

            watcher.Watch<Size>(nameof(Offset), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setOffset", val));

            watcher.Watch<float>(nameof(Rotation), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setRotation", val));
        }
    }
}