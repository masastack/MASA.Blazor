using System.Drawing;
using System.Text.Json;

namespace Masa.Blazor
{
    public partial class MBaiduMap : BDomComponentBase, IThemeable, IMap
    {
        [Inject]
        public BaiduMapJSModule Module { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 360;

        [Parameter]
        public StringNumber Height { get; set; } = 240;

        [Parameter]
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
        public PointF Center
        {
            get => GetValue<PointF>(new(116.403f, 39.917f));
            set => SetValue(value);
        }

        [Parameter]
        public bool EnableScrollWheelZoom
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        [Parameter]
        [EditorRequired]
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

        //[Parameter]
        //public EventCallback<float> OnZoomChanged { get; set; }

        private IJSObjectReference JsMap { get; set; }

        public static float DefaultMaxZoom { get; } = 19;

        public static float DefaultMinZoom { get; } = 3;

        private bool ZoomChangedInJs = false;

        private bool CenterChangedInJs = false;

        private DotNetObjectReference<MBaiduMap> ObjRef { get; set; }

        private JsonSerializerOptions SerializerOptions = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            SerializerOptions.PropertyNameCaseInsensitive = true;
        }

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
                ObjRef = DotNetObjectReference.Create(this);

                JsMap = await Module.InitMapAsync(Id, new BaiduMapInitOption()
                {
                    EnableScrollWheelZoom = EnableScrollWheelZoom,
                    Zoom = Zoom,
                    Center = Center,
                    DarkThemeId = DarkThemeId,
                    Dark = Dark,
                }, ObjRef);

                StateHasChanged();
            }
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher.Watch<float>(nameof(Zoom), async (val) =>
            {
                if (JsMap is null)
                    return;

                if (ZoomChangedInJs)
                {
                    ZoomChangedInJs = false;
                    return;
                }

                await JsMap.InvokeVoidAsync("setZoom", val);
            });

            Watcher.Watch<bool>(nameof(EnableScrollWheelZoom), async (val) =>
            {
                if (JsMap is null)
                    return;

                if (val)
                    await JsMap.InvokeVoidAsync("enableScrollWheelZoom");
                else
                    await JsMap.InvokeVoidAsync("disableScrollWheelZoom");
            });

            Watcher.Watch<bool>(nameof(Dark), async (val) =>
            {
                if (JsMap is null)
                    return;

                if (val)
                    await JsMap.InvokeVoidAsync("setMapStyleV2", new { StyleId = DarkThemeId });
                else
                    await JsMap.InvokeVoidAsync("setMapStyleV2", new { StyleId = string.Empty });
            });

            Watcher.Watch<PointF>(nameof(Center), async (val) =>
            {
                if (JsMap is null)
                    return;

                if (CenterChangedInJs)
                {
                    CenterChangedInJs = false;
                    return;
                }

                await JsMap.InvokeVoidAsync("panTo", val.ToGeoPoint());
            });
        }

        [JSInvokable]
        public void OnJsZoomEnd(object zoomJson)
        {
            ZoomChangedInJs = true;
            Zoom = zoomJson.ToString().ToObject<float>();
        }

        [JSInvokable]
        public void OnJsMoveEnd(object pointJson)
        {
            CenterChangedInJs = true;
            Center = pointJson.ToString().ToObject<GeoPoint>(SerializerOptions).ToPointF();
        }

    }
}
