using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MRating : BRating, IRating
    {
        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public string EmptyIcon { get; set; } = "mdi-star-outline";

        [Parameter]
        public string FullIcon { get; set; } = "mdi-star";

        [Parameter]
        public string HalfIcon { get; set; } = "mdi-star-half-full";

        [Parameter]
        public bool HalfIncrements { get; set; }

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public string IconLabel { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public StringNumber Size { get; set; }

        private double _value = 0;

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


        private double _hoverIndex = -1;

        protected bool _isHovering => Hover && _hoverIndex >= 0;



        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        [Inject]
        public Document Document { get; set; }

        private bool _running;

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
                    attrs[nameof(MIcon.Icon)] = true;
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

                    attrs["onexclick"] = EventCallback.Factory.Create(this, ratingItem.Click);
                    attrs["onexmouseenter"] = EventCallback.Factory.Create<ExMouseEventArgs>(this,
                        async args => await HandleOnExMouseEventAsync(args, itemIndex, MouseType.MouseEnter));
                    attrs["onexmouseleave"] = EventCallback.Factory.Create<ExMouseEventArgs>(this,
                        async args => await HandleOnExMouseEventAsync(args, itemIndex, MouseType.MouseLeave));
                    attrs["onexmousemove"] = EventCallback.Factory.Create<ExMouseEventArgs>(this,
                        async args => await HandleOnExMouseEventAsync(args, itemIndex, MouseType.MouseMove));
                    attrs["return-target"] = true;
                });
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
            var isFull = _isHovering ? item.IsHovered : item.IsFilled;
            var isHalf = _isHovering ? item.IsHalfHovered : item.IsHalfFilled;

            return isFull ? FullIcon : (isHalf != null && (bool)isHalf ? HalfIcon : EmptyIcon);
        }

        private string GetColor(RatingItem props)
        {
            if (_isHovering)
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
                return;

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
            if (HalfIncrements)
            {
                var target = Document.GetElementByReference(args.Target.ElementReference);
                if (target != null)
                {
                    var rect = await target.GetBoundingClientRectAsync();
                    if (rect != null && (args.PageX - rect.Left) < rect.Width / 2)
                        return true;
                }
            }

            return false;
        }

        private async Task HandleOnExMouseEventAsync(ExMouseEventArgs args, int index, MouseType type)
        {
            if (_running)
                return;

            _running = true;
            if (Hover)
            {
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
            }

            await Task.Delay(10);
            _running = false;
        }
    }
}