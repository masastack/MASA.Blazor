using Masa.Blazor.Mixins.Menuable;
using Masa.Blazor.Mixins.ScrollStrategy;

namespace Masa.Blazor
{
    public partial class MMenu : MMenuable, IDependent
    {
        [Inject] private OutsideClickJSModule OutsideClickJSModule { get; set; } = null!;

        [Inject] private ScrollStrategyJSModule ScrollStrategyJSModule { get; set; } = default!;

        [CascadingParameter] public IDependent? CascadingDependent { get; set; }

        [CascadingParameter(Name = "AppIsDark")]
        public bool AppIsDark { get; set; }

        [Parameter] public bool Auto { get; set; }

        [Parameter] [MasaApiParameter(true)] public bool CloseOnClick { get; set; } = true;

        [Parameter]
        [MasaApiParameter(true)]
        public bool CloseOnContentClick
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string? ContentStyle { get; set; }

        [Parameter] public bool DisableKeys { get; set; }

        [Parameter] [MasaApiParameter("auto")] public StringNumber MaxHeight { get; set; } = "auto";

        [Parameter] public EventCallback<WheelEventArgs> OnScroll { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter] public string? Origin { get; set; }

        [Parameter] public StringBoolean? Rounded { get; set; }

        [Parameter] public bool Tile { get; set; }

        [Parameter] public string? Transition { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedIn = "v1.8.0")]
        public ScrollStrategy ScrollStrategy { get; set; } = ScrollStrategy.Reposition;

        private static Block _block = new("m-menu");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private ModifierBuilder _contentModifierBuilder = _block.Element("content").CreateModifierBuilder();

        private readonly string _contentId = $"menu-{Guid.NewGuid():N}";
        private readonly List<IDependent> _dependents = new();

        private bool _isPopupEventsRegistered;
        private ScrollStrategyResult? _scrollStrategyResult;
        private DotNetObjectReference<MMenu>? _dotNetObjectReference;

        private StringNumber? CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.Content?.Width ?? 0, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth);

                return CalcXOverflow(CalcLeftAuto(), menuWidth);
            }
        }

        private string? CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        private string? CalculatedMaxWidth => MaxWidth?.ConvertToUnit();

        private string? CalculatedMinWidth
        {
            get
            {
                if (MinWidth != null)
                {
                    return MinWidth.ConvertToUnit();
                }

                var nudgeWidth = 0d;
                if (NudgeWidth != null)
                {
                    (_, nudgeWidth) = NudgeWidth.TryGetNumber();
                }

                var minWidth = Math.Min(
                    Dimensions.Activator.Width + nudgeWidth + (Auto ? 16 : 0),
                    Math.Max(PageWidth - 24, 0));

                double calculatedMaxWidth;
                if (NumberHelper.TryParseDouble(CalculatedMaxWidth, out var value))
                {
                    calculatedMaxWidth = value;
                }
                else
                {
                    calculatedMaxWidth = minWidth;
                }

                return ((StringNumber)Math.Min(calculatedMaxWidth, minWidth)).ConvertToUnit();
            }
        }

        private StringNumber? CalculatedTop => !Auto ? CalcTop() : CalcYOverflow(CalcTopAuto());

        private int DefaultOffset { get; set; } = 8;

        protected override string DefaultAttachSelector => Permanent ? ".m-application__permanent" : ".m-application";

        protected override bool IsRtl => MasaBlazor.RTL;

        public IEnumerable<string> DependentSelectors
        {
            get
            {
                var elements = _dependents
                    .SelectMany(dependent => dependent.DependentSelectors)
                    .ToList();

                elements.Add(ActivatorSelector);

                // do not use the ContentElement elementReference because it's delay assignment.
                elements.Add($"#{_contentId}");

                elements.AddRange(new[] { MSnackbar.ROOT_CSS_SELECTOR, PEnqueuedSnackbars.ROOT_CSS_SELECTOR });

                return elements.Distinct();
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Transition ??= "m-menu-transition";
            Origin ??= "top left";
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (CascadingDependent is not null)
                {
                    (this as IDependent).CascadingDependents.ForEach(item => item.AddDependent(this));
                }
            }
        }

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder.Add("attached", IsAttachSelf).Build();
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<bool>(nameof(CloseOnContentClick), () => ResetPopupEvents(CloseOnContentClick));
        }

        protected override bool ShouldUpdateActiveInJS => ActivatorContent is not null || CloseOnContentClick;

        public void AddDependent(IDependent dependent)
        {
            _dependents.Add(dependent);
            NextTickIf(
                () => { _ = OutsideClickJSModule.UpdateDependentElementsAsync(DependentSelectors.ToArray()); },
                () => !OutsideClickJSModule.Initialized);
        }

        //TODO:keydown event

        protected override async Task OnActiveUpdatingAsync(bool active)
        {
            await base.OnActiveUpdatingAsync(active);

            if (!_isPopupEventsRegistered && ContentElement.Context is not null)
            {
                _isPopupEventsRegistered = true;

                RegisterPopupEvents(ContentElement.GetSelector()!, CloseOnContentClick);

                if (ScrollStrategy != ScrollStrategy.None || (Absolute && ScrollStrategy != ScrollStrategy.Reposition))
                {
                    _dotNetObjectReference ??= DotNetObjectReference.Create(this);

                    _scrollStrategyResult = await ScrollStrategyJSModule.CreateScrollStrategy(Ref, ContentElement,
                        new ScrollStrategyOptions(ScrollStrategy.Reposition), _dotNetObjectReference);
                }
            }

            if (!OpenOnHover && CloseOnClick && OutsideClickJSModule is { Initialized: false })
            {
                await OutsideClickJSModule.InitializeAsync(this, DependentSelectors.ToArray());
            }
        }

        [JSInvokable("ScrollStrategy_OnScroll")]
        public async Task ScrollStrategy_OnScroll(string strategy)
        {
            switch (strategy)
            {
                case "close":
                    RunDirectly(false);
                    break;
                case "reposition":
                    await UpdateDimensionsAsync();
                    StateHasChanged();
                    break;
                default:
                    Logger.LogWarning("Unknown scroll strategy: {0}", strategy);
                    break;
            }
        }

        private Func<ValueTask<bool>>? CloseConditional { get; set; }

        private Func<Task>? Handler { get; set; }

        public override async Task HandleOnOutsideClickAsync()
        {
            if (IsActive && CloseOnClick)
            {
                await OnOutsideClick.InvokeAsync();
                RunDirectly(false);
            }
        }

        private double CalcTopAuto()
        {
            if (OffsetY)
            {
                return ComputedTop;
            }

            //TODO: check this
            //ignores some code about List

            return ComputedTop - 1;
        }

        private double CalcLeftAuto()
        {
            return Dimensions.Activator.Left - DefaultOffset * 2;
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            await OutsideClickJSModule.UnbindAndDisposeAsync();
            _scrollStrategyResult?.Unbind?.Invoke();
            _scrollStrategyResult?.Dispose?.Invoke();
            await base.DisposeAsyncCore();
        }
    }
}