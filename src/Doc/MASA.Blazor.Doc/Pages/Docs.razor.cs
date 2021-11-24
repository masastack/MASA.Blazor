using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorComponent.Doc.Models;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Docs : IDisposable
    {
        [Parameter]
        public string FileName { get; set; }

        [Parameter]
        public string Dir { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        private DocFileModel _file;

        private bool _waitingHighlight = false;


        private string CurrentLanguage => LanguageService.CurrentCulture.Name;

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILanguageService LanguageService { get; set; }
        [Inject] private IPrismHighlighter PrismHighlighter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NavigationManager.LocationChanged += OnLocationChanged;

            if (string.IsNullOrWhiteSpace(FileName))
            {
                var currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                var newUrl = currentUrl.IndexOf('/') > 0 ? currentUrl.Substring(currentUrl.IndexOf('/') + 1) : currentUrl;
                var menus = await Service.GetMenuAsync();
                var current = menus.FirstOrDefault(x => x.Url == newUrl);
                if (current != null)
                {
                    NavigationManager.NavigateTo($"{CurrentLanguage}/{current.Children[0].Url}");
                }
            }
        }

        private async ValueTask SetDocUrl()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                _file = await Service.GetDocFileAsync($"_content/MASA.Blazor.Doc/docs/{(Dir == null ? "" : Dir + "/")}{FileName}.{CurrentLanguage}.json");
                _waitingHighlight = true;

                //await MainLayout.ChangePrevNextNav(FileName);

                //_filePath = $"docs/{FileName}.{CurrentLanguage}.md";
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await SetDocUrl();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_waitingHighlight)
            {
                _waitingHighlight = false;
                await PrismHighlighter.HighlightAllAsync();
            }
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
