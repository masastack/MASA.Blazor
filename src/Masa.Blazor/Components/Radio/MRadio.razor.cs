namespace Masa.Blazor
{
#if NET6_0
    public partial class MRadio<TValue> : IRadio<TValue>, IRippleState
#else
    public partial class MRadio<TValue> : IRadio<TValue>, IRippleState where TValue : notnull
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

        private string? RippleState => !IsDisabled && !string.IsNullOrWhiteSpace(ValidationState) ? ValidationState : null;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            RadioGroup?.AddRadio(this);
            Id ??= "input-" + Guid.NewGuid().ToString("N");
        }

        private static Block _block = new("m-radio");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private static Block _selectionControls = new Block("m-input--selection-controls");

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder.Add("is-disabled", Disabled || InputIsDisabled)
                .Add(IsFocused)
                .AddTheme(ComputedTheme)
                .Build();
        }

        protected bool IsActive { get; private set; }

        public bool IsDisabled => Disabled || RadioGroup?.IsDisabled is true;

        protected bool IsReadonly => Readonly || RadioGroup?.IsReadonly is true;

        private async Task HandleOnChange()
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