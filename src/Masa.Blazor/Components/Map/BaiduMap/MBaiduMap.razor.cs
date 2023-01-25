using Microsoft.JSInterop;
using System.Drawing;
using Util.Reflection.Expressions;

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
                if (value < DefaultMinZoom)
                    value = DefaultMinZoom;

                if (value > DefaultMaxZoom)
                    value = DefaultMaxZoom;

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

        private IJSObjectReference JsMap { get; set; }

        public static float DefaultMaxZoom { get; } = 19;

        public static float DefaultMinZoom { get; } = 3;

        private DotNetObjectReference<MBaiduMap> ObjRef { get; set; }

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

                if (val == await JsMap.InvokeAsync<float>("getZoom", null))
                    return;

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

            //Watcher.Watch<PointF>(nameof(Center), async (val) =>
            //{
            //    if (JsMap is null)
            //        return;

            //    await JsMap.InvokeVoidAsync("panTo", val.ToGeoPoint());
            //});
        }

        [JSInvokable]
        public void OnJsZoomEnd(object zoomJson)
        {
            Zoom = zoomJson.ToString().ToObject<float>();
        }

        public async Task PanTo(PointF point)
        {
            await JsMap.InvokeVoidAsync("panTo", point.ToGeoPoint());
            Center = point;
        }

    }
}
