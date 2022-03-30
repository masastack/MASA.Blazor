using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets
{
    public partial class PPageHeader
    {
        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public RenderFragment<(Func<KeyboardEventArgs, Task> onEnter, Func<Task> onSearch)> Filters { get; set; }

        [Parameter]
        public RenderFragment LeftActions { get; set; }

        [Parameter]
        public EventCallback OnBack { get; set; }

        [Parameter]
        public EventCallback OnSearch { get; set; }

        [Parameter]
        public RenderFragment RightActions { get; set; }

        [Parameter]
        public bool ShowFiltersByDefault { get; set; }

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

        private bool _loading;

        private bool _showFilters;

        private bool ShowFilters
        {
            get => Filters != null && _showFilters;
            set => _showFilters = value;
        }

        protected override void OnInitialized()
        {
            _showFilters = ShowFiltersByDefault;
        }

        private async Task HandleOnBack()
        {
            if (OnBack.HasDelegate)
            {
                await OnBack.InvokeAsync();
            }
        }

        private async Task HandleOnEnter(KeyboardEventArgs args)
        {
            if (args.Code is "Enter" or "NumpadEnter")
            {
                await HandleOnSearchWithDelay();
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

        private async Task HandleOnSearchWithDelay()
        {
            // waiting value changed
            await Task.Delay(333);

            await OnSearch.InvokeAsync();
        }
    }
}