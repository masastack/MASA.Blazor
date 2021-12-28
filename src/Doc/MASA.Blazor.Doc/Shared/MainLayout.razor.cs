using BlazorComponent;
using BlazorComponent.Web;
using MASA.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private ErrorBoundary _errorBoundary;
        private Window _window;
        private bool _isCanHideDrawerByLocationChanged;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public DemoMenuItemModel[] MenuItems { get; set; } = { };

        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _errorBoundary?.Recover();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _window = await JSRuntime.InvokeAsync<Window>(JsInteropConstants.GetWindow);

                if (_window != null)
                {
                    DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);

                    _isCanHideDrawerByLocationChanged = _window.InnerWidth <= 1264;
                    BaseLayout.UpdateNav(!_isCanHideDrawerByLocationChanged, _isCanHideDrawerByLocationChanged);
                }
            }
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (_isCanHideDrawerByLocationChanged)
                BaseLayout.UpdateNav(false);

            StateHasChanged();
        }

        private async void OnResize(Window window)
        {
            await Task.Run(() =>
            {
                _isCanHideDrawerByLocationChanged = window.InnerWidth <= 1264;
                BaseLayout.UpdateNav(!_isCanHideDrawerByLocationChanged, _isCanHideDrawerByLocationChanged);
            });
        }

        public void Dispose()
        {
            if (_window != null)
            {
                DomEventJsInterop.RemoveEventListener<Window>("window", "resize", OnResize);
            }

            NavigationManager.LocationChanged -= OnLocationChanged;

            GC.SuppressFinalize(this);
        }
    }
}