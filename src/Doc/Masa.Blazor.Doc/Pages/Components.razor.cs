using BlazorComponent.I18n;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Components
    {
        // TODO: i18n {zh-CN}
        private static string _githubUrlTemplate =
            "https://github.com/BlazorComponent/Masa.Blazor/blob/main/src/Doc/Masa.Blazor.Doc/Demos/Components";

        private DemoComponentModel _demoComponent;
        private int _demoIndex;

        private DemoItemModel Usage { get; set; }

        private List<DemoItemModel> PropsList { get; set; }

        private List<DemoItemModel> ContentsList { get; set; }

        private List<DemoItemModel> EventsList { get; set; }

        private List<DemoItemModel> MiscList { get; set; }

        private string GithubUrlHref { get; set; }

        [Parameter]
        public string Name { get; set; }

        [CascadingParameter(Name = "Lang")]
        public bool IsChinese { get; set; }

        [Inject]
        public I18nConfig I18nConfig { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public I18n I18n { get; set; }

        [Inject]
        public DemoService Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Service.InitializeDemos();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Name.Contains('?'))
            {
                Name = Name.Split("?")[0];
            }

            if (Name.Contains('#'))
            {
                Name = Name.Split("#")[0];
            }

            Service.ChangeLanguage(I18nConfig.Language ?? CultureInfo.CurrentCulture.Name);
            _demoComponent = await Service.GetComponentAsync(Name);

            var demos = _demoComponent.DemoList?
                .Where(x => !x.Debug && !x.Docs.HasValue)
                .OrderBy(x => x.Order)
                .ThenBy(r => r.Name) ?? Enumerable.Empty<DemoItemModel>();

            Usage = demos.FirstOrDefault(demo => demo.Group == DemoGroup.Usage);
            PropsList = demos.Where(demo => demo.Group == DemoGroup.Props).ToList();
            EventsList = demos.Where(demo => demo.Group == DemoGroup.Events).ToList();
            ContentsList = demos.Where(demo => demo.Group == DemoGroup.Contents).ToList();
            MiscList = demos.Where(demo => demo.Group == DemoGroup.Misc).ToList();

            GithubUrlHref = _githubUrlTemplate;
            _demoIndex = 0;
        }

        public string T(string key)
        {
            return I18n.T(key);
        }
    }
}