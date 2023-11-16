namespace Masa.Blazor
{
    public partial class MBaiduMap : BDomComponentBase, IThemeable, IMap<BaiduOverlayBase>, IBaiduMapJsCallbacks, IAsyncDisposable
    {
        [Inject]
        public BaiduMapJSModule Module { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [MassApiParameter(360)]
        public StringNumber Width { get; set; } = 360;

        [Parameter]
        [MassApiParameter(240)]
        public StringNumber Height { get; set; } = 240;

        [Parameter]
        [MassApiParameter(10)]
        public float Zoom
        {
            get => GetValue<float>(10);
            set
            {
                if (value < MinZoom)
                    SetValue(MinZoom);

                else if (value > MaxZoom)
                    SetValue(MaxZoom);

                else
                    SetValue(value);
            }
        }

        [Parameter]
        [MassApiParameter(19)]
        public float MaxZoom
        {
            get => GetValue(DefaultMaxZoom);
            set
            {
                if (value >= MinZoom && value <= DefaultMaxZoom)
                    SetValue(value);
            }
        }

        [Parameter]
        [MassApiParameter(3)]
        public float MinZoom
        {
            get => GetValue(DefaultMinZoom);
            set
            {
                if (value >= DefaultMinZoom && value <= MaxZoom)
                    SetValue(value);
            }
        }

        [Parameter]
        public bool EnableScrollWheelZoom
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter("116.403, 39.917")]
        public GeoPoint Center
        {
            get => GetValue<GeoPoint>(new(116.403f, 39.917f));
            set => SetValue(value);
        }

        [Parameter]
        [MassApiParameter(BaiduMapType.Normal)]
        public BaiduMapType MapType
        {
            get => GetValue(BaiduMapType.Normal);
            set => SetValue(value);
        }

        [Parameter]
        public bool TrafficOn
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        [Parameter]
        public string? DarkThemeId { get; set; }

        [Parameter]
        public bool Dark
        {
            get => GetValue(false);
            set => SetValue(value);
        }

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

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnDoubleClick { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnRightClick { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnRightDoubleClick { get; set; }

        [Parameter]
        public EventCallback OnMapTypeChanged { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnMouseMove { get; set; }

        [Parameter]
        public EventCallback OnMouseOver { get; set; }

        [Parameter]
        public EventCallback OnMouseOut { get; set; }

        [Parameter]
        public EventCallback OnMoveStart { get; set; }

        [Parameter]
        public EventCallback OnMoving { get; set; }

        [Parameter]
        public EventCallback OnMoveEnd { get; set; }

        [Parameter]
        public EventCallback OnZoomStart { get; set; }

        [Parameter]
        public EventCallback OnZoomEnd { get; set; }

        [Parameter]
        public EventCallback OnAddOverlay { get; set; }

        [Parameter]
        public EventCallback OnAddControl { get; set; }

        [Parameter]
        public EventCallback OnRemoveOverlay { get; set; }

        [Parameter]
        public EventCallback OnRemoveControl { get; set; }

        [Parameter]
        public EventCallback OnClearOverlays { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnDragStart { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnDragging { get; set; }

        [Parameter]
        public EventCallback<BaiduMapEventArgs> OnDragEnd { get; set; }

        [Parameter]
        public EventCallback OnResize { get; set; }

        [Parameter]
        public EventCallback<float> ZoomChanged { get; set; }

        [Parameter]
        public EventCallback<GeoPoint> CenterChanged { get; set; }

        public async ValueTask HandleOnMapTypeChanged()
        {
            _maptypeChangedInJs = true;

            switch (await _baiduMap.TryInvokeAsync<string>("getMapType"))
            {
                case "B_NORMAL_MAP": MapType = BaiduMapType.Normal; break;
                case "B_EARTH_MAP": MapType = BaiduMapType.Earth; break;
                case "B_SATELLITE_MAP": MapType = BaiduMapType.Satellite; break;
            }

            await OnMapTypeChanged.InvokeAsync();
        }

        public async ValueTask HandleOnMoving()
        {
            _centerChangedInJs = true;

            if (CenterChanged.HasDelegate)
                await CenterChanged.InvokeAsync(await _baiduMap.TryInvokeAsync<GeoPoint>("getCenter"));

            await OnMoving.InvokeAsync();
        }

        public async ValueTask HandleOnZoomEnd()
        {
            _zoomChangedInJs = true;

            if (ZoomChanged.HasDelegate)
                await ZoomChanged.InvokeAsync(await _baiduMap.TryInvokeAsync<float>("getZoom"));

            await OnZoomEnd.InvokeAsync();
        }

        private IBaiduMapJSObjectReferenceProxy? _baiduMap;

        private bool _zoomChangedInJs = false;

        private bool _centerChangedInJs = false;

        private bool _maptypeChangedInJs = false;

        public static float DefaultMaxZoom { get; } = 19;

        public static float DefaultMinZoom { get; } = 3;

        private static Dictionary<BaiduMapType, string> BaiduMapTypeName { get; } = new()
        {
            { BaiduMapType.Normal, "B_NORMAL_MAP" },
            { BaiduMapType.Earth, "B_EARTH_MAP" },
            { BaiduMapType.Satellite, "B_SATELLITE_MAP" },
        };

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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            Id.ThrowIfNull(ComponentName);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _baiduMap = await Module.InitAsync(Id!, new BaiduMapInitOptions()
                {
                    EnableScrollWheelZoom = EnableScrollWheelZoom,
                    Zoom = Zoom,
                    Center = Center,
                    DarkThemeId = DarkThemeId,
                    Dark = Dark,
                    MaxZoom = MaxZoom,
                    MinZoom = MinZoom,
                    TrafficOn = TrafficOn,
                    MapTypeString = BaiduMapTypeName[MapType],
                }, this);

                StateHasChanged();
            }
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<float>(nameof(Zoom), async (val) =>
            {
                if (_zoomChangedInJs)
                {
                    _zoomChangedInJs = false;
                    return;
                }

                await _baiduMap.TryInvokeVoidAsync("setZoom", val);
            });

            watcher.Watch<float>(nameof(MaxZoom), async (val) =>
            {
                await _baiduMap.TryInvokeVoidAsync("setMaxZoom", val);

                if (Zoom > val)
                    Zoom = val;
            });

            watcher.Watch<float>(nameof(MinZoom), async (val) =>
            {
                await _baiduMap.TryInvokeVoidAsync("setMinZoom", val);

                if (Zoom < val)
                    Zoom = val;
            });

            watcher.Watch<bool>(nameof(EnableScrollWheelZoom), async (val) =>
                await _baiduMap.TryInvokeVoidAsync(val ? "enableScrollWheelZoom" : "disableScrollWheelZoom"));

            watcher.Watch<BaiduMapType>(nameof(MapType), async (val) =>
            {
                if (_maptypeChangedInJs)
                {
                    _maptypeChangedInJs = false;
                    return;
                }

                await _baiduMap.TryInvokeVoidAsync("setMapType", BaiduMapTypeName[val]);
            });

            watcher.Watch<bool>(nameof(TrafficOn), async (val) =>
                await _baiduMap.TryInvokeVoidAsync(val ? "setTrafficOn" : "setTrafficOff"));

            watcher.Watch<bool>(nameof(Dark), async (val) =>
                await _baiduMap.TryInvokeVoidAsync("setMapStyleV2", new { StyleId = val ? DarkThemeId : string.Empty }));

            watcher.Watch<GeoPoint>(nameof(Center), async (val) =>
            {
                if (_centerChangedInJs)
                {
                    _centerChangedInJs = false;
                    return;
                }

                await _baiduMap.TryInvokeVoidAsync("panTo", val);
            });
        }

        public async ValueTask AddOverlayAsync(BaiduOverlayBase overlay)
        {
            if (_baiduMap is null)
            {
                return;
            }
            
            await _baiduMap.AddOverlayAsync(overlay);
        }

        public async ValueTask RemoveOverlayAsync(BaiduOverlayBase overlay)
            => await _baiduMap.TryInvokeVoidAsync("removeOverlay", overlay.OverlayJSObjectRef);

        public async ValueTask ClearOverlaysAsync()
            => await _baiduMap.TryInvokeVoidAsync("clearOverlays");

        public async ValueTask<bool> ContainsOverlayAsync(BaiduOverlayBase overlay)
            => await _baiduMap.TryInvokeAsync<bool>("contains", overlay.OverlayJSObjectRef);

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                if (_baiduMap is not null)
                    await _baiduMap.DisposeAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}