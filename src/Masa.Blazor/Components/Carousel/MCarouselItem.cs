namespace Masa.Blazor;

public class MCarouselItem : MWindowItem, IRoutable
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public bool Link { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public string? Tag { get; set; }

    [Parameter]
    public string? Target { get; set; }

    [Parameter]
    public string? Src { get; set; }

    public bool Exact { get; }

    public string? MatchPattern { get; }

    private IRoutable? _router;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

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
                attrs[nameof(MImage.Src)] = Src;

                if (WindowGroup is MCarousel carousel)
                {
                    attrs[nameof(MImage.Height)] = carousel.InternalHeight;
                }
            });
    }
}
