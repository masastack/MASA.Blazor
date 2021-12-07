using MASA.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        private ErrorBoundary _errorBoundary;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public DemoMenuItemModel[] MenuItems { get; set; } = { };

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            
            _errorBoundary?.Recover();
        }
    }
}