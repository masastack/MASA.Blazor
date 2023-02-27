using BlazorComponent.Abstracts;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars : BDomComponentBase
    {
        [Parameter]
        [ApiDefaultValue(nameof(SnackPosition.BottomCenter))]
        public SnackPosition Position { get; set; } = DEFAULT_SNACK_POSITION;

        [Parameter]
        [ApiDefaultValue(DEFAULT_MAX_COUNT)]
        public int MaxCount { get; set; } = DEFAULT_MAX_COUNT;

        [Parameter]
        [ApiDefaultValue(DEFAULT_MAX_WIDTH)]
        public StringNumber MaxWidth { get; set; } = DEFAULT_MAX_WIDTH;

        [Parameter]
        public int? Timeout { get; set; }

        [Parameter]
        public bool? Closeable { get; set; }

        internal const int DEFAULT_MAX_COUNT = 5;
        internal const SnackPosition DEFAULT_SNACK_POSITION = SnackPosition.BottomCenter;
        internal const int DEFAULT_MAX_WIDTH = 576;

        private readonly List<SnackbarOptions> _stack = new();

        protected override void SetComponentClass()
        {
            CssProvider.Apply((cssBuilder) =>
            {
                cssBuilder.Add("m-enqueued-snackbars")
                          .AddIf("m-enqueued-snackbars--top m-enqueued-snackbars--left", () => Position == SnackPosition.TopLeft)
                          .AddIf("m-enqueued-snackbars--top m-enqueued-snackbars--right", () => Position == SnackPosition.TopRight)
                          .AddIf("m-enqueued-snackbars--top m-enqueued-snackbars--center", () => Position == SnackPosition.TopCenter)
                          .AddIf("m-enqueued-snackbars--bottom m-enqueued-snackbars--left", () => Position == SnackPosition.BottomLeft)
                          .AddIf("m-enqueued-snackbars--bottom m-enqueued-snackbars--right", () => Position == SnackPosition.BottomRight)
                          .AddIf("m-enqueued-snackbars--bottom m-enqueued-snackbars--center", () => Position == SnackPosition.BottomCenter);
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

            StateHasChanged();
        }

        internal void RemoveSnackbar(Guid id)
        {
            var config = _stack.FirstOrDefault(c => c.Id == id);
            _stack.Remove(config);
            StateHasChanged();
        }
    }
}
