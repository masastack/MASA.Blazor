namespace Masa.Blazor;

public class MCarouselItem : MWindowItem, IRoutable
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public string? Href { get; set; }

    [Parameter] public bool Link { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public string? Tag { get; set; }

    [Parameter] public string? Target { get; set; }

    [Parameter] public string? Src { get; set; }

    public bool Exact { get; }

    public string? MatchPattern { get; }

    private IRoutable? _router;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _router = new Router(this);

        var noRipple = Attributes.TryGetValue("ripple", out var ripple) && ripple is false or "false";

        (Tag, Attributes) = _router.GenerateRouteLink();

        if (Tag == "a" && !noRipple)
        {
            Attributes["ripple"] = true;
        }
    }

    protected override RenderFragment GenChildContent()
    {
        return builder =>
        {
            builder.OpenComponent<MImage>(0);
            builder.AddAttribute(1, "Class", "m-carousel__item");
            builder.AddAttribute(2, "Src", Src);
            builder.AddAttribute(3, "Height", ((MCarousel?)WindowGroup)?.InternalHeight);
            builder.CloseComponent();
        };
    }
}