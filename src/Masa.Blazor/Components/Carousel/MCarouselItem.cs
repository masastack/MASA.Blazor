using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor;

public class MCarouselItem : MWindowItem, IRoutable
{
    [Parameter]
    public string Href { get; set; }

    [Parameter]
    public bool Link { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public string Tag { get; set; }

    [Parameter]
    public string Target { get; set; }

    private IRoutable _router;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _router = new Router(this);
        (Tag, Attributes) = _router.GenerateRouteLink();
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        AbstractProvider
            .Merge(typeof(BWindowItemDefaultSlot<>), typeof(BCarouselItemDefaultSlot<MCarouselItem>))
            .Apply(typeof(BResponsive), typeof(MImage), attrs =>
            {
                attrs[nameof(MImage.Class)] = "m-carousel__item";

                if (WindowGroup is MCarousel carousel)
                {
                    Console.WriteLine($"carousel height:{carousel.InternalHeight}");
                    attrs[nameof(MImage.Height)] = carousel.InternalHeight;
                }
            });
    }
}
