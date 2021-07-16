using BlazorComponent;
using MASA.Blazor.Doc.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Pages
{
    public class ApiModel
    {
        public List<string> Components { get; set; }
        public List<Props> Props { get; set; }
        public List<Props1> Contents { get; set; }
        public List<Props1> Events { get; set; }
    }

    public class Props
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public string Description { get; set; }
    }

    public class Props1
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public partial class Api
    {
        [Parameter]
        public string ComponentName { get; set; }

        private List<TableHeaderOptions> _headers = new List<TableHeaderOptions>
        {
            new TableHeaderOptions
            {
                Text="Name",
                Width="20%"
            },new TableHeaderOptions
            {
                Text="Type",
                Width="15%"
            },new TableHeaderOptions
            {
                Text="Default",
                Width="12%"
            },
            "Description",
        };

        private List<TableHeaderOptions> _headers2 = new List<TableHeaderOptions>
        {
            new TableHeaderOptions
            {
                Text="Name",
                Width="20%"
            },
            "Description",
        };

        private ApiModel _api;

        private string CurrentLanguage => LanguageService.CurrentCulture.Name;

        [Inject] private ILanguageService LanguageService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(ComponentName))
            {
                var baseUrl = new Uri("http://127.0.0.1:5000");
                var apiUrl = new Uri(baseUrl, $"_content/MASA.Blazor.Doc/docs/api/{ComponentName}.{CurrentLanguage}.json").ToString();
                _api = await Service.GetApiAsync(apiUrl);
            }
        }
    }
}
