namespace Masa.Blazor.Core;

public static class ModifierBuilderExtensions
{
    public static ModifierBuilder AddTheme(this ModifierBuilder builder, bool isDark, bool isIndependent = false)
    {
        builder.AddClass(CssClassUtils.GetTheme(isDark, isIndependent));
        return builder;
    }

    public static ModifierBuilder AddBackgroundColor(this ModifierBuilder builder, string? color, bool apply = true)
    {
        return builder.AddColor(color, false, apply);
    }

    public static ModifierBuilder AddElevation(this ModifierBuilder builder, StringNumber? elevation)
    {
        if (elevation != null)
        {
            builder.AddClass($"elevation-{elevation}");
        }

        return builder;
    }

    public static ModifierBuilder AddRounded(this ModifierBuilder builder, StringBoolean? rounded, bool tile = false)
    {
        if (tile)
        {
            builder.AddClass("rounded-0");
        }
        else
        {
            if (rounded != null)
            {
                if (rounded.IsT0)
                {
                    var values = rounded.AsT0.Split(' ');

                    foreach (var val in values)
                    {
                        builder.AddClass($"rounded-{val}");
                    }
                }
                else if (rounded.IsT1 && rounded.AsT1)
                {
                    builder.AddClass("rounded");
                }
            }
        }

        return builder;
    }

    public static ModifierBuilder AddTextColor(this ModifierBuilder bem, string? color, bool apply = true)
    {
        return bem.AddColor(color, true, apply);
    }

    public static ModifierBuilder AddColor(this ModifierBuilder bem, string? color, bool isText, bool apply = true)
    {
        if (apply)
        {
            bem.AddClass(CssClassUtils.GetColor(color, isText));
        }

        return bem;
    }
}