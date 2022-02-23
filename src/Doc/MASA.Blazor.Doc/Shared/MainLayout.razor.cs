using BlazorComponent;
using BlazorComponent.Web;
using Masa.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        private ErrorBoundary _errorBoundary;

        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _errorBoundary?.Recover();
        }
    }
}