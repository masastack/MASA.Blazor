using Masa.Blazor.Components.Pagination;

namespace Masa.Blazor;

public partial class MPagination : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public bool Circle { get; set; }

    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The format of the link href. It's useful for SEO.
    /// </summary>
    /// <example>
    /// “/page/{0}“
    /// </example>
    [Parameter]
    public string? HrefFormat { get; set; }

    [Parameter] public int Value { get; set; }

    [Parameter] public EventCallback<int> ValueChanged { get; set; }

    [Parameter] public EventCallback<int> OnInput { get; set; }

    [Parameter] public EventCallback OnPrevious { get; set; }

    [Parameter] public EventCallback OnNext { get; set; }

    [Parameter] public int Length { get; set; }

    [Parameter] public string NextIcon { get; set; } = "$next";

    [Parameter] public string PrevIcon { get; set; } = "$prev";

    [Parameter] public StringNumber? TotalVisible { get; set; }

    [Parameter] public string? Color { get; set; } = "primary";

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    [Parameter, MasaApiParameter(false, "v1.7.0")]
    public bool MiniVariant
    {
        get => GetValue<bool>(false);
        set => SetValue(value);
    }

    [Parameter, MasaApiParameter(ReleasedOn = "v1.7.0")]
    public EventCallback<bool> MiniVariantChanged { get; set; }

    [Parameter, MasaApiParameter(600, "v1.7.0")]
    public OneOf<Breakpoints, double> MobileBreakpoint
    {
        get => GetValue<OneOf<Breakpoints, double>>(600);
        set => SetValue(value);
    }

    [Parameter, MasaApiParameter(2, ReleasedOn = "v1.8.0")]
    public int Elevation { get; set; } = 2;

    private bool _internalMiniVariant;

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

    private static Block _block = new("m-pagination");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _navigationModifierBuilder = _block.Element("navigation").CreateModifierBuilder();
    private ModifierBuilder _itemModifierBuilder = _block.Element("item").CreateModifierBuilder();

    private bool PrevDisabled => Value <= 1;

    private bool NextDisabled => Value >= Length;

    protected int MaxButtons { get; set; }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif

        if (IsAuto is false)
        {
            if (MiniVariant != _internalMiniVariant)
            {
                _internalMiniVariant = MiniVariant;
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var clientWidth =
                await Js.InvokeAsync<double>(JsInteropConstants.GetParentClientWidthOrWindowInnerWidth, Ref);
            if (clientWidth > 0)
            {
                CalcMaxButtons(clientWidth);
            }
            else
            {
                // clientWidth may be 0 when place in dialog,
                // so we need to observe the element
                await IntersectJSModule.ObserverAsync(Ref, async e =>
                {
                    if (e.IsIntersecting)
                    {
                        await InvokeAsync(async () =>
                        {
                            clientWidth = await Js.InvokeAsync<double>(
                                JsInteropConstants.GetParentClientWidthOrWindowInnerWidth,
                                Ref);
                            if (clientWidth > 0)
                            {
                                CalcMaxButtons(clientWidth);
                            }
                        });
                    }
                });
            }
        }

        void CalcMaxButtons(double width)
        {
            MaxButtons = Convert.ToInt32(Math.Floor((width - 96.0) / 42.0));
            StateHasChanged();
        }
    }

    private string GetIcon(PaginationIconType iconType)
    {
        return iconType == PaginationIconType.First
            ? (MasaBlazor.RTL ? NextIcon : PrevIcon)
            : (MasaBlazor.RTL ? PrevIcon : NextIcon);
    }

    private int ComputedTotalVisible
    {
        get
        {
            if (TotalVisible is null)
            {
                return MaxButtons > 0 ? MaxButtons : Length;
            }

            return TotalVisible.ToInt32();
        }
    }

    private IEnumerable<StringNumber> CreateRange(int from, int to)
    {
        var range = new List<StringNumber>();

        from = from > 0 ? from : 1;

        for (int i = from; i <= to; i++)
        {
            range.Add(i);
        }

        return range;
    }

    private IEnumerable<StringNumber> GetItems()
    {
        if (ComputedTotalVisible == 0)
        {
            return [];
        }

        // Get the maximum number of visible buttons
        var maxLength = Math.Min(ComputedTotalVisible, Length);
        if (MaxButtons != 0)
        {
            maxLength = Math.Min(maxLength, MaxButtons);
        }

        // If the total number of items is less than or equal to the maximum visible buttons,
        if (Length <= maxLength)
        {
            return CreateRange(1, Length);
        }

        List<StringNumber> items = new();

        var even = maxLength % 2 == 0 ? 1 : 0;
        var left = (int)Math.Floor((maxLength / 2m));
        var right = Length - left + 1 + even;

        if (Value > left && Value < right)
        {
            var firstItem = 1;
            var lastItem = Length;
            var start = Value - left + 2;
            var end = Value + left - 2 - even;
            StringNumber secondItem = (start - 1) == (firstItem + 1) ? 2 : "...";
            StringNumber beforeLastItem = (end + 1) == (lastItem - 1) ? (end + 1) : "...";

            items.Add(1);
            items.Add(secondItem);
            items.AddRange(CreateRange(start, end));
            items.Add(beforeLastItem);
            items.Add(Length);
        }
        else if (Value == left)
        {
            var end = Value + left - 1 - even;
            items.AddRange(CreateRange(1, end));
            items.Add("...");
            items.Add(Length);
        }
        else if (Value == right)
        {
            var start = Value - left + 1;
            items.Add(1);
            items.Add("...");
            items.AddRange(CreateRange(start, Length));
        }
        else
        {
            items.AddRange(CreateRange(1, left));
            items.Add("...");
            items.AddRange(CreateRange(right, Length));
        }

        return items;
    }

    private async Task HandlePreviousAsync(MouseEventArgs args)
    {
        if (PrevDisabled)
        {
            return;
        }

        Value--;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(Value);
        }

        if (OnPrevious.HasDelegate)
        {
            await OnPrevious.InvokeAsync();
        }
    }

    private async Task HandleNextAsync(MouseEventArgs args)
    {
        if (NextDisabled)
        {
            return;
        }

        Value++;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(Value);
        }

        if (OnNext.HasDelegate)
        {
            await OnNext.InvokeAsync();
        }
    }

    private async Task HandleItemClickAsync(StringNumber item)
    {
        Value = item.AsT1;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(item.AsT1);
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await IntersectJSModule.UnobserveAsync(Ref);
        MasaBlazor.WindowSizeChanged -= MasaBlazor_WindowSizeChanged;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (IsAuto)
        {
            MasaBlazor.WindowSizeChanged -= MasaBlazor_WindowSizeChanged;
            MasaBlazor.WindowSizeChanged += MasaBlazor_WindowSizeChanged;
        }
    }

    private void MasaBlazor_WindowSizeChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (IsAuto)
        {
            var isMiniVariant = IsMiniVariant();
            if (isMiniVariant != _internalMiniVariant)
            {
                InvokeAsync(() =>
                {
                    _internalMiniVariant = isMiniVariant;
                    _ = MiniVariantChanged.InvokeAsync(_internalMiniVariant);
                    StateHasChanged();
                });
            }
        }
    }

    private bool IsMiniVariant()
    {
        var (width, mobile, name, mobileBreakpoint) = MasaBlazor.Breakpoint;

        if (width == 0)
        {
            return false;
        }

        if (mobileBreakpoint.Equals(MobileBreakpoint))
        {
            return mobile;
        }

        if (MobileBreakpoint.IsT1)
        {
            return width <= MobileBreakpoint.AsT1;
        }

        return name <= MobileBreakpoint.AsT0;
    }

    private bool? isAuto;
    private bool IsAuto => isAuto ?? InitializeIsAuto();

    private bool InitializeIsAuto()
    {
        if (isAuto is null)
        {
            var hasDelegate = MiniVariantChanged.HasDelegate;
            var isDirty = IsDirtyParameter(nameof(MiniVariant));
            isAuto = (hasDelegate, isDirty) switch
            {
                (true, true) => true,
                (false, true) => false,
                (true, false) => true,
                (false, false) => true,
            };
        }

        return isAuto!.Value;
    }
}