namespace Masa.Blazor.Docs.Examples.components.carousels;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage)) { }

    protected override string ComponentName => nameof(MCarousel);

    protected override ParameterList<bool> GenToggleParameters()
    {
        return new ParameterList<bool>()
        {
            { nameof(MCarousel.HideDelimiters), false },
            { nameof(MCarousel.ShowArrowsOnHover), false }
        };
    }
}