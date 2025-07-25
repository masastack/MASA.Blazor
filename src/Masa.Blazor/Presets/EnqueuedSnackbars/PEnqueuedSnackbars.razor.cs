using System.ComponentModel;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor.Presets
{
    public partial class PEnqueuedSnackbars
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

        /// <summary>
        /// The duration in milliseconds for which duplicate messages will be filtered.
        /// Filter by message fingerprint, which is combined from the title and content of the message.
        /// </summary>
        [Parameter]
        [MasaApiParameter(1000, ReleasedIn = "v1.10.0")]
        public int DuplicateMessageFilterDuration { get; set; } = 1000;

        private const string ROOT_CSS = "m-enqueued-snackbars";
        private static Block _block = new(ROOT_CSS);
        internal const int DEFAULT_MAX_COUNT = 5;
        internal const SnackPosition DEFAULT_SNACK_POSITION = SnackPosition.BottomCenter;
        internal const int DEFAULT_MAX_WIDTH = 576;
        internal const string ROOT_CSS_SELECTOR = $".{ROOT_CSS}";

        private readonly List<SnackbarOptions> _stack = new();

        private readonly SemaphoreSlim _semaphore = new(1, 1);

        // Use for tracking recently displayed messages to filter duplicates
        private readonly Dictionary<string, DateTime> _recentMessages = new();

        private bool IsPositionBottom =>
            Position is SnackPosition.BottomCenter or SnackPosition.BottomLeft or SnackPosition.BottomRight;

        private bool IsPositionTop =>
            Position is SnackPosition.TopCenter or SnackPosition.TopLeft or SnackPosition.TopRight;

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
            yield return _block.Name;

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
                .AddIf("bottom", $"{MasaBlazor.Application.Bottom + MasaBlazor.Application.Footer}px", IsPositionBottom)
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

        public async Task EnqueueSnackbar(SnackbarOptions config)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (IsDuplicateMessage(config))
                {
                    return;
                }

                if (MaxCount > 0 && _stack.Count >= MaxCount)
                {
                    var diff = _stack.Count - MaxCount + 1;

                    _stack.RemoveRange(0, diff);
                }

                _stack.Add(config);

                await InvokeAsync(StateHasChanged);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private bool IsDuplicateMessage(SnackbarOptions config)
        {
            // If the filter duration is set to 0, do not perform duplicate message filtering
            if (DuplicateMessageFilterDuration <= 0) return false;

            // Get message fingerprint using SnackbarOptions.MessageFingerprint property
            var messageFingerprint = config.MessageFingerprint;

            // Check if the same message has been shown in the timeout window
            if (_recentMessages.TryGetValue(messageFingerprint, out var lastShownTime))
            {
                // Skip if the message is within the filter duration window
                if ((DateTime.UtcNow - lastShownTime).TotalMilliseconds < DuplicateMessageFilterDuration)
                {
                    return true;
                }
            }

            // Only clean up expired records when the dictionary becomes large (e.g., over 100 records)
            if (_recentMessages.Count > 100)
            {
                var keysToRemove = _recentMessages
                    .Where(kv => (DateTime.UtcNow - kv.Value).TotalMilliseconds >= DuplicateMessageFilterDuration)
                    .Select(kv => kv.Key)
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    _recentMessages.Remove(key);
                }
            }

            // Update or add timestamp for the current message
            _recentMessages[messageFingerprint] = DateTime.UtcNow;

            // We've already checked time-based filtering above, and we're outside the filter window,
            // so we should allow the message to be displayed even if it's already in the stack
            return false;
        }

        internal async Task RemoveSnackbar(Guid id)
        {
            await _semaphore.WaitAsync();
            try
            {
                var config = _stack.FirstOrDefault(c => c.Id == id);
                if (config is null) return;

                _stack.Remove(config);

                await InvokeAsync(StateHasChanged);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        protected override ValueTask DisposeAsyncCore()
        {
            MasaBlazor.Application.PropertyChanged -= ApplicationOnPropertyChanged;
            return base.DisposeAsyncCore();
        }
    }
}