using System.Drawing;

namespace Masa.Blazor
{
    public class MBaiduMarker : BaiduOverlayBase, IMarker
    {
        [Parameter]
        [MasaApiParameter("116.403, 39.917")]
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

        [Parameter]
        [MasaApiParameter(ReleasedIn = "v1.7.1")]
        public BaiduMapIcon? Icon { get; set; }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string>(nameof(Title), (val) => InvokeJsMethod("setTitle", val))
                .Watch<GeoPoint>(nameof(Point), (val) => InvokeJsMethod("setPosition", val))
                .Watch<Size>(nameof(Offset), (val) => InvokeJsMethod("setOffset", val))
                .Watch<float>(nameof(Rotation), (val) => InvokeJsMethod("setRotation", val));

            return;

            void InvokeJsMethod(string methodName, object args)
            {
                _ = OverlayJSObjectRef.TryInvokeVoidAsync(methodName, args);
            }
        }
    }
}