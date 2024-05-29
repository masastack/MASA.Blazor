namespace Masa.Blazor;

public partial class MSwiperSlide
{
    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private static Block _block => new("m-swiper-slide");
    private ModifierBuilder _modifierBuilder => _block.CreateModifierBuilder();
}