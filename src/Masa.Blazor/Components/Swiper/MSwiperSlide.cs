namespace Masa.Blazor;

public class MSwiperSlide : Container
{
    [CascadingParameter] private MSwiper? Swiper { get; set; }

    private static Block _block => new("m-swiper-slide");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await NotifySwiperAsync();
        }
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _block.Name;
        yield return "swiper-slide";
    }

    private async Task NotifySwiperAsync()
    {
        if (Swiper is null)
        {
            return;
        }

        await Swiper.UpdateAsync();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await NotifySwiperAsync();
    }
}