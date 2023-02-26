using BlazorComponent.Abstracts;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars : BDomComponentBase
    {
        [Parameter]
        [ApiDefaultValue(nameof(SnackPosition.BottomCenter))]
        public SnackPosition Position { get; set; } = SnackPosition.BottomCenter;

        [Parameter]
        [ApiDefaultValue(DEFAULT_MAX_COUNT)]
        public int MaxCount { get; set; } = DEFAULT_MAX_COUNT;

        [Parameter]
        [ApiDefaultValue(576)]
        public StringNumber MaxWidth { get; set; } = 576;

        [Parameter]
        public int? Timeout { get; set; }

        [Parameter]
        public bool? Closeable { get; set; }

        private const int DEFAULT_MAX_COUNT = 5;

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

        internal void RemoveNoRender(Guid id)
        {
            var config = _stack.FirstOrDefault(c => c.Id == id);
            _stack.Remove(config);
        }
    }
}
