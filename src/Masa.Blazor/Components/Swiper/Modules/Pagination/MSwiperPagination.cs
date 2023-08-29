using Masa.Blazor.Swiper.Modules;

namespace Masa.Blazor;

public class MSwiperPagination : SwiperModuleBase
{
    [Parameter] public SwiperPaginationType Type { get; set; }

    /// <summary>
    /// Good to enable if you use bullets pagination with a lot of slides.
    /// So it will keep only few bullets visible at the same time.
    /// </summary>
    [Parameter] public bool DynamicBullets { get; set; }

    /// <summary>
    /// The number of main bullets visible when `dynamicBullets` enabled.
    /// </summary>
    [Parameter] public int DynamicMainBullets { get; set; } = 1;

    internal SwiperPaginationOptions GetOptions(string el)
    {
        return new SwiperPaginationOptions(el, Type, DynamicBullets, DynamicMainBullets);
    }
}
