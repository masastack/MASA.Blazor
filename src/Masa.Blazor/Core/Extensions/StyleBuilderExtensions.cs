﻿namespace Masa.Blazor;

using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

public static class StyleBuilderExtensions
{
    public static StyleBuilder AddColor(this StyleBuilder styleBuilder, string? color, bool isText)
    {
        return styleBuilder.AddColor(color, isText, () => true);
    }

    public static StyleBuilder AddColor(this StyleBuilder styleBuilder, string? color, bool isText, Func<bool> func)
    {
        if (string.IsNullOrEmpty(color) || (!color.StartsWith("#") && !color.StartsWith("rgb")))
        {
            return styleBuilder;
        }

        return styleBuilder.AddIf(isText ? "color" : "background-color", color, func.Invoke());
    }

    public static StyleBuilder AddBackgroundColor(this StyleBuilder styleBuilder, string? color, bool apply = true)
    {
        if (apply && color != null && IsCssColor(color))
        {
            styleBuilder
                .Add("background-color", color)
                .Add("border-color", color);
        }

        return styleBuilder;
    }

    public static StyleBuilder AddTop(this StyleBuilder styleBuilder, StringNumber top)
    {
        return styleBuilder.Add("top", top.ToUnit());
    }

    public static StyleBuilder AddTop(this StyleBuilder styleBuilder, StringNumber top, Func<bool> func)
    {
        return styleBuilder.AddIf("top", top.ToUnit(), func.Invoke());
    }

    public static StyleBuilder AddRight(this StyleBuilder styleBuilder, StringNumber right, Func<bool> func)
    {
        return styleBuilder.AddIf("right", right.ToUnit(), func.Invoke());
    }

    private static bool IsCssColor(string color)
    {
        return Regex.Match(color, @"^(#|var\(--|(rgb|hsl)a?\()").Success;
    }

    public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string? color, Func<bool> func)
    {
        return styleBuilder.AddColor(color, true, func);
    }

    public static StyleBuilder AddTransition(this StyleBuilder styleBuilder, string? transition)
    {
        return styleBuilder.AddIf("transition", transition, transition != null);
    }

    public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string? color)
    {
        if (color != null && IsCssColor(color))
        {
            styleBuilder
                .Add("color", color)
                .Add("caret-color", color);
        }

        return styleBuilder;
    }

    public static StyleBuilder AddHeight(this StyleBuilder styleBuilder, StringNumber? height, bool isImportant = false)
    {
        return styleBuilder
            .AddSize("height", height, isImportant);
    }

    public static StyleBuilder AddWidth(this StyleBuilder styleBuilder, StringNumber? width, bool isImportant = false, Func<bool>? predicate = null)
    {
        if (predicate?.Invoke() is null or true)
        {
            styleBuilder.AddSize("width", width, isImportant);
        }

        return styleBuilder;
    }

    public static StyleBuilder AddMinWidth(this StyleBuilder styleBuilder, StringNumber? minWidth,
        bool isImportant = false)
    {
        return styleBuilder
            .AddSize("min-width", minWidth, isImportant);
    }

    public static StyleBuilder AddMaxWidth(this StyleBuilder styleBuilder, StringNumber? maxWidth,
        bool isImportant = false)
    {
        return styleBuilder
            .AddSize("max-width", maxWidth, isImportant);
    }

    public static StyleBuilder AddMinHeight(this StyleBuilder styleBuilder, StringNumber? minHeight,
        bool isImportant = false)
    {
        return styleBuilder
            .AddSize("min-height", minHeight, isImportant);
    }

    public static StyleBuilder AddMaxHeight(this StyleBuilder styleBuilder, StringNumber? maxHeight,
        bool isImportant = false)
    {
        return styleBuilder
            .AddSize("max-height", maxHeight, isImportant);
    }

    public static StyleBuilder AddMeasurable(this StyleBuilder styleBuilder, IMeasurable measurable)
    {
        return styleBuilder
            .AddSize("height", measurable.Height)
            .AddSize("min-height", measurable.MinHeight)
            .AddSize("max-height", measurable.MaxHeight)
            .AddSize("width", measurable.Width)
            .AddSize("min-width", measurable.MinWidth)
            .AddSize("max-width", measurable.MaxWidth);
    }

    private static StyleBuilder AddSize(this StyleBuilder styleBuilder, string name, StringNumber? size,
        bool isImportant = false)
    {
        return styleBuilder.AddIf(name, size.ToUnit(), size != null, isImportant);
    }
}