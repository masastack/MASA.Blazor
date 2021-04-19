using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorComponent.Doc.CLI.Models;
using MASA.Blazor.Doc.Highlight;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Docs : ComponentBase, IDisposable
    {
        [Parameter] public string FileName { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        private DocFileModel _file;

        private bool _waitingHighlight = false;

        private string _filePath;
        private string EditUrl => $"https://github.com/ant-design-blazor/ant-design-blazor/edit/master/{_filePath}";

        private string CurrentLanguage => LanguageService.CurrentCulture.Name;

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILanguageService LanguageService { get; set; }
        [Inject] private DemoService DemoService { get; set; }
        [Inject] private IPrismHighlighter PrismHighlighter { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NavigationManager.LocationChanged += OnLocationChanged;

            if (string.IsNullOrWhiteSpace(FileName))
            {
                var currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                var newUrl = currentUrl.IndexOf('/') > 0 ? currentUrl.Substring(currentUrl.IndexOf('/') + 1) : currentUrl;
                var menus = await DemoService.GetMenuAsync();
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
                var baseUrl = NavigationManager.ToAbsoluteUri(NavigationManager.BaseUri);
                var docUrl = new Uri(baseUrl, $"_content/MASA.Blazor.Doc/docs/{FileName}.{CurrentLanguage}.json").ToString();
                _file = await HttpClient.GetFromJsonAsync<DocFileModel>(docUrl);
                _waitingHighlight = true;

                await MainLayout.ChangePrevNextNav(FileName);

                _filePath = $"docs/{FileName}.{CurrentLanguage}.md";
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await SetDocUrl();
            await base.OnParametersSetAsync();
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            await SetDocUrl();
            await InvokeAsync(StateHasChanged);
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
