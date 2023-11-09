using System.Drawing;

namespace Masa.Blazor
{
    public class MBaiduLabel : BaiduOverlayBase, ILabel
    {
        [Parameter]
        public string? Content
        {
            get => GetValue<string?>();
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter("116.403, 39.917")]
        public GeoPoint Position
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

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string>(nameof(Content), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setContent", val));

            watcher.Watch<GeoPoint>(nameof(Position), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setPosition", val));

            watcher.Watch<Size>(nameof(Offset), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setOffset", val));
        }
    }
}