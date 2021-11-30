using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Pages
{
    public class ApiModel
    {
        public string Title { get; set; }
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

        private List<DataTableHeader<Props>> _headers = new List<DataTableHeader<Props>>
        {
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Name),
                Text = "Name",
                Width = "20%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Type),
                Text = "Type",
                Width = "15%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Default),
                Text = "Default",
                Width = "12%",
                Sortable = false
            },
            new DataTableHeader<Props>
            {
                Value = nameof(Props.Description),
                Text = "Description",
                Sortable = false
            }
        };

        private List<DataTableHeader<Props1>> _headers2 = new List<DataTableHeader<Props1>>
        {
            new DataTableHeader<Props1>
            {
                Value = nameof(Props1.Name),
                Text = "Name",
                Width = "20%",
                Sortable = false
            },
            new DataTableHeader<Props1>
            {
                Value = nameof(Props1.Description),
                Text = "Description",
                Sortable = false
            }
        };

        private ApiModel _api;

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(ComponentName))
            {
                var baseUrl = new Uri("http://127.0.0.1:5000");
                var apiUrl = new Uri(baseUrl, $"_content/MASA.Blazor.Doc/docs/api/{ComponentName}.zh-CN.json").ToString();
                _api = await Service.GetApiAsync(apiUrl);
            }
        }
    }
}