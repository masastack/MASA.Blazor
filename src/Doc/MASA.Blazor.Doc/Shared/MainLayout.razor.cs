using MASA.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        //TODO:use i18n
        private bool _isEnglish;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsInterop { get; set; }

        [Parameter]
        public DemoMenuItemModel[] MenuItems { get; set; } = { };
    }
}