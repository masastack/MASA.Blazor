using System.Drawing;

namespace Masa.Blazor
{
    public partial class MBaiduMap : BDomComponentBase, IThemeable, IMap
    {
        [Inject]
        public BaiduMapJSModule Module { get; set; }

        [Parameter]
        [EditorRequired]
        public string ServiceKey { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 360;

        [Parameter]
        public StringNumber Height { get; set; } = 240;

        [Parameter]
        public byte Zoom { get; set; } = 10;

        [Parameter]
        public PointF MapCenter { get; set; } = new(116.404f, 39.915f);

        [Parameter]
        public bool CanZoom { get; set; } = true;

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

            // 1st render, inject BaiduMap Javascript code
            if (firstRender)
            {
                // invalid servicekey
                if (string.IsNullOrWhiteSpace(ServiceKey) || Module is null)
                    return;

                // 2nd render, load and init map
                NextTick(async () =>
                {
                    JsMap = await Module.LoadMapAsync(Id, new BaiduMapInitOption()
                    {
                        CanZoom = CanZoom,
                        Zoom = Zoom,
                        MapCenter = MapCenter,
                    });

                });

                await Module.InjectBaiduMapScriptAsync(ServiceKey);

                StateHasChanged();
            }
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher.Watch<byte>(nameof(Zoom), async (val) =>
            {
                if (val != Zoom)
                    await JsMap.InvokeVoidAsync("setZoom", val);
            });

            Watcher.Watch<bool>(nameof(CanZoom), async (val) =>
            {
                if (val == CanZoom)
                    return;

                if (val)
                    await JsMap.InvokeVoidAsync("enableScrollWheelZoom");
                else
                    await JsMap.InvokeVoidAsync("disableScrollWheelZoom");
            });

            Watcher.Watch<PointF>(nameof(MapCenter), async (val) =>
            {
                if (val == MapCenter)
                    return;

                await JsMap.InvokeVoidAsync("panTo", val.ToGeoPoint());
            });
        }

    }
}
