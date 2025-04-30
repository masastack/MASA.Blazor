using Masa.Blazor.Components.Pagination;

namespace Masa.Blazor;

public partial class MPagination
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public bool Circle { get; set; }

    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The format of the link href. It's useful for SEO.
    /// </summary>
    /// <example>
    /// ”/page/{0}“
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

    private static Block _block = new("m-pagination");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _navigationModifierBuilder = _block.Element("navigation").CreateModifierBuilder();
    private ModifierBuilder _itemModifierBuilder = _block.Element("item").CreateModifierBuilder();

    public bool PrevDisabled => Value <= 1;

    public bool NextDisabled => Value >= Length;

    protected int MaxButtons { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

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

    private IEnumerable<StringNumber> CreateRange(int length, int start = 0)
    {
        return Enumerable.Range(0, length).Select(i => (StringNumber)(start + i));
    }

    public IEnumerable<StringNumber> GetItems()
    {
        List<StringNumber> items = new();

        var start = 1;

        if (Length <= 0)
        {
            return items;
        }

        if (ComputedTotalVisible <= 1)
        {
            items.Add(Value);
            return items;
        }

        if (Length <= ComputedTotalVisible)
        {
            items.AddRange(CreateRange(Length, start));
            return items;
        }

        var even = ComputedTotalVisible % 2 == 0;
        var middle = even ? ComputedTotalVisible / 2d : Math.Floor(ComputedTotalVisible / 2d);
        var left = even ? middle : middle + 1;
        var right = Length - middle;

        if (left - Value >= 0)
        {
            items.AddRange(CreateRange(Math.Max(1, ComputedTotalVisible - 1), start));
            items.Add("...");
            items.Add(Length);
        }
        else if (Value - right >= (even ? 1 : 0))
        {
            var rangeLength = ComputedTotalVisible - 1;
            var rangeStart = Length - rangeLength + start;
            items.Add(start);
            items.Add("...");
            items.AddRange(CreateRange(rangeLength, rangeStart));
        }
        else
        {
            var rangeLength = Math.Max(1, ComputedTotalVisible - 3);
            var rangeStart = rangeLength == 1 ? Value : Value - Convert.ToInt32(Math.Ceiling(rangeLength / 2d)) + start;
            items.Add(start);
            items.Add("...");
            items.AddRange(CreateRange(rangeLength, rangeStart));
            items.Add("...");
            items.Add(Length);
        }

        return items;
    }

    public virtual async Task HandlePreviousAsync(MouseEventArgs args)
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

    public virtual async Task HandleNextAsync(MouseEventArgs args)
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

    public virtual async Task HandleItemClickAsync(StringNumber item)
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

    private async void MasaBlazor_WindowSizeChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (IsAuto is true)
        {
            var isMiniVaraint = IsMiniVariant();
            if (isMiniVaraint != _internalMiniVariant)
            {
                InvokeAsync(() => {
                    _internalMiniVariant = isMiniVaraint;
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
