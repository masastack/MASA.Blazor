using BlazorComponent.I18n;
using Masa.Blazor.Doc.Highlight;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Docs
    {
        private string _previousPath;

        private string Path => $"{Category}/{FileName}.{I18nConfig.Language ?? System.Globalization.CultureInfo.CurrentCulture.Name}";

        private DocFileModel File { get; set; }

        [CascadingParameter(Name = "Lang")]
        public bool IsChinese { get; set; }

        [Inject]
        public I18nConfig I18nConfig { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IPrismHighlighter PrismHighlighter { get; set; }

        [Parameter]
        public string Category { get; set; }

        [Parameter]
        public string FileName { get; set; }

        [Inject]
        public DemoService Service { get; set; }

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

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrWhiteSpace(FileName)) return;

            if (_previousPath == Path) return;
            _previousPath = Path;

            File = await Service.GetDocFileAsync($"_content/Masa.Blazor.Doc/docs/{Path}.json");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await PrismHighlighter.HighlightAllAsync();
        }
    }
}