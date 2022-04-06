using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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