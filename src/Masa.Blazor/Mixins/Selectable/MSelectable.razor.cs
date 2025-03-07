using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

#if NET6_0
public partial class MSelectable<TValue> : MInput<TValue>
#else
public partial class MSelectable<TValue> : MInput<TValue> where TValue : notnull
#endif
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool? Ripple { get; set; } = true;

    [Parameter] public TValue TrueValue { get; set; } = default!;

    [Parameter] public TValue FalseValue { get; set; } = default!;

    private bool _focusVisible;

    public override bool HasColor => IsActive;

    public override string? ComputedColor => Color;

    private bool IsCustomValue => IsDirtyParameter(nameof(TrueValue)) && IsDirtyParameter(nameof(FalseValue));

    public bool IsActive
    {
        get
        {
            var input = InternalValue;

            if (IsCustomValue)
                return EqualityComparer<TValue>.Default.Equals(input, TrueValue);

            if (input is bool value)
            {
                return value;
            }

            return false;
        }
    }

    protected override bool IsDirty => IsActive;

    public override async Task HandleOnClickAsync(ExMouseEventArgs args)
    {
        await HandleOnChange();

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    public async Task HandleOnChange()
    {
        if (!IsInteractive)
        {
            return;
        }

        var input = InternalValue;

        if (IsCustomValue)
        {
            input = EqualityComparer<TValue>.Default.Equals(input, TrueValue) ? FalseValue : TrueValue;
        }
        else if (input is bool val)
        {
            input = (TValue)(object)(!val);
        }

        Validate(input, force: true);

        UpdateInternalValue(input, InternalValueChangeType.InternalOperation);

        await TryInvokeFieldChangeOfInputsFilter();
    }

    public async Task HandleOnBlur(FocusEventArgs args)
    {
        IsFocused = false;
        _focusVisible = false;

        await Task.CompletedTask;
    }

    public async Task HandleOnFocus(FocusEventArgs args)
    {
        IsFocused = true;
        _focusVisible = await Js.InvokeAsync<bool>(JsInteropConstants.MatchesSelector, InputElement, ":focus-visible");
    }

    protected virtual Task HandleOnKeyDown(KeyboardEventArgs args)
    {
        return Task.CompletedTask;
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

    protected static Block ControlBlock => new("m-input--selection-controls");
    protected static BemIt.Element ControlWrapper => ControlBlock.Element("wrapper");

    private ModifierBuilder _controlInputModifierBuilder = ControlBlock.Element("input").CreateModifierBuilder();

    protected string ControlInputClasses => _controlInputModifierBuilder.Add(_focusVisible).Build();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat([ControlBlock.Name]);
    }
}