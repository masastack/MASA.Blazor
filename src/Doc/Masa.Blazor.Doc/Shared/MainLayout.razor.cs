using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter]
        public BaseLayout BaseLayout { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}