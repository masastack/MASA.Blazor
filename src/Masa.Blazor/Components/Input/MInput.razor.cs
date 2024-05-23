using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MInput<TValue> : MasaComponentBase, IThemeable, IFilterInput
{
    [Inject] private I18n I18n { get; set; } = null!;

    [CascadingParameter] protected MInputsFilter? InputsFilter { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public string? TextColor { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public EventCallback<TValue> OnChange { get; set; }

    /// <summary>
    /// The required rule built-in.
    /// </summary>
    [Parameter]
    public bool Required
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The error message when the required rule is not satisfied.
    /// </summary>
    [Parameter, MasaApiParameter("$masaBlazor.required")]
    public string RequiredMessage
    {
        get => _requiredMessage ?? I18n.T("$masaBlazor.required");
        set => _requiredMessage = value;
    }

    [Parameter] public RenderFragment? AppendContent { get; set; }

    [Parameter] public virtual string? AppendIcon { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Label
    {
        get => GetValue<string?>();
        set => SetValue(value);
    }

    [Parameter] public virtual string? PrependIcon { get; set; }

    [Parameter] public RenderFragment? LabelContent { get; set; }

    [Parameter] public RenderFragment? PrependContent { get; set; }

    [Parameter] [MasaApiParameter(false)] public StringBoolean? HideDetails { get; set; } = false;

    [Parameter] public string? Hint { get; set; }

    [Parameter] public bool PersistentHint { get; set; }

    [Parameter] [MasaApiParameter(false)] public StringBoolean? Loading { get; set; } = false;

    [Parameter] public RenderFragment<string>? MessageContent { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public virtual EventCallback<MouseEventArgs> OnMouseDown { get; set; }

    [Parameter] public virtual EventCallback<MouseEventArgs> OnMouseUp { get; set; }

    public ElementReference InputSlotElement { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnPrependClick { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnAppendClick { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public virtual bool IsDark
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

    protected bool HasMouseDown { get; set; }

    protected virtual Dictionary<string, object> InputSlotAttrs { get; set; } = new();

    public virtual bool HasLabel => LabelContent != null || Label != null;

    public virtual bool HasDetails => MessagesToDisplay.Count > 0;

    public bool IsLoading => Loading != null && Loading != false;

    public virtual bool ShowDetails
        => HideDetails is null || HideDetails == false || (HideDetails == "auto" && HasDetails);

    public virtual bool HasHint => !HasError && !string.IsNullOrEmpty(Hint) && (PersistentHint || IsFocused);

    public virtual List<string> MessagesToDisplay
    {
        get
        {
            if (HasHint)
            {
                return new List<string> { Hint! };
            }

            if (!HasMessages)
            {
                return new List<string>();
            }

            return ValidationTarget.Take(ErrorCount).ToList();
        }
    }

    public virtual bool HasPrependClick => OnPrependClick.HasDelegate;

    public virtual bool HasAppendClick => OnAppendClick.HasDelegate;

    public virtual async Task HandleOnPrependClickAsync(MouseEventArgs args)
    {
        if (OnPrependClick.HasDelegate)
        {
            await OnPrependClick.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnAppendClickAsync(MouseEventArgs args)
    {
        if (OnAppendClick.HasDelegate)
        {
            await OnAppendClick.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnClickAsync(ExMouseEventArgs args)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnMouseDownAsync(MouseEventArgs args)
    {
        HasMouseDown = true;
        if (OnMouseDown.HasDelegate)
        {
            await OnMouseDown.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnMouseUpAsync(ExMouseEventArgs args)
    {
        HasMouseDown = false;
        if (OnMouseUp.HasDelegate)
        {
            await OnMouseUp.InvokeAsync(args);
        }
    }

    public void StateHasChangedForJsInvokable()
    {
        StateHasChanged();
    }

    #region built-in Required rule

    private static readonly Func<TValue, bool> s_defaultRequiredRule = v =>
    {
        if (v == null)
        {
            return false;
        }

        if (v is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        return true;
    };

    private string? _requiredMessage;

    #endregion

    public virtual string ComputedColor => IsDisabled ? "" : Color ?? (IsDark ? "white" : "primary");

    public virtual bool HasColor => false;

    public virtual string? ValidationState
    {
        get
        {
            if (IsDisabled)
            {
                return null;
            }

            if (HasError && ShouldValidate)
            {
                return "error";
            }

            if (HasSuccess)
            {
                return "success";
            }

            if (HasColor)
            {
                return ComputedColor;
            }

            return null;
        }
    }

    protected virtual bool IsDirty => Convert.ToString(InternalValue)?.Length > 0;

    protected IEnumerable<Func<TValue, StringBoolean>> InternalRules
    {
        get
        {
            if (Required)
            {
                var rules = new List<Func<TValue, StringBoolean>>()
                    { v => s_defaultRequiredRule(v) ? true : RequiredMessage };
                return Rules is null ? rules : rules.Concat(Rules);
            }

            return Rules ?? Enumerable.Empty<Func<TValue, StringBoolean>>();
        }
    }

    public virtual bool IsLabelActive => IsDirty;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    protected bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    private Block _block = new("m-input");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(HasState)
            .And("hide-details", !ShowDetails)
            .And(IsLabelActive)
            .And(IsDirty)
            .And(IsDisabled)
            .And(IsFocused)
            .And("is-loading", Loading != null && Loading != false)
            .And(IsReadonly)
            .And(Dense)
            .AddTheme(IsDark, IndependentTheme)
            .AddTextColor(ValidationState)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddTextColor(ValidationState).GenerateCssStyles();
    }

    private string? GetControlStyle()
    {
        var styleList = BuildControlStyle().ToArray();
        return styleList.Length > 0 ? string.Join("; ", styleList).Trim(' ') : null;
    }

    protected virtual IEnumerable<string> BuildControlStyle()
    {
        return Enumerable.Empty<string>();
    }

    protected async Task TryInvokeFieldChangeOfInputsFilter(bool isClear = false)
    {
        if (InputsFilter is null)
        {
            return;
        }

        await InputsFilter.FieldChange(ValueIdentifier.FieldName, isClear);
    }

    public void ResetFilter()
    {
        Reset();
    }
}