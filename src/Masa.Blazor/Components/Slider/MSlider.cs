namespace Masa.Blazor;

#if NET6_0
public class MSlider<TValue> : MSliderBase<TValue, TValue>
#else
public class MSlider<TValue> : MSliderBase<TValue, TValue> where TValue : struct, IComparable<TValue>
#endif
{
}
