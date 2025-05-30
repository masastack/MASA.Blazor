using Masa.Blazor.Utils;

namespace Masa.Blazor.Components.Slider;

#if NET6_0
public class SliderInteropHandle<TValue, TNumeric>
#else
public class SliderInteropHandle<TValue, TNumeric> where TNumeric : struct, IComparable<TNumeric>
#endif
{
    private readonly ThrottleTask _throttleTask = new(16);
    private readonly MSliderBase<TValue, TNumeric> _slider;

    public SliderInteropHandle(MSliderBase<TValue, TNumeric> slider)
    {
        _slider = slider;
    }

    [JSInvokable]
    public Task OnMouseDownInternal(SliderMouseEventArgs args)
        => _slider.HandleOnSliderMouseDownAsync(args);

    [JSInvokable]
    public Task OnTouchStartInternal(SliderTouchEventArgs args)
        => _slider.HandleOnTouchStartAsync(args);

    [JSInvokable]
    public Task OnMouseUpInternal(SliderEventArgs args)
        => _slider.HandleOnSliderEndSwiping(args);

    [JSInvokable]
    public Task OnMouseMoveInternal(SliderEventArgs args)
        => _throttleTask.RunAsync(() => _slider.HandleOnMouseMoveAsync(args.MouseEventArgs, args.TrackRect));
}

public record SliderEventArgs(MouseEventArgs MouseEventArgs, BoundingClientRect? TrackRect);

public record SliderMouseEventArgs(ExMouseEventArgs MouseEventArgs, BoundingClientRect? TrackRect);

public record SliderTouchEventArgs(ExTouchEventArgs TouchEventArgs, BoundingClientRect? TrackRect);