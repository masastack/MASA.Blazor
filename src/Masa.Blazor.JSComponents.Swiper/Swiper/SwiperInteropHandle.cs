namespace Masa.Blazor;

public class SwiperInteropHandle
{
    private readonly MSwiper _swiper;

    public SwiperInteropHandle(MSwiper swiper)
    {
        _swiper = swiper;
    }

    [JSInvokable]
    public async Task OnIndexChanged(int index)
    {
        await _swiper.UpdateIndexAsync(index);
    }
}
