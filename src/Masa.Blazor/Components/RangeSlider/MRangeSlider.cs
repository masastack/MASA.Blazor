namespace Masa.Blazor
{
    public class MRangeSlider<TValue> : MSliderBase<IList<TValue>, TValue>, IRangeSlider<TValue> where TValue : IComparable
    {
        public ElementReference SecondThumbElement { get; set; }

        protected override IList<TValue> DefaultValue => new List<TValue>() { default, default };

        protected IList<double> InputWidths
        {
            get { return DoubleInternalValues.Select(v => (RoundValue(v) - Min) / (Max - Min) * 100).ToList(); }
        }

        protected IList<double> DoubleInternalValues => InternalValue.Select(item => (double)(dynamic)item).ToList();

        protected override IList<TValue> LazyValue { get; set; } = new List<TValue>()
        {
            default,
            default
        };

        protected int? ActiveThumb { get; set; }

        protected override double DoubleInternalValue
        {
            get
            {
                if (ActiveThumb.HasValue)
                {
                    return (double)(dynamic)InternalValue[ActiveThumb.Value];
                }

                return default;
            }
            set
            {
                var val = RoundValue(Math.Min(Math.Max(value, Min), Max));

                if (ActiveThumb.HasValue)
                {
                    InternalValue[ActiveThumb.Value] = ConvertDoubleToTValue<TValue>(val);
                }
            }
        }

        protected override double GetRoundedValue(int index)
        {
            return RoundValue(DoubleInternalValues[index]);
        }

        public override async Task HandleOnSliderClickAsync(MouseEventArgs args)
        {
            if (!IsActive)
            {
                if (NoClick)
                {
                    NoClick = false;
                    return;
                }

                var value = await ParseMouseMoveAsync(args);

                await ReevaluateSelectedAsync(value);

                await SetInternalValueAsync(value);
            }
        }

        private double _value = 0;

        public override async Task HandleOnMouseMoveAsync(MouseEventArgs args)
        {
            _value = await ParseMouseMoveAsync(args);

            if (args.Type == "mousemove")
            {
                ThumbPressed = true;
            }

            ActiveThumb ??= GetIndexOfClosestValue(DoubleInternalValues, _value);

            await SetInternalValueAsync(_value);
        }

        protected override async Task SetInternalValueAsync(double value)
        {
            var values = new List<double>();

            for (var i = 0; i < DoubleInternalValues.Count; i++)
            {
                values.Add(i == ActiveThumb ? value : DoubleInternalValues[i]);
            }

            var val = values.Select(v => RoundValue(Math.Min(Math.Max(v, Min), Max))).ToList();

            if (val[0] > val[1] || val[1] < val[0])
            {
                if (ActiveThumb != null)
                {
                    var toFocusElement = ActiveThumb == 1 ? ThumbElement : SecondThumbElement;
                    await toFocusElement.FocusAsync();
                }

                val = new List<double> { val[1], val[0] };
            }

            InternalValue = val.Select(item => (TValue)Convert.ChangeType(item, typeof(TValue))).ToList();
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            if (ActiveThumb == null)
            {
                return;
            }

            var value = ParseKeyDown(args, DoubleInternalValues[ActiveThumb.Value]);
            if (value == null)
            {
                return;
            }

            await SetInternalValueAsync(value.AsT2);
        }

        public override async Task HandleOnTouchStartAsync(ExTouchEventArgs args)
        {
            var value = await ParseMouseMoveAsync(new MouseEventArgs() { ClientX = args.Touches[0].ClientX, ClientY = args.Touches[0].ClientY });
            await ReevaluateSelectedAsync(value);
            await base.HandleOnTouchStartAsync(args);
        }

        public override async Task HandleOnSliderMouseDownAsync(ExMouseEventArgs args)
        {
            var value = await ParseMouseMoveAsync(args);
            await ReevaluateSelectedAsync(value);
            await base.HandleOnSliderMouseDownAsync(args);
        }

        private async Task ReevaluateSelectedAsync(double value)
        {
            ActiveThumb = GetIndexOfClosestValue(DoubleInternalValues, value);
            var thumbElement = ActiveThumb == 0 ? ThumbElement : SecondThumbElement;
            await thumbElement.FocusAsync();
        }

        private int? GetIndexOfClosestValue(IList<double> values, double value)
        {
            if (Math.Abs(values[0] - value) < Math.Abs(values[1] - value))
            {
                return 0;
            }

            return 1;
        }

        public override Task HandleOnFocusAsync(FocusEventArgs args)
        {
            ActiveThumb = 0;
            _value = DoubleInternalValue;
            return base.HandleOnFocusAsync(args);
        }

        public override Task HandleOnBlurAsync(FocusEventArgs args)
        {
            _value = DoubleInternalValue;
            ActiveThumb = null;
            return base.HandleOnBlurAsync(args);
        }

        public async Task HandleOnSecondFocusAsync(FocusEventArgs args)
        {
            ActiveThumb = 1;
            _value = DoubleInternalValue;
            await base.HandleOnFocusAsync(args);
        }

        public async Task HandleOnSecondBlurAsync(FocusEventArgs args)
        {
            _value = DoubleInternalValue;
            ActiveThumb = null;
            await base.HandleOnBlurAsync(args);
        }

        protected override void OnValueChanged(IList<TValue> val)
        {
            val ??= new List<TValue>() { default, default };

            //Value may not between min and max
            //If that so,we should invoke ValueChanged 
            var roundedVal = val.Select(v => ConvertDoubleToTValue<TValue>(RoundValue(Math.Min(Math.Max(Convert.ToDouble(v), Min), Max)))).ToList();
            if (!ListComparer.Equals(roundedVal, InternalValue))
            {
                InternalValue = roundedVal;
            }

            LazyValue = roundedVal;
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--range-slider");
                })
                .Apply("range-track-background", cssBuilder =>
                {
                    var index = cssBuilder.Index;
                    cssBuilder
                        .AddIf("m-slider__track-background", () => index != 1 || IsDisabled)
                        .AddIf("m-slider__track-fill", () => index == 1 && !IsDisabled)
                        .AddBackgroundColor(index != 1 || IsDisabled ? ComputedTrackColor : "")
                        .AddBackgroundColor(index == 1 && !IsDisabled ? ComputedTrackFillColor : "");
                }, styleBuilder =>
                {
                    var index = styleBuilder.Index;
                    styleBuilder
                        .AddBackgroundColor(index != 1 || IsDisabled ? ComputedTrackColor : "")
                        .AddBackgroundColor(index == 1 && !IsDisabled ? ComputedTrackFillColor : "");

                    var padding = IsDisabled ? 10 : 0;
                    if (index == 0)
                    {
                        GetTrackStyle(styleBuilder, 0, InputWidths[0], 0, -padding);
                    }
                    else if (index == 1)
                    {
                        GetTrackStyle(styleBuilder, InputWidths[0], Math.Abs(InputWidths[1] - InputWidths[0]), padding, padding * -2);
                    }
                    else
                    {
                        GetTrackStyle(styleBuilder, InputWidths[1], Math.Abs(100 - InputWidths[1]), padding, -padding);
                    }
                });

            AbstractProvider
                .ApplyRangeSliderDefault<TValue>();
        }

        private void GetTrackStyle(StyleBuilder styleBuilder, double startLength, double endLength, double startPadding = 0, double endPadding = 0)
        {
            var startDir = Vertical ? (MasaBlazor.RTL ? "top" : "bottom") : (MasaBlazor.RTL ? "right" : "left");
            var endDir = Vertical ? "height" : "width";

            var start = $"calc({startLength}% + {startPadding}px)";
            var end = $"calc({endLength}% + {endPadding}px)";

            styleBuilder
                .Add($"transition:{TrackTransition}")
                .Add($"{startDir}:{start}")
                .Add($"{endDir}:{end}");
        }

        protected override bool IsThumbActive(int index)
        {
            return IsActive && ActiveThumb == index;
        }

        protected override bool IsThumbFocus(int index)
        {
            return IsFocused && ActiveThumb == index;
        }

        protected override void GetThumbContainerStyles(StyleBuilder styleBuilder)
        {
            var index = styleBuilder.Index;

            var direction = Vertical ? "top" : "left";
            var value = MasaBlazor.RTL ? 100 - InputWidths[index] : InputWidths[index];
            value = Vertical ? 100 - value : value;

            styleBuilder
                .AddTransition(TrackTransition)
                .Add($"{direction}:{value}%")
                .AddTextColor(ComputedThumbColor);
        }
    }
}
