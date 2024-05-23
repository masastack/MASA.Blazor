namespace Masa.Blazor
{
#if NET6_0
    public partial class MRadio<TValue> : MasaComponentBase, IRadio<TValue>, IRippleState
#else
    public partial class MRadio<TValue> : MasaComponentBase, IRadio<TValue>, IRippleState where TValue : notnull
#endif
    {
        [CascadingParameter] public IRadioGroup<TValue>? RadioGroup { get; set; }

        [Parameter]
        [MasaApiParameter("$radioOn")]
        public string? OnIcon { get; set; } = "$radioOn";

        [Parameter]
        [MasaApiParameter("$radioOff")]
        public string? OffIcon { get; set; } = "$radioOff";

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool Readonly { get; set; }

        [Parameter] public RenderFragment? LabelContent { get; set; }

        [Parameter] public TValue Value { get; set; } = default!;

        [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter] public string? Label { get; set; }

        [Parameter] [MasaApiParameter(true)] public bool Ripple { get; set; } = true;

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        [CascadingParameter(Name = "Input_IsDisabled")]
        protected bool InputIsDisabled { get; set; }

        [Parameter] public string? Color { get; set; }

        protected bool IsFocused { get; set; }

        private bool HasState => RadioGroup?.HasState is true;
        
        private string? ComputedColor
        {
            get
            {
                if (IsDisabled || !IsActive)
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(Color))
                {
                    return Color;
                }

                if (IsDark)
                {
                    return "white";
                }

                return "primary";
            }
        }

        public string? ValidationState => RadioGroup?.ValidationState ?? ComputedColor;
        
        // TODO: 20240517 检查是否有性能影响
        // TODO: 20240517 没有的话 其他需要用GenRipple()的地方也改了
        private UseRippleState UseRippleState => new(this);

        protected override void OnInitialized()
        {
            base.OnInitialized();
            RadioGroup?.AddRadio(this);
        }

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

        private Block _block = new("m-radio");
        private Block _selectionBlock = new("m-input--selection-controls");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier("is-disabled", Disabled || InputIsDisabled)
                .And(IsFocused)
                .And(IndependentTheme)
                .GenerateCssClasses();
        }

        protected bool IsActive { get; private set; }

        protected bool IsDark
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

        public bool IsDisabled => Disabled || RadioGroup?.IsDisabled is true;

        protected bool IsReadonly => Readonly || RadioGroup?.IsReadonly is true;

        private async Task HandleClick(MouseEventArgs args)
        {
            if (IsDisabled || IsReadonly || IsActive)
            {
                return;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(new ChangeEventArgs { Value = Value });
            }

            if (RadioGroup is not null)
            {
                await RadioGroup.Toggle(Value);
            }
        }

        public void RefreshState()
        {
            if (RadioGroup is null) return;

            IsActive = EqualityComparer<TValue>.Default.Equals(RadioGroup.Value, Value);
            StateHasChanged();
        }
    }
}