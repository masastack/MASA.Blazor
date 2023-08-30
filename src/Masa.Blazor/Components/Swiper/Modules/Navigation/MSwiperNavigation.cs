using Masa.Blazor.Swiper.Modules;

namespace Masa.Blazor;

public class MSwiperNavigation : SwiperModuleBase
{
    internal SwiperNavigationOptions GetOptions(string nextEl, string prevEl)
    {
        return new SwiperNavigationOptions(nextEl, prevEl);
    }
}
