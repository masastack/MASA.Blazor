namespace Masa.Blazor.Swiper.Modules;

public class SwiperNavigationOptions
{
    public SwiperNavigationOptions(
        string nextEl,
        string prevEl)
    {
        NextEl = nextEl;
        PrevEl = prevEl;
    }

    public string NextEl { get; }

    public string PrevEl { get; }
}
