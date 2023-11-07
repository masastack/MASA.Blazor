using BlazorComponent.Abstracts;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars : BDomComponentBase
    {
        [Parameter]
        [MassApiParameter(SnackPosition.BottomCenter)]
        public SnackPosition Position { get; set; } = DEFAULT_SNACK_POSITION;

        [Parameter]
        [MassApiParameter(DEFAULT_MAX_COUNT)]
        public int MaxCount { get; set; } = DEFAULT_MAX_COUNT;

        [Parameter]
        [MassApiParameter(DEFAULT_MAX_WIDTH)]
        public StringNumber MaxWidth { get; set; } = DEFAULT_MAX_WIDTH;

        [Parameter]
        public int? Timeout { get; set; }

        [Parameter]
        public bool? Closeable { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public StringBoolean? Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Text { get; set; }

        private const string ROOT_CSS = "m-enqueued-snackbars";
        internal const int DEFAULT_MAX_COUNT = 5;
        internal const SnackPosition DEFAULT_SNACK_POSITION = SnackPosition.BottomCenter;
        internal const int DEFAULT_MAX_WIDTH = 576;
        internal const string ROOT_CSS_SELECTOR = $".{ROOT_CSS}";

        private readonly List<SnackbarOptions> _stack = new();

        protected override void SetComponentClass()
        {
            CssProvider.Apply((cssBuilder) =>
            {
                cssBuilder.Add(ROOT_CSS)
                          .AddIf($"{ROOT_CSS}--top {ROOT_CSS}--left", () => Position == SnackPosition.TopLeft)
                          .AddIf($"{ROOT_CSS}--top {ROOT_CSS}--right", () => Position == SnackPosition.TopRight)
                          .AddIf($"{ROOT_CSS}--top {ROOT_CSS}--center", () => Position == SnackPosition.TopCenter)
                          .AddIf($"{ROOT_CSS}--bottom {ROOT_CSS}--left", () => Position == SnackPosition.BottomLeft)
                          .AddIf($"{ROOT_CSS}--bottom {ROOT_CSS}--right", () => Position == SnackPosition.BottomRight)
                          .AddIf($"{ROOT_CSS}--bottom {ROOT_CSS}--center", () => Position == SnackPosition.BottomCenter)
                          .AddIf($"{ROOT_CSS}--center", () => Position == SnackPosition.Center);
            }, styleBuilder => { styleBuilder.AddMaxWidth(MaxWidth); });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MaxCount < 1)
            {
                MaxCount = DEFAULT_MAX_COUNT;
            }
        }

        public void EnqueueSnackbar(SnackbarOptions config)
        {
            if (MaxCount > 0 && _stack.Count >= MaxCount)
            {
                var diff = _stack.Count - MaxCount + 1;

                _stack.RemoveRange(0, diff);
            }

            _stack.Add(config);

            InvokeAsync(StateHasChanged);
        }

        internal void RemoveSnackbar(Guid id)
        {
            var config = _stack.FirstOrDefault(c => c.Id == id);
            if (config is null) return;

            _stack.Remove(config);

            InvokeAsync(StateHasChanged);
        }
    }
}
