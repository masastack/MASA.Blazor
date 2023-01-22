using BlazorComponent.Components.Map;
using Masa.Blazor;
using System.Drawing;

namespace Masa.Blazor
{
    public partial class MBaiduMap : BDomComponentBase, IThemeable, IMap
    {
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
        public PointF Center { get; set; } = new(116.404f, 39.915f);

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

        [Inject]
        public BaiduMapJSModule? Module { get; set; }

        private bool jsHasInjected = false;
        private bool mapHasLoaded = false;

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

        /// <summary>
        /// Inject BaiduMap Javascript code and load map. Click
        /// <see href="https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-7.0">
        ///     here
        /// </see> for more details.
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // 1st render, inject BaiduMap Javascript code
            if (firstRender && !jsHasInjected)
            {
                // invalid servicekey
                if (string.IsNullOrWhiteSpace(ServiceKey))
                    throw new ArgumentNullException
                        (
                            nameof(ServiceKey),
                            $"{nameof(ServiceKey)} is required to enable map services"
                    );

                // js modules fail to be injected
                if (Module is null)
                    return;

                await Module.InjectBaiduMapScriptAsync(ServiceKey);

                jsHasInjected = true;
                StateHasChanged();
            }

            // 2nd render, load map
            if (!firstRender && !mapHasLoaded)
            {
                if (Module is null)
                    return;

                await Module.LoadMapAsync(Id, (CanZoom, Zoom, Center));

                mapHasLoaded = true;
                StateHasChanged();
            }
        }

    }
}
