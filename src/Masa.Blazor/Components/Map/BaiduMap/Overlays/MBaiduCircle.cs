using BlazorComponent.Attributes;

namespace Masa.Blazor
{
    public class MBaiduCircle : BaiduOverlayBase, ICircle, IStroke, IFillable
    {
        [Parameter]
        [MassApiParameter("116.403, 39.917")]
        public GeoPoint Center
        {
            get => GetValue<GeoPoint>(new(116.403f, 39.917f));
            set => SetValue(value);
        }

        [Parameter]
        public float Radius
        {
            get => GetValue<float>();
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
        public string? FillColor
        {
            get => GetValue<string?>();
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter(0.3f)]
        public float FillOpacity
        {
            get => GetValue(0.3f);
            set => SetValue(value);
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<GeoPoint>(nameof(Center), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setCenter", val));

            watcher.Watch<float>(nameof(Radius), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setRadius", val));

            watcher.Watch<string>(nameof(StrokeColor), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeColor", val));

            watcher.Watch<float>(nameof(StrokeOpacity), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeOpacity", val));

            watcher.Watch<float>(nameof(StrokeWeight), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeWeight", val));

            watcher.Watch<StrokeStyle>(nameof(StrokeStyle), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setStrokeStyle", StrokeStyle == StrokeStyle.Solid ? 0 : 1));

            watcher.Watch<string>(nameof(FillColor), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setFillColor", val));

            watcher.Watch<float>(nameof(FillOpacity), async (val)
                => await OverlayJSObjectRef.TryInvokeVoidAsync("setFillOpacity", val));
        }
    }
}