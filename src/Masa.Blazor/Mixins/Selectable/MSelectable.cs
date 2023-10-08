namespace Masa.Blazor;

#if NET6_0
public partial class MSelectable<TValue> : MInput<TValue>, ISelectable<TValue>
#else
public partial class MSelectable<TValue> : MInput<TValue>, ISelectable<TValue> where TValue : notnull
#endif
{
    [Parameter]
    public bool? Ripple { get; set; }

    [Parameter]
    public TValue TrueValue { get; set; } = default!;

    [Parameter]
    public TValue FalseValue { get; set; } = default!;

    public Dictionary<string, object> InputAttrs => new();

    public override bool HasColor => IsActive;

    public override string ComputedColor => Color ?? (IsDark ? "white" : "primary");

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

        Validate(input);

        InternalValue = input;

        await TryInvokeFieldChangeOfInputsFilter();
    }

    public async Task HandleOnBlur(FocusEventArgs args)
    {
        IsFocused = false;

        await Task.CompletedTask;
    }

    public async Task HandleOnFocus(FocusEventArgs args)
    {
        IsFocused = true;

        await Task.CompletedTask;
    }

    public Task HandleOnKeyDown(KeyboardEventArgs args)
    {
        return Task.CompletedTask;
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        AbstractProvider
            .Merge(typeof(BLabel), typeof(MLabel), attrs =>
            {
                attrs[nameof(MLabel.Attributes)] = new Dictionary<string, object>()
                {
                    { "__internal_preventDefault_onclick", true }
                };
            });
    }
}
