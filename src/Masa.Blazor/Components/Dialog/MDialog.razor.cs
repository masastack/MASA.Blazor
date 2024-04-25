using BlazorComponent.Mixins;

namespace Masa.Blazor
{
    public partial class MDialog : BBootable, IThemeable, IDependent
    {
        [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public string? ContentClass { get; set; }

        [Parameter] public string? ContentStyle { get; set; }

        [Parameter]
        [MasaApiParameter("center center")]
        public string Origin { get; set; } = "center center";

        [Parameter] public string? OverlayScrimClass { get; set; }

        /// <summary>
        /// The lazy content would be created in a [data-permanent] element.
        /// It's useful when you use this component in a layout.
        /// </summary>
        [Parameter]
        public bool Permanent { get; set; }

        [Parameter] public bool Scrollable { get; set; }

        [Parameter]
        [MasaApiParameter("dialog-transition")]
        public string? Transition { get; set; } = "dialog-transition";

        [Inject] private OutsideClickJSModule? OutsideClickJsModule { get; set; }

        [CascadingParameter] public IDependent? CascadingDependent { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        [Parameter] public string? Attach { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        // NEXT MAJOR: This parameter overlaps with the ChildContent parameter,
        // but it's not possible to simply set the context for ChildContent,
        // so we need to keep it for now util next major.
        [Parameter] public RenderFragment<DialogContentContext>? OutcomeContent { get; set; }

        [Parameter] public bool Fullscreen { get; set; }

        [Parameter] public bool HideOverlay { get; set; }

        [Parameter] public StringNumber? MaxWidth { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.5.0")]
        public bool NoPersistentAnimation { get; set; }

        [Parameter] public bool Persistent { get; set; }

        [Parameter] public StringNumber? Width { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [Parameter] public Dictionary<string, object?>? ContentAttributes { get; set; }

        private readonly List<IDependent> _dependents = new();

        private bool _attached;
        private DialogContentContext? _contentContext;

        public bool IsDark
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

        protected bool ShowOverlay => !Fullscreen && !HideOverlay;

        protected ElementReference? OverlayRef => Overlay?.Ref;

        protected int StackMinZIndex { get; set; } = 200;

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        protected MOverlay? Overlay { get; set; }

        protected int ZIndex { get; set; }

        protected bool Animated { get; set; }

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            if (ContentRef.Context is not null)
            {
                await AttachAsync(value);
            }
            else
            {
                NextTick(() => AttachAsync(value));
            }

            if (value)
            {
                ZIndex = await GetActiveZIndex(true);

                NextTick(async () =>
                {
                    // TODO: previousActiveElement

                    var contains = await JsInvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, ContentRef);
                    if (!contains)
                    {
                        await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
                    }
                });
            }

            await base.WhenIsActiveUpdating(value);
        }

        private async Task AttachAsync(bool value)
        {
            if (OutsideClickJsModule is { Initialized: false })
            {
                await OutsideClickJsModule.InitializeAsync(this, DependentSelectors.ToArray());
                await JsInvokeAsync(JsInteropConstants.AddElementTo, OverlayRef, AttachSelector);
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachSelector);

                _attached = true;
            }
        }

        private async Task<int> GetActiveZIndex(bool isActive)
        {
            return !isActive
                ? await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef)
                : await GetMaxZIndex() + 2;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex,
                new List<ElementReference> { ContentRef }, Ref);

            return maxZindex > StackMinZIndex ? maxZindex : StackMinZIndex;
        }

        public Task Keydown(KeyboardEventArgs args)
        {
            if (args.Key == "Escape")
            {
                Close();
            }

            return Task.CompletedTask;
        }

        private void Close()
        {
            if (Persistent)
            {
                if (NoPersistentAnimation)
                {
                    return;
                }

                Animated = true;
                StateHasChanged();
                NextTick(async () =>
                {
                    //This animated need 150ms
                    await Task.Delay(150);
                    Animated = false;
                    StateHasChanged();
                });
            }
            else
            {
                RunDirectly(false);
            }
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            if (!_attached)
            {
                return;
            }

            if (ContentRef.Context != null)
            {
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachSelector);
            }

            if (OverlayRef?.Context != null)
            {
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, OverlayRef, AttachSelector);
            }
        }

        public override async Task HandleOnOutsideClickAsync()
        {
            var maxZIndex = await GetMaxZIndex();

            // TODO: should ignore the click if e.target was dragged onto the overlay
            if (IsActive && ZIndex >= maxZIndex)
            {
                await OnOutsideClick.InvokeAsync();

                Close();
            }
        }

        public void RegisterChild(IDependent dependent)
        {
            _dependents.Add(dependent);

            NextTickWhile(() => { OutsideClickJsModule?.UpdateDependentElementsAsync(DependentSelectors.ToArray()); },
                () => OutsideClickJsModule == null || OutsideClickJsModule.Initialized == false);
        }

        public Dictionary<string, object?> ContentAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object?>();

                if (IsActive)
                {
                    attrs.Add("tabindex", 0);
                }

                ContentAttributes?.ForEach(x => attrs[x.Key] = x.Value);

                return attrs;
            }
        }

        protected string AttachSelector
            => Attach ?? (Permanent ? ".m-application__permanent" : ".m-application");

        public IEnumerable<string> DependentSelectors
        {
            get
            {
                var elements = _dependents.SelectMany(dependent => dependent.DependentSelectors).ToList();

                if (ContentRef.TryGetSelector(out var selector))
                {
                    elements.Add(selector);
                }

                elements.Add(MSnackbar.ROOT_CSS_SELECTOR);
                elements.Add(PEnqueuedSnackbars.ROOT_CSS_SELECTOR);

                return elements.Distinct();
            }
        }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _contentContext = new DialogContentContext(() => RunDirectly(false));
        }

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__container")
                        .AddIf($"{prefix}__container--attached", () => Attach != null);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}__content--active", () => IsActive)
                        .AddTheme(IsDark, IndependentTheme);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"z-index: {ZIndex}");
                })
                .Apply("innerContent", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)

                        // NEXT MAJOR: ContentClass should be added into "content" element, but due to its widespread usage
                        // and the potential for breaking changes, we keep it unchanged.
                        .Add(ContentClass)

                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--persistent", () => Persistent)
                        .AddIf($"{prefix}--fullscreen", () => Fullscreen)
                        .AddIf($"{prefix}--scrollable", () => Scrollable)
                        .AddIf($"{prefix}--animated", () => Animated);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"transform-origin: {Origin}")
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth)

                        // NEXT MAJOR: ContentClass should be added into "content" element, but due to its widespread usage
                        // and the potential for breaking changes, we keep it unchanged.
                        .Add(ContentStyle);
                });
        }
    }
}