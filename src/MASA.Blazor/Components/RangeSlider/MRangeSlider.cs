using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MRangeSlider<TValue> : MSlider<IList<TValue>>, IRangeSlider<TValue>
    {
        [Parameter]
        public override IList<TValue> Value
        {
            get
            {
                return GetValue<IList<TValue>>(new List<TValue> { default, default });
            }
            set
            {
                SetValue(value);
            }
        }

        public ElementReference SecondThumbElement { get; set; }

        protected IList<double> InputWidths
        {
            get
            {
                return DoubleInteralValues.Select(v => (RoundValue(v) - Min) / (Max - Min) * 100).ToList();
            }
        }

        protected IList<double> DoubleInteralValues
        {
            get
            {
                return InternalValue is IList<double> val ? val : default;
            }
            set
            {
                var val = value.Select(v => RoundValue(Math.Min(Math.Max(v, Min), Max))).ToList();

                if (val[0] > val[1] || val[1] < val[0])
                {
                    if (ActiveThumb != null)
                    {
                        var toFocusElement = ActiveThumb == 1 ? ThumbElement : SecondThumbElement;
                        toFocusElement.FocusAsync();
                    }

                    val = new List<double> { val[1], val[0] };
                }

                InternalValue = val is IList<TValue> internalVal ? internalVal : default;
            }
        }

        protected int? ActiveThumb { get; set; }

        protected override double GetRoundedValue(int index)
        {
            return RoundValue(DoubleInteralValues[index]);
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
                SetInternalValue(value);

                if (OnChange.HasDelegate)
                {
                    await OnChange.InvokeAsync(InternalValue);
                }
            }
        }

        public override async Task HandleOnMouseMoveAsync(MouseEventArgs args)
        {
            var value = await ParseMouseMoveAsync(args);

            if (args.Type == "mousemove")
            {
                ThumbPressed = true;
            }

            ActiveThumb = GetIndexOfClosestValue(DoubleInteralValues, value);
            SetInternalValue(value);
        }

        private void SetInternalValue(double value)
        {
            var values = new List<double>();

            for (int i = 0; i < DoubleInteralValues.Count; i++)
            {
                if (i == ActiveThumb)
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(DoubleInteralValues[i]);
                }
            }

            DoubleInteralValues = values;
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            if (ActiveThumb == null)
            {
                return;
            }

            var value = ParseKeyDown(args, DoubleInteralValues[ActiveThumb.Value]);
            if (value == null)
            {
                return;
            }

            SetInternalValue(value.AsT2);
            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(InternalValue);
            }
        }

        public override async Task HandleOnSliderMouseDownAsync(ExMouseEventArgs args)
        {
            var value = await ParseMouseMoveAsync(args);
            await ReevaluateSelectedAsync(value);
            await base.HandleOnSliderMouseDownAsync(args);
        }

        private async Task ReevaluateSelectedAsync(double value)
        {
            ActiveThumb = GetIndexOfClosestValue(DoubleInteralValues, value);
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
            return base.HandleOnFocusAsync(args);
        }

        public override Task HandleOnBlurAsync(FocusEventArgs args)
        {
            ActiveThumb = null;
            return base.HandleOnBlurAsync(args);
        }

        public async Task HandleOnSecondFocusAsync(FocusEventArgs args)
        {
            ActiveThumb = 1;
            await base.HandleOnFocusAsync(args);
        }

        public async Task HandleOnSecondBlurAsync(FocusEventArgs args)
        {
            ActiveThumb = null;
            await base.HandleOnBlurAsync(args);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            InternalValue = Value;
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
            var startDir = Vertical ? (GlobalConfig.RTL ? "top" : "bottom") : (GlobalConfig.RTL ? "right" : "left");
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
            var value = GlobalConfig.RTL ? 100 - InputWidths[index] : InputWidths[index];
            value = Vertical ? 100 - value : value;

            styleBuilder
                .AddTransition(TrackTransition)
                .Add($"{direction}:{value}%")
                .AddTextColor(ComputedThumbColor);
        }
    }
}
