using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MTimePickerClock : BTimePickerClock, ITimePickerClock
    {
        [Parameter]
        public int? Value
        {
            get
            {
                return GetValue<int?>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public int Rotate { get; set; }

        [Parameter]
        public bool Double { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

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

                return Themeable != null && Themeable.IsDark;
            }
        }

        [Parameter]
        public int Max { get; set; }

        [Parameter]
        public int Min { get; set; }

        [Parameter]
        public Func<int, bool> AllowedValues { get; set; }

        [Parameter]
        public EventCallback<int> OnInput { get; set; }

        [Parameter]
        public EventCallback<int> OnChange { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public int Step { get; set; } = 1;

        [Parameter]
        public Func<int, string> Format { get; set; }

        protected int DisplayedValue
        {
            get
            {
                return Value == null ? Min : Value.Value;
            }
        }

        protected override async Task HandleOnWheelAsync(WheelEventArgs args)
        {
            if (Readonly || Disabled)
            {
                return;
            }

            if (!Scrollable)
            {
                return;
            }

            var delta = Math.Sign(-args.DeltaY == 0 ? 1 : -args.DeltaY);
            var value = DisplayedValue;
            do
            {
                value = value + delta;
                value = (value - Min + Count) % Count + Min;
            } while (!IsAllowed(value) && value != DisplayedValue);

            if (value != DisplayedValue)
            {
                await UpdateAsync(value);
            }
        }

        protected int? ValueOnMouseDown { get; set; }

        protected int? ValueOnMouseUp { get; set; }

        protected bool IsDragging { get; set; }

        protected HtmlElement Clock => Document.QuerySelector(Ref);

        protected HtmlElement InnerClock => Document.QuerySelector(InnerClockElement);

        protected double InnerRadiusScale => 0.62;

        protected int Count => Max - Min + 1;

        protected double DegreesPerUnit => 360 / RoundCount;

        protected int RoundCount => Double ? (Count / 2) : Count;

        protected int? InputValue { get; set; }

        protected double Degrees => DegreesPerUnit * Math.PI / 180;

        protected override async Task HandleOnMouseDownAsync(MouseEventArgs arg)
        {
            if (Readonly || Disabled)
            {
                return;
            }

            ValueOnMouseDown = null;
            ValueOnMouseUp = null;
            IsDragging = true;

            await HandleOnDragMoveAsync(arg);
        }

        protected override async Task HandleOnDragMoveAsync(MouseEventArgs args)
        {
            if (Readonly || Disabled)
            {
                return;
            }

            if (!IsDragging)
            {
                return;
            }

            var clockRect = await Clock.GetBoundingClientRectAsync();
            var width = clockRect.Width;
            var top = clockRect.Top;
            var left = clockRect.Left;

            var innerClockRect = await InnerClock.GetBoundingClientRectAsync();
            var innerWidth = innerClockRect.Width;

            var clientX = args.ClientX;
            var clientY = args.ClientY;
            var center = (X: width / 2, Y: -width / 2);
            var coords = (X: clientX - left, Y: top - clientY);
            var handAngle = Math.Round(Angle(center, coords) - Rotate + 360) % 360;
            var insideClick = Double && Euclidean(center, coords) < (innerWidth + innerWidth * InnerRadiusScale) / 4;
            var checksCount = Math.Ceiling(15 / DegreesPerUnit);

            for (int i = 0; i < checksCount; i++)
            {
                var value = AngleToValue(handAngle + i * DegreesPerUnit, insideClick);
                if (IsAllowed(value))
                {
                    await SetMouseDownValueAsync(value);
                    return;
                }

                value = AngleToValue(handAngle - i * DegreesPerUnit, insideClick);
                if (IsAllowed(value))
                {
                    await SetMouseDownValueAsync(value);
                    return;
                }
            }
        }

        private async Task SetMouseDownValueAsync(int value)
        {
            if (ValueOnMouseDown == null)
            {
                ValueOnMouseDown = value;
            }

            ValueOnMouseUp = value;
            await UpdateAsync(value);
        }

        private async Task UpdateAsync(int value)
        {
            if (InputValue != value)
            {
                InputValue = value;
                if (OnInput.HasDelegate)
                {
                    await OnInput.InvokeAsync(value);
                }
            }
        }

        private bool IsAllowed(int value)
        {
            return AllowedValues == null || AllowedValues(value);
        }

        private int AngleToValue(double angel, bool insideClick)
        {
            var value = (Math.Round(angel / DegreesPerUnit) + (insideClick ? RoundCount : 0)) % Count + Min;

            if (angel < (360 - DegreesPerUnit / 2))
            {
                return Convert.ToInt32(value);
            }

            return Convert.ToInt32(insideClick ? Max - RoundCount + 1 : Min);
        }

        private double Angle((double X, double Y) center, (double X, double Y) p1)
        {
            var value = 2 * Math.Atan2(p1.Y - center.Y - Euclidean(center, p1), p1.X - center.X);
            return Math.Abs(value * 180 / Math.PI);
        }

        private double Euclidean((double X, double Y) p0, (double X, double Y) p1)
        {
            var dx = p1.X - p0.X;
            var dy = p1.Y - p0.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        protected override async Task HandleOnMouseUpAsync(MouseEventArgs arg)
        {
            if (Readonly || Disabled)
            {
                return;
            }

            IsDragging = false;
            if (ValueOnMouseUp != null && IsAllowed(ValueOnMouseUp.Value))
            {
                if (OnChange.HasDelegate)
                {
                    await OnChange.InvokeAsync(ValueOnMouseUp.Value);
                }
            }
        }

        protected override async Task HandleOnMouseLeaveAsync(MouseEventArgs arg)
        {
            if (Readonly || Disabled)
            {
                return;
            }

            if (IsDragging)
            {
                await HandleOnMouseUpAsync(arg);
            }
        }

        private bool IsInner(int? value)
        {
            return Double && (value - Min >= RoundCount);
        }

        private double HandScale(int value)
        {
            return IsInner(value) ? InnerRadiusScale : 1;
        }

        private (double, double) GetPosition(int value)
        {
            var rotateRadians = Rotate * Math.PI / 180;
            var x = Math.Sin((value - Min) * Degrees + rotateRadians) * HandScale(value);
            var y = -Math.Cos((value - Min) * Degrees + rotateRadians) * HandScale(value);
            return (x, y);
        }

        protected override void OnInitialized()
        {
            InputValue = Value;

            Watcher
                .Watch<int?>(nameof(Value), val =>
                {
                    InputValue = val;
                });
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-clock")
                        .AddIf("m-time-picker-clock--indeterminate", () => Value == null)
                        .AddTheme(IsDark);
                })
                .Apply("inner", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-clock__inner");
                })
                .Apply("hand", cssBuilder =>
                {
                    var color = Value != null ? Color ?? "accent" : "";
                    cssBuilder
                        .Add("m-time-picker-clock__hand")
                        .AddIf("m-time-picker-clock__hand--inner", () => IsInner(Value))
                        .AddBackgroundColor(color);
                }, styleBuilder =>
                {
                    var scale = $"scaleY({HandScale(DisplayedValue)})";
                    var angle = Rotate + DegreesPerUnit * (DisplayedValue - Min);
                    var color = Value != null ? Color ?? "accent" : "";
                    styleBuilder
                        .AddBackgroundColor(color)
                        .Add(() => $"transform: rotate({angle}deg) {scale}");
                })
                .Apply("item", cssBuilder =>
                {
                    var value = cssBuilder.Index;
                    var color = value == Value ? Color ?? "accent" : "";
                    cssBuilder
                        .Add("m-time-picker-clock__item")
                        .AddIf("m-time-picker-clock__item--active", () => value == DisplayedValue)
                        .AddIf("m-time-picker-clock__item--disabled", () => Disabled || !IsAllowed(value))
                        .AddBackgroundColor(color);
                }, styleBuilder =>
                {
                    var value = styleBuilder.Index;
                    var (x, y) = GetPosition(value);
                    var color = value == Value ? Color ?? "accent" : "";
                    styleBuilder
                        .Add(() => $"left:{50 + x * 50}%")
                        .Add(() => $"top:{50 + y * 50}%")
                        .AddBackgroundColor(color);
                });

            AbstractProvider
                .ApplyTimePickerClocDefault();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Scrollable)
                {
                    var el = Document.QuerySelector(Ref);
                    await el.AddEventListenerAsync("wheel", CreateEventCallback<WheelEventArgs>(HandleOnWheelAsync), false, new EventListenerActions() { PreventDefault = true });
                }
            }
        }
    }
}
