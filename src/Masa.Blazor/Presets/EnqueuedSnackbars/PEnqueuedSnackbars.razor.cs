using System.ComponentModel;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars : MasaComponentBase
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        [MasaApiParameter(SnackPosition.BottomCenter)]
        public SnackPosition Position { get; set; } = DEFAULT_SNACK_POSITION;

        [Parameter]
        [MasaApiParameter(DEFAULT_MAX_COUNT)]
        public int MaxCount { get; set; } = DEFAULT_MAX_COUNT;

        [Parameter]
        [MasaApiParameter(DEFAULT_MAX_WIDTH)]
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

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }
        
        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }
        
        private const string ROOT_CSS = "m-enqueued-snackbars";
        private static Block _block = new(ROOT_CSS);
        internal const int DEFAULT_MAX_COUNT = 5;
        internal const SnackPosition DEFAULT_SNACK_POSITION = SnackPosition.BottomCenter;
        internal const int DEFAULT_MAX_WIDTH = 576;
        internal const string ROOT_CSS_SELECTOR = $".{ROOT_CSS}";

        private readonly List<SnackbarOptions> _stack = new();

        private bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        private bool IsPositionBottom => Position is SnackPosition.BottomCenter or SnackPosition.BottomLeft or SnackPosition.BottomRight;

        private bool IsPositionTop => Position is SnackPosition.TopCenter or SnackPosition.TopLeft or SnackPosition.TopRight;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.Application.PropertyChanged += ApplicationOnPropertyChanged;
        }

        private void ApplicationOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        protected override IEnumerable<string> BuildComponentClass()
        {
            if (Position is SnackPosition.TopLeft or SnackPosition.TopRight or SnackPosition.TopCenter)
            {
                yield return _block.Modifier("top");
            }
            
            if (Position is SnackPosition.BottomLeft or SnackPosition.BottomRight or SnackPosition.BottomCenter)
            {
                yield return _block.Modifier("bottom");
            }
            
            if (Position is SnackPosition.TopCenter or SnackPosition.BottomCenter or SnackPosition.Center)
            {
                yield return _block.Modifier("center");
            }
            
            if (Position is SnackPosition.TopLeft or SnackPosition.BottomLeft)
            {
                yield return _block.Modifier("left");
            }
            
            if (Position is SnackPosition.TopRight or SnackPosition.BottomRight)
            {
                yield return _block.Modifier("right");
            }
        }

        protected override IEnumerable<string?> BuildComponentStyle()
        {
            return StyleBuilder.Create()
                .AddMaxWidth(MaxWidth)
                .AddIf("bottom", $"{MasaBlazor.Application.Bottom}px", IsPositionBottom)
                .AddIf("top", $"{MasaBlazor.Application.Top}px", IsPositionTop).GenerateCssStyles();
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

        protected override ValueTask DisposeAsyncCore()
        {
            MasaBlazor.Application.PropertyChanged -= ApplicationOnPropertyChanged;
            return base.DisposeAsyncCore();
        }
    }
}
