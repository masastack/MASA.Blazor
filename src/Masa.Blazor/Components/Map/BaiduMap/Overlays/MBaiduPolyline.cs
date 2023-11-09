using BlazorComponent.Attributes;

namespace Masa.Blazor
{
    public class MBaiduPolyline : BaiduOverlayBase, IPolyline, IStroke
    {
        [Parameter]
        [EditorRequired]
        public IEnumerable<GeoPoint> Points
        {
            get => GetValue<IEnumerable<GeoPoint>>() ?? Array.Empty<GeoPoint>();
            set => SetValue(value);
        }

        [Parameter]
        public string? StrokeColor
        {
            get => GetValue<string?>();
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter(0.9f)]
        public float StrokeOpacity
        {
            get => GetValue(0.9f);
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter(2f)]
        public float StrokeWeight
        {
            get => GetValue(2f);
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter(StrokeStyle.Solid)]
        public StrokeStyle StrokeStyle
        {
            get => GetValue(StrokeStyle.Solid);
            set => SetValue(value);
        }

        [Parameter]
        public bool Geodesic { get; set; } = false;

        [Parameter]
        [MassApiParameter(true)]
        public bool Clip { get; set; } = true;

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<IEnumerable<GeoPoint>>(nameof(Points), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setPath", val));

            watcher.Watch<string>(nameof(StrokeColor), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeColor", val));

            watcher.Watch<float>(nameof(StrokeOpacity), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeOpacity", val));

            watcher.Watch<float>(nameof(StrokeWeight), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeWeight", val));

            watcher.Watch<StrokeStyle>(nameof(StrokeStyle), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeStyle", StrokeStyle == StrokeStyle.Solid ? 0 : 1));
        }

    }
}