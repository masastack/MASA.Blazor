namespace Masa.Blazor.Swiper.Modules;

public class SwiperAutoplayOptions
{
    public SwiperAutoplayOptions(MSwiperAutoplay autoplay)
    {
        Delay = autoplay.Delay;
        DisableOnInteraction = autoplay.DisableOnInteraction;
        PauseOnMouseEnter = autoplay.PauseOnMouseEnter;
        ReverseDirection = autoplay.ReverseDirection;
        StopOnLastSlide = autoplay.StopOnLastSlide;
        WaitForTransition = autoplay.WaitForTransition;
    }

    public int Delay { get; }

    public bool DisableOnInteraction { get; }

    public bool PauseOnMouseEnter { get; }

    public bool ReverseDirection { get; }

    public bool StopOnLastSlide { get; }

    public bool WaitForTransition { get; }
}
