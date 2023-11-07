using Masa.Blazor.Swiper.Modules;

namespace Masa.Blazor;

public class MSwiperAutoplay : SwiperModuleBase
{
    /// <summary>
    /// Delay between transitions (in ms).
    /// If this parameter is not specified, auto play will be disabled
    /// </summary>
    [Parameter] [MassApiParameter(3000)] public int Delay { get; set;  } = 3000;

    /// <summary>
    /// Set to false and autoplay will not be disabled after user interactions (swipes),
    /// it will be restarted every time after interaction
    /// </summary>
    [Parameter] [MassApiParameter(true)] public bool DisableOnInteraction { get; set; } = true;

    /// <summary>
    /// When enabled autoplay will be paused on pointer (mouse) enter over Swiper container.
    /// </summary>
    [Parameter] public bool PauseOnMouseEnter { get; set; }

    /// <summary>
    /// Enables autoplay in reverse direction
    /// </summary>
    [Parameter] public bool ReverseDirection { get; set; }

    /// <summary>
    /// Enable this parameter and autoplay will be stopped when it reaches last slide (has no effect in loop mode)
    /// </summary>
    [Parameter] public bool StopOnLastSlide { get; set; }

    /// <summary>
    /// When enabled autoplay will wait for wrapper transition to continue.
    /// Can be disabled in case of using Virtual Translate when your slider may not have transition
    /// </summary>
    [Parameter] [MassApiParameter(true)] public bool WaitForTransition { get; set; } = true;

    internal SwiperAutoplayOptions GetOptions()
    {
        return new SwiperAutoplayOptions(this);
    }
}
