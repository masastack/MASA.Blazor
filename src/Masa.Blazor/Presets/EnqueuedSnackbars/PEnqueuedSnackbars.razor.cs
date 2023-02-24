using BlazorComponent.Abstracts;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars
    {
        [Parameter]
        public ToastPosition Position { get; set; } = ToastPosition.BottomRight;

        [Parameter]
        public int? Duration { get; set; } = 4000;

        [Parameter]
        public int MaxCount { get; set; }

        [Parameter]
        public EventCallback<ToastConfig> OnClose { get; set; }

        private readonly List<ToastConfig> _configs = new();

        private ComponentCssProvider CssProvider { get; } = new();

        protected override Task OnInitializedAsync()
        {
            SetComponentClass();

            return base.OnInitializedAsync();
        }

        private void SetComponentClass()
        {
            CssProvider.Apply((cssBuilder) => { cssBuilder.Add("m-toast-container"); }, (styleBuilder) =>
            {
                styleBuilder
                    .AddIf("top: 1rem; left: 1rem;", () => Position == ToastPosition.TopLeft)
                    .AddIf("top: 1rem; right: 1rem;", () => Position == ToastPosition.TopRight)
                    .AddIf("bottom: 1rem; left: 1rem;", () => Position == ToastPosition.BottomLeft)
                    .AddIf("bottom: 1rem; right: 1rem;", () => Position == ToastPosition.BottomRight);
            });
        }

        public async Task AddToast(ToastConfig config)
        {
            config.Duration ??= Duration ??= 4000;

            if (MaxCount > 0 && _configs.Count >= MaxCount)
            {
                var removeConfig = _configs[0];
                Remove(removeConfig);
            }

            _configs.Add(config);

            StateHasChanged();
        }

        internal void Remove(Guid id)
        {
            var config = _configs.FirstOrDefault(c => c.Id == id);
            Remove(config);
        }

        private void Remove(ToastConfig config)
        {
            // config.Visible = false;?
            // StateHasChanged();

            _configs.Remove(config);
        }

        internal void RemoveNoRender(Guid id)
        {
            var config = _configs.FirstOrDefault(c => c.Id == id);
            _configs.Remove(config);
        }
    }
}
