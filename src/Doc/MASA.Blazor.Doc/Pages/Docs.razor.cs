using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Docs
    {
        private string _currentLanguage = "zh-CN";

        private string _previousPath;

        private string Path => $"{Category}/{FileName}.{_currentLanguage}";

        private DocFileModel File { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IPrismHighlighter PrismHighlighter { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        [Parameter]
        public string Category { get; set; }

        [Parameter]
        public string FileName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(FileName))
            {
                var menus = await Service.GetMenuAsync();
                var current = menus.FirstOrDefault(x => x.Url == Category);
                if (current == null) throw new Exception("No page matched.");

                NavigationManager.NavigateTo(current.Children[0].Url);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (string.IsNullOrWhiteSpace(FileName)) return;

            if (_previousPath == Path) return;

            File = await Service.GetDocFileAsync($"_content/MASA.Blazor.Doc/docs/{Path}.json");

            StateHasChanged();

            await PrismHighlighter.HighlightAllAsync();

            _previousPath = Path;
        }
    }
}