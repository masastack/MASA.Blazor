﻿using Masa.Blazor.Components.Input;
using Masa.Blazor.Components.Slider;

namespace Masa.Blazor;

#if NET6_0
public partial class MRangeSlider<TValue> : MSliderBase<IList<TValue>, TValue>
#else
public partial class MRangeSlider<TValue> : MSliderBase<IList<TValue>, TValue> where TValue : struct, IComparable<TValue>
#endif
{
    private ElementReferenceWrapper SecondThumbElementWrapper = new();

    public ElementReference SecondThumbElement => SecondThumbElementWrapper.Value;

    protected override IList<TValue> DefaultValue => new List<TValue>() { default, default };

    protected IList<double> InputWidths
    {
        get { return DoubleInternalValues.Select(v => (RoundValue(v) - Min) / (Max - Min) * 100).ToList(); }
    }

    protected IList<double> DoubleInternalValues =>
        InternalValue?.Select(item => (double)(dynamic)item).ToList() ?? new List<double>();

    protected int? ActiveThumb { get; set; }

    protected override double DoubleInternalValue
    {
        get
        {
            if (InternalValue is { Count: 2 } && ActiveThumb.HasValue)
            {
                return (double)(dynamic)InternalValue[ActiveThumb.Value];
            }

            return default;
        }
        set
        {
            var val = RoundValue(Math.Min(Math.Max(value, Min), Max));

            if (InternalValue is { Count: 2 } && ActiveThumb.HasValue)
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

        if (val is not null)
        {
            
            Console.Out.WriteLine(string.Join(',', val));
        }

        UpdateInternalValue(val.Select(item => (TValue)Convert.ChangeType(item, typeof(TValue))).ToList(),
            InternalValueChangeType.InternalOperation);
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
        var value = await ParseMouseMoveAsync(new MouseEventArgs()
            { ClientX = args.Touches[0].ClientX, ClientY = args.Touches[0].ClientY });
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

    public override Task HandleOnFocusAsync(int index, FocusEventArgs args)
    {
        ActiveThumb = index;
        _value = DoubleInternalValue;
        return base.HandleOnFocusAsync(index,args);
    }

    public override Task HandleOnBlurAsync(int index, FocusEventArgs args)
    {
        _value = DoubleInternalValue;
        ActiveThumb = null;
        return base.HandleOnBlurAsync(index, args);
    }

    protected override IList<TValue> InternalValue
    {
        get => GetValue((IList<TValue>)new List<TValue>() { default, default });
        set => SetValue(value); // TODO: need to deep clone?
    }

    protected override void OnValueChanged(IList<TValue>? val)
    {
        val ??= new List<TValue>() { default, default };

        //Value may not between min and max
        //If that so,we should invoke ValueChanged 
        var roundedVal = val.Select(v =>
            ConvertDoubleToTValue<TValue>(RoundValue(Math.Min(Math.Max(Convert.ToDouble(v), Min), Max)))).ToList();
        if (InternalValue != null && !ListComparer.Equals(roundedVal, InternalValue))
        {
            InternalValue = roundedVal;
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new string[]
        {
            "m-input--range-slider"
        });
    }

    protected override bool IsThumbActive(int index)
    {
        return IsActive && ActiveThumb == index;
    }

    protected override bool IsThumbFocus(int index)
    {
        return IsFocused && ActiveThumb == index;
    }
}