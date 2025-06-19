using Masa.Blazor.Components.Transition;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MResponsive : ThemeComponentBase
{
    [Parameter] public StringNumber? AspectRatio { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool TransitionValue { get; set; }

    [Parameter] public ConditionType TransitionType { get; set; }

    private Block _block = new("m-responsive");

    protected virtual StringNumber? ComputedAspectRatio => AspectRatio;

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Height)
            .AddWidth(Width)
            .AddMinWidth(MinWidth)
            .AddMaxWidth(MaxWidth)
            .AddMinHeight(MinHeight)
            .AddMaxHeight(MaxHeight)
            .GenerateCssStyles();
    }

    private string? GenAspectStyle(StringNumber? aspectRatio)
    {
        if (aspectRatio == null) return null;

        var (isNumber, ratio) = aspectRatio.TryGetNumber();

        if (!isNumber) return null;

        var paddingBottom = (1 / ratio).ToString("P2", CultureInfo.InvariantCulture);

        return $"padding-bottom: {paddingBottom.Replace(" ", "")}";
    }
}