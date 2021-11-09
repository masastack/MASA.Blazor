using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class PPageHeader
    {
        private bool _loading;

        private bool _showFilters = true;
        
        private bool ShowFilters
        {
            get => Filters != null && _showFilters;
            set => _showFilters = value;
        }
        
        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public RenderFragment Filters { get; set; }

        [Parameter]
        public RenderFragment LeftActions { get; set; }

        [Parameter]
        public EventCallback OnBack { get; set; }

        [Parameter]
        public EventCallback OnSearch { get; set; }

        [Parameter]
        public RenderFragment RightActions { get; set; }
        
        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public string Subtitle { get; set; }

        [Parameter]
        public RenderFragment SubtitleFragment { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment TitleFragment { get; set; }

        private async Task HandleOnBack()
        {
            if (OnBack.HasDelegate)
            {
                await OnBack.InvokeAsync();
            }
        }

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
