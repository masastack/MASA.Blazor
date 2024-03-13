using Masa.Blazor.Utils;

namespace Masa.Blazor;

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
    public Task OnMouseDownInternal(ExMouseEventArgs args)
        => _slider.HandleOnSliderMouseDownAsync(args);

    [JSInvokable]
    public Task OnTouchStartInternal(ExTouchEventArgs args)
        => _slider.HandleOnTouchStartAsync(args);

    [JSInvokable]
    public Task OnMouseUpInternal()
        => _slider.HandleOnSliderEndSwiping();

    [JSInvokable]
    public Task OnMouseMoveInternal(MouseEventArgs args)
        => _throttleTask.RunAsync(() => _slider.HandleOnMouseMoveAsync(args));
}
