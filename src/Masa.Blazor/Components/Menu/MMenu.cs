using BlazorComponent.Mixins;
using Masa.Blazor.Mixins.Menuable;

namespace Masa.Blazor
{
    public partial class MMenu : MMenuable, IDependent
    {
        [Inject] private OutsideClickJSModule? Module { get; set; }

        [CascadingParameter] public IDependent? CascadingDependent { get; set; }

        [CascadingParameter(Name = "AppIsDark")]
        public bool AppIsDark { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

        [Parameter] public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter] public string? Origin { get; set; }

        [Parameter] public StringBoolean? Rounded { get; set; }

        [Parameter] public bool Tile { get; set; }

        [Parameter] public string? Transition { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        private bool _isPopupEventsRegistered;

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

                if (CascadingIsDark)
                {
                    return true;
                }

                return AppIsDark;
            }
        }

        protected StringNumber? CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.Content?.Width ?? 0, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth);

                return CalcXOverflow(CalcLeftAuto(), menuWidth);
            }
        }

        protected string? CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        protected string? CalculatedMaxWidth => MaxWidth?.ConvertToUnit();

        protected string? CalculatedMinWidth
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

        protected StringNumber? CalculatedTop => !Auto ? CalcTop() : CalcYOverflow(CalcTopAuto());

        private readonly List<IDependent> _dependents = new();

        protected int DefaultOffset { get; set; } = 8;

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
                elements.Add($"#{Id}");

                elements.AddRange(new[] { MSnackbar.ROOT_CSS_SELECTOR, PEnqueuedSnackbars.ROOT_CSS_SELECTOR });

                return elements.Distinct();
            }
        }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Transition ??= "m-menu-transition";
            Origin ??= "top left";
#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (CascadingDependent is not null)
            {
                (this as IDependent).CascadingDependents.ForEach(item => item.RegisterChild(this));
            }
        }

        private Block _block = new("m-menu");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier("attached", IsAttachSelf).GenerateCssClasses();
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<bool>(nameof(CloseOnContentClick), () => ResetPopupEvents(CloseOnContentClick));
        }

        protected override void RunDirectly(bool val)
        {
            if (ActivatorContent is not null || CloseOnContentClick)
            {
                UpdateActiveInJS(val);
            }
            else
            {
                _ = SetActive(val);
            }
        }

        public void RegisterChild(IDependent dependent)
        {
            _dependents.Add(dependent);
            NextTickWhile(() => { Module?.UpdateDependentElementsAsync(DependentSelectors.ToArray()); },
                () => Module == null || Module.Initialized == false);
        }

        //TODO:keydown event

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            await base.WhenIsActiveUpdating(value);

            if (!_isPopupEventsRegistered && ContentElement.Context is not null)
            {
                _isPopupEventsRegistered = true;

                RegisterPopupEvents(ContentElement.GetSelector()!, CloseOnContentClick);
            }

            if (!OpenOnHover && CloseOnClick && Module is { Initialized: false })
            {
                await Module.InitializeAsync(this, DependentSelectors.ToArray());
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
            if (Module is not null)
            {
                try
                {
                    await Module.UnbindAndDisposeAsync();
                }
                catch (JSDisconnectedException)
                {
                    // ignore
                }
            }

            await base.DisposeAsyncCore();
        }
    }
}