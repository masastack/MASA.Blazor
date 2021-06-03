using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class PageHeader
    {
        private bool _loading = false;

        private bool _showFilters = true;
        private bool ShowFilters
        {
            get
            {
                if (Filters == null)
                {
                    return false;
                }

                return _showFilters;
            }

            set
            {
                _showFilters = value;
            }
        }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment LeftActions { get; set; }

        [Parameter]
        public RenderFragment RightActions { get; set; }

        [Parameter]
        public RenderFragment Filters { get; set; }

        [Parameter]
        public EventCallback OnSearch { get; set; }

        private async Task HandleOnSearch()
        {
            if (OnSearch.HasDelegate)
            {
                _loading = true;

                await OnSearch.InvokeAsync();

                _loading = false;
            }
        }
    }
}
