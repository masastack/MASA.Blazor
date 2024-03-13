namespace Masa.Blazor;

public interface ISwiperJSObjectReferenceProxy
{
    Task SlideToAsync(int index, int speed);

    Task SlideNextAsync(int speed);

    Task SlidePrevAsync(int speed);

    Task DisposeAsync();
}
