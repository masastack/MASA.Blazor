using BlazorComponent.Web;
using Masa.Blazor.Components.Rating;

namespace Masa.Blazor;

public partial class MRating : MasaComponentBase
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] public Document Document { get; set; } = null!;

    [Parameter]
    [MasaApiParameter("accent")]
    public string BackgroundColor { get; set; } = "accent";

    [Parameter]
    [MasaApiParameter("primary")]
    public string Color { get; set; } = "primary";

    [Parameter] public bool Clearable { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter]
    [MasaApiParameter("$ratingEmpty")]
    public string EmptyIcon { get; set; } = "$ratingEmpty";

    [Parameter]
    [MasaApiParameter("$ratingFull")]
    public string FullIcon { get; set; } = "$ratingFull";

    [Parameter]
    [MasaApiParameter("$ratingHalf")]
    public string HalfIcon { get; set; } = "$ratingHalf";

    [Parameter] public bool HalfIncrements { get; set; }

    [Parameter] public bool Hover { get; set; }

    [Parameter] public string? IconLabel { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public StringNumber? Size { get; set; }

    [Parameter]
    public double Value
    {
        get => _value;
        set
        {
            if (value == _value) return;
            _value = value;
        }
    }

    [Parameter] public EventCallback<double> ValueChanged { get; set; }

    [Parameter] public bool Small { get; set; }

    [Parameter] public bool XSmall { get; set; }

    [Parameter] public bool XLarge { get; set; }

    [Parameter] public bool Large { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment<RatingItemContext>? ItemContent { get; set; }

    [Parameter] [MasaApiParameter(5)] public StringNumber Length { get; set; } = 5;

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

    private bool _running;
    private double _value;
    private double _hoverIndex = -1;

    private CancellationTokenSource? _cancellationTokenSource;
    private Dictionary<int, RatingItem> _cachedItems = new();

    public bool IsHovering => Hover && _hoverIndex >= 0;

    private enum MouseType
    {
        MouseEnter,
        MouseLeave,
        MouseMove
    }

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

    private Block _block = new("m-rating");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(Readonly)
            .And(Dense)
            .AddTheme(IsDark, IndependentTheme)
            .GenerateCssClasses();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            foreach (var (k, v) in _cachedItems)
            {
                if (v.ForwardRef.Current.TryGetSelector(out var selector))
                {
                    _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mouseenter",
                        e => HandleOnExMouseEventAsync(e, k, MouseType.MouseEnter), false);
                    _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mouseleave",
                        e => HandleOnExMouseEventAsync(e, k, MouseType.MouseLeave), false);
                    _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mousemove",
                        e => HandleOnExMouseEventAsync(e, k, MouseType.MouseMove), false,
                        new EventListenerExtras(0, 16));
                }
            }
        }
    }

    public RatingItem CreateProps(int i)
    {
        if (!_cachedItems.TryGetValue(i, out var item))
        {
            item = new RatingItem(i, x => CreateClickFn(i, x));
            _cachedItems[i] = item;
        }

        item.Value = Value;
        item.IsFilled = Math.Floor(Value) > i;
        item.IsHovered = Math.Floor(_hoverIndex) > i;

        if (HalfIncrements)
        {
            item.IsHalfHovered = !item.IsHovered && ((_hoverIndex - i) % 1 > 0);
            item.IsHalfFilled = !item.IsFilled && ((Value - i) % 1 > 0);
        }

        return item;
    }

    public string GetIconName(RatingItem item)
    {
        var isFull = IsHovering ? item.IsHovered : item.IsFilled;
        var isHalf = IsHovering ? item.IsHalfHovered : item.IsHalfFilled;

        return isFull ? FullIcon : (isHalf != null && (bool)isHalf ? HalfIcon : EmptyIcon);
    }

    private string? GetColor(RatingItem props)
    {
        if (IsHovering)
        {
            if (props.IsHovered || (props.IsHalfHovered != null && (bool)props.IsHalfHovered))
                return Color;
        }
        else
        {
            if (props.IsFilled || (props.IsHalfFilled != null && (bool)props.IsHalfFilled))
                return Color;
        }

        return BackgroundColor;
    }

    private async Task CreateClickFn(int i, ExMouseEventArgs args)
    {
        if (Readonly)
        {
            return;
        }

        var newValue = await GenHoverIndex(i, args);
        Value = Clearable && Value == newValue ? 0 : newValue;
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task<double> GenHoverIndex(int i, ExMouseEventArgs args)
    {
        var isHalf = await IsHalfEvent(args);
        isHalf = HalfIncrements && MasaBlazor.RTL ? !isHalf : isHalf;

        return i + (isHalf ? 0.5 : 1);
    }

    private async Task<bool> IsHalfEvent(ExMouseEventArgs args)
    {
        if (HalfIncrements && args.Target != null)
        {
            var target = Document.GetElementByReference(args.Target.ElementReference);
            if (target == null) return false;

            var rect = await target.GetBoundingClientRectAsync();
            if (rect != null && (args.PageX - rect.Left) < rect.Width / 2)
                return true;
        }

        return false;
    }

    private async Task HandleOnExMouseEventAsync(ExMouseEventArgs args, int index, MouseType type)
    {
        if (!Hover)
        {
            return;
        }

        var taskCanceled = false;

        if (type is MouseType.MouseEnter or MouseType.MouseLeave)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(16, _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                taskCanceled = true;
            }
        }

        if (taskCanceled)
        {
            return;
        }

        var prevHoverIndex = _hoverIndex;

        switch (type)
        {
            case MouseType.MouseEnter:
                _hoverIndex = await GenHoverIndex(index, args);
                break;
            case MouseType.MouseLeave:
                _hoverIndex = -1;
                break;
            case MouseType.MouseMove:
                if (HalfIncrements)
                    _hoverIndex = await GenHoverIndex(index, args);
                break;
        }

        if (_hoverIndex != prevHoverIndex)
        {
            StateHasChanged();
        }
    }

    protected override ValueTask DisposeAsyncCore()
    {
        foreach (var (k, v) in _cachedItems)
        {
            if (v.ForwardRef.Current.TryGetSelector(out var selector))
            {
                _ = Js.RemoveHtmlElementEventListener(selector, "mouseenter");
                _ = Js.RemoveHtmlElementEventListener(selector, "mouseleave");
                _ = Js.RemoveHtmlElementEventListener(selector, "mousemove");
            }
        }

        return ValueTask.CompletedTask;
    }
}