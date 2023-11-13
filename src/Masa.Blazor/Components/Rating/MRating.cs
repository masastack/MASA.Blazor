using BlazorComponent.Web;

namespace Masa.Blazor
{
    public partial class MRating : BRating, IRating, IAsyncDisposable
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject]
        public Document Document { get; set; } = null!;

        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        [MassApiParameter("$ratingEmpty")]
        public string EmptyIcon { get; set; } = "$ratingEmpty";

        [Parameter]
        [MassApiParameter("$ratingFull")]
        public string FullIcon { get; set; } = "$ratingFull";

        [Parameter]
        [MassApiParameter("$ratingHalf")]
        public string HalfIcon { get; set; } = "$ratingHalf";

        [Parameter]
        public bool HalfIncrements { get; set; }

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public string? IconLabel { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public StringNumber? Size { get; set; }

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

        [Parameter]
        public EventCallback<double> ValueChanged { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool Large { get; set; }

        private bool _running;
        private double _value;
        private double _hoverIndex = -1;

        private CancellationTokenSource? _cancellationTokenSource;
        private Dictionary<int, ForwardRef> _iconForwardRefs = new();

        public bool IsHovering => Hover && _hoverIndex >= 0;

        private enum MouseType
        {
            MouseEnter,
            MouseLeave,
            MouseMove
        }

        protected override void SetComponentClass()
        {
            BackgroundColor ??= "accent";
            Color ??= "primary";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-rating")
                        .AddIf("m-rating--readonly", () => Readonly)
                        .AddIf("m-rating--dense", () => Dense)
                        .AddTheme(IsDark);
                });

            AbstractProvider
                .ApplyRatingDefault()
                .Apply<BIcon, MIcon>(attrs =>
                {
                    var itemIndex = attrs.Index;
                    var ratingItem = CreateProps(itemIndex);

                    attrs[nameof(MIcon.Size)] = Size;
                    attrs[nameof(MIcon.Small)] = Small;
                    attrs[nameof(MIcon.XLarge)] = XLarge;
                    attrs[nameof(MIcon.Large)] = Large;
                    attrs[nameof(MIcon.XSmall)] = XSmall;
                    attrs[nameof(MIcon.Dark)] = Dark;
                    attrs[nameof(MIcon.Light)] = Light;
                    attrs[nameof(MIcon.Color)] = GetColor(ratingItem);
                    attrs[nameof(MIcon.Tag)] = "button";
                    attrs["ripple"] = true;

                    if (IconLabel != null)
                    {
                        attrs["aria-label"] = string.Format(IconLabel, itemIndex, Length);
                    }

                    if (ratingItem.Click != null)
                    {
                        // TODO:(1.1.0) change ratingItem.Click type to MouseEventArgs!
                        attrs[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                        {
                            ExMouseEventArgs args = new()
                            {
                                Detail = e.Detail,
                                ScreenX = e.ScreenX,
                                ScreenY = e.ScreenY,
                                ClientX = e.ClientX,
                                ClientY = e.ClientY,
                                OffsetX = e.OffsetX,
                                OffsetY = e.OffsetY,
                                PageX = e.PageX,
                                PageY = e.PageY,
                                Button = e.Button,
                                Buttons = e.Buttons,
                                CtrlKey = e.CtrlKey,
                                ShiftKey = e.ShiftKey,
                                AltKey = e.AltKey,
                                MetaKey = e.MetaKey,
                                Type = e.Type,
                            };

                            ratingItem.Click.Invoke(args);
                        });
                    }

                    _iconForwardRefs.TryAdd(itemIndex, new ForwardRef());
                    attrs[nameof(MIcon.RefBack)] = _iconForwardRefs[itemIndex];
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                foreach (var (k, v) in _iconForwardRefs)
                {
                    if (v.Current.TryGetSelector(out var selector))
                    {
                        _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mouseenter",
                            e => HandleOnExMouseEventAsync(e, k, MouseType.MouseEnter), false);
                        _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mouseleave",
                            e => HandleOnExMouseEventAsync(e, k, MouseType.MouseLeave), false);
                        _ = Js.AddHtmlElementEventListener<ExMouseEventArgs>(selector, "mousemove",
                            e => HandleOnExMouseEventAsync(e, k, MouseType.MouseMove), false, new EventListenerExtras(0, 16));
                    }
                }
            }
        }

        public RatingItem CreateProps(int i)
        {
            var props = new RatingItem
            {
                Index = i,
                Value = Value,
                Click = async x => await CreateClickFn(i, x),
                IsFilled = Math.Floor(Value) > i,
                IsHovered = Math.Floor(_hoverIndex) > i
            };

            if (HalfIncrements)
            {
                props.IsHalfHovered = !props.IsHovered && ((_hoverIndex - i) % 1 > 0);
                props.IsHalfFilled = !props.IsFilled && ((Value - i) % 1 > 0);
            }

            return props;
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

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                foreach (var (k, v) in _iconForwardRefs)
                {
                    if (v.Current.TryGetSelector(out var selector))
                    {
                        _ = Js.RemoveHtmlElementEventListener(selector, "mouseenter");
                        _ = Js.RemoveHtmlElementEventListener(selector, "mouseleave");
                        _ = Js.RemoveHtmlElementEventListener(selector, "mousemove");
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
