using BlazorComponent.Attributes;
using System.Drawing;
using System.Text.Json;

namespace Masa.Blazor
{
    public partial class MBaiduMap : BDomComponentBase, IThemeable, IMap, IAsyncDisposable
    {
        [Inject]
        public BaiduMapJSModule Module { get; set; }

        [Parameter]
        [DefaultValue(360)]
        public StringNumber Width { get; set; } = 360;

        [Parameter]
        [DefaultValue(240)]
        public StringNumber Height { get; set; } = 240;

        [Parameter]
        [DefaultValue(10)]
        public float Zoom
        {
            get => GetValue<float>(10);
            set
            {
                if (value >= DefaultMinZoom && value <= DefaultMaxZoom)
                    SetValue(value);
            }
        }

        [Parameter]
        [DefaultValue("116.403, 39.917")]
        public GeoPoint Center
        {
            get => GetValue<GeoPoint>(new(116.403f, 39.917f));
            set => SetValue(value);
        }

        [Parameter]
        [DefaultValue(false)]
        public bool EnableScrollWheelZoom
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        [Parameter]
        public string DarkThemeId { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        private IJSObjectReference _jsMap;

        public static float DefaultMaxZoom { get; } = 19;

        public static float DefaultMinZoom { get; } = 3;

        private bool _zoomChangedInJs = false;

        private bool _centerChangedInJs = false;

        private DotNetObjectReference<MBaiduMap> _objRef;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-baidumap")
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(Width)
                        .AddHeight(Height);
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _objRef = DotNetObjectReference.Create(this);

                _jsMap = await Module.InitAsync(Id, new BaiduMapInitOptions()
                {
                    EnableScrollWheelZoom = EnableScrollWheelZoom,
                    Zoom = Zoom,
                    Center = Center,
                    DarkThemeId = DarkThemeId,
                    Dark = Dark,
                }, _objRef);
            }
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher.Watch<float>(nameof(Zoom), async (val) =>
            {
                if (_jsMap is null)
                    return;

                if (_zoomChangedInJs)
                {
                    _zoomChangedInJs = false;
                    return;
                }

                await _jsMap.InvokeVoidAsync("setZoom", val);
            });

            Watcher.Watch<bool>(nameof(EnableScrollWheelZoom), async (val) =>
            {
                if (_jsMap is null)
                    return;

                await _jsMap.InvokeVoidAsync(val ? "enableScrollWheelZoom" : "disableScrollWheelZoom");
            });

            Watcher.Watch<bool>(nameof(Dark), async (val) =>
            {
                if (_jsMap is null)
                    return;

                await _jsMap.InvokeVoidAsync("setMapStyleV2", new { StyleId = val ? DarkThemeId : string.Empty });
            });

            Watcher.Watch<GeoPoint>(nameof(Center), async (val) =>
            {
                if (_jsMap is null)
                    return;

                if (_centerChangedInJs)
                {
                    _centerChangedInJs = false;
                    return;
                }

                await _jsMap.InvokeVoidAsync("panTo", val);
            });
        }

        [JSInvokable]
        public void OnJsZoomEnd(float zoom)
        {
            _zoomChangedInJs = true;
            Zoom = zoom;
        }

        [JSInvokable]
        public void OnJsMoveEnd(GeoPoint point)
        {
            _centerChangedInJs = true;
            Center = point;
        }

        public async ValueTask DisposeAsync()
        {
            _objRef?.Dispose();

            if (_jsMap is not null)
                await _jsMap.DisposeAsync();
        }
    }
}
