using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MAvatar : Container
{
    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] [MasaApiParameter(48)] public StringNumber? Size { get; set; } = 48;

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public string? Color { get; set; }

    private Block _block = new("m-avatar");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(Left)
            .And(Right)
            .AddRounded(Rounded, Tile)
            .AddBackgroundColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        var isDirtySize = IsDirtyParameter(nameof(Size));

        return StyleBuilder.Create()
            .AddHeight(Size, isDirtySize)
            .AddMinWidth(Size, isDirtySize)
            .AddWidth(Size, isDirtySize)
            .AddHeight(Height, IsDirtyParameter(nameof(Height)))
            .AddWidth(Width, IsDirtyParameter(nameof(Width)))
            .AddMinWidth(MinWidth, IsDirtyParameter(nameof(MinWidth)))
            .AddMaxWidth(MaxWidth, IsDirtyParameter(nameof(MaxWidth)))
            .AddMinHeight(MinHeight, IsDirtyParameter(nameof(MinHeight)))
            .AddMaxHeight(MaxHeight, IsDirtyParameter(nameof(MaxHeight)))
            .AddBackgroundColor(Color)
            .GenerateCssStyles();
    }
}