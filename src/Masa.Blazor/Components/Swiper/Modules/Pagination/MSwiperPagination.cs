using Masa.Blazor.Swiper.Modules;

namespace Masa.Blazor;

public class MSwiperPagination : SwiperModuleBase
{
    /// <summary>
    /// If true then clicking on pagination button will cause transition to appropriate slide.
    /// Only for bullets pagination type.
    /// </summary>
    [Parameter] public bool Clickable { get; set; }

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

    /// <summary>
    /// Toggle (hide/show) pagination container visibility after click on Slider's container
    /// </summary>
    [Parameter] public bool HideOnClick { get; set; }

    internal SwiperPaginationOptions GetOptions(string el)
    {
        return new SwiperPaginationOptions(el, this);
    }
}
