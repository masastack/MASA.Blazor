namespace Masa.Blazor.Core;

public static class RenderFragments
{
    public static RenderFragment? GenProgress(MCard card)
        => GenProgress(card.Loading, card.Color, card.LoaderHeight, card.ProgressContent);

    public static RenderFragment? GenProgress(StringBoolean? loading, string? color, StringNumber? loaderHeight,
        RenderFragment? childContent)
    {
        loading ??= false;
        loaderHeight ??= 2;

        // REVIEW: should return null when loading is null?
        if (loading is { IsT1: true, AsT1: false })
        {
            return null;
        }

        return childContent ?? (RenderFragment)(builder =>
        {
            builder.OpenComponent<MProgressLinear>(0);
            builder.AddAttribute(1, nameof(MProgressLinear.Absolute), true);
            builder.AddAttribute(2, nameof(MProgressLinear.Indeterminate), true);
            builder.AddAttribute(3, nameof(MProgressLinear.Color),
                loading is not null && (loading == true || loading == "")
                    ? color ?? "primary"
                    : loading?.ToString() ?? string.Empty);
            builder.AddAttribute(4, nameof(MProgressLinear.Height), loaderHeight);
            builder.CloseComponent();
        });
    }
}