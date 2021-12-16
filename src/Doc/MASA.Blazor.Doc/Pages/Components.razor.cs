using BlazorComponent;
using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Shared;
using MASA.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Globalization;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Components
    {
        // TODO: i18n {zh-CN}
        private static string _githubUrlTemplate =
            "https://github.com/BlazorComponent/MASA.Blazor/blob/main/src/Doc/MASA.Blazor.Doc/Demos/Components/{0}/doc/index.zh-CN.md";

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

        [CascadingParameter]
        public bool IsChinese { get; set; }

        [Inject]
        public GlobalConfigs GlobalConfig { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

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

            Service.ChangeLanguage(GlobalConfig.Language ?? CultureInfo.CurrentCulture.Name);
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

            GithubUrlHref = string.Format(_githubUrlTemplate, _demoComponent.Type);

            _demoIndex = 0;
        }

        public async Task ScrollToAsync(string target)
        {
            var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, "#" + target);

            var options = new
            {
                Top = element.OffsetTop,
                Left = 0,
                Behavior = "smooth"
            };

            await Js.InvokeVoidAsync("window.scrollTo", options);
        }
    }
}