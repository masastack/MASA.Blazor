namespace Masa.Blazor;

public class MSwiperPagination : ComponentBase
{
    [CascadingParameter]
    private MSwiper? Swiper { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public SwiperPaginationType Type { get; set; }

    /// <summary>
    /// Good to enable if you use bullets pagination with a lot of slides.
    /// So it will keep only few bullets visible at the same time.
    /// </summary>
    [Parameter]
    public bool DynamicBullets { get; set; }

    /// <summary>
    /// The number of main bullets visible when `dynamicBullets` enabled.
    /// </summary>
    [Parameter]
    public int DynamicMainBullets { get; set; } = 1;

    private ElementReference _elementReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Swiper == null)
        {
            return;
        }

        await Swiper.AddModuleAsync("pagination", new SwiperPaginationOptions(_elementReference, Type, DynamicBullets, DynamicMainBullets));
    }
}
