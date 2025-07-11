﻿namespace Masa.Blazor.Core;

public static class RenderFragments
{
    public static RenderFragment? GenProgress(MCard card)
        => GenProgress(card.Loading, card.Color, card.LoaderHeight, card.ProgressContent);

    public static RenderFragment? GenProgress(
        StringBoolean? loading,
        string? color,
        StringNumber? loaderHeight,
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

    public static RenderFragment? GenProgressCircular(
        StringBoolean? loading,
        string? color,
        StringNumber? loaderSize,
        StringNumber? loaderWidth)
    {
        loading ??= false;
        loaderSize ??= 16;
        loaderWidth ??= 2;

        // REVIEW: should return null when loading is null?
        if (loading is { IsT1: true, AsT1: false })
        {
            return null;
        }

        return (RenderFragment)(builder =>
        {
            builder.OpenComponent<MProgressCircular>(0);
            builder.AddAttribute(1, nameof(MProgressCircular.Size), loaderSize);
            builder.AddAttribute(2, nameof(MProgressCircular.Indeterminate), true);
            builder.AddAttribute(3, nameof(MProgressCircular.Color),
                loading is not null && (loading == true || loading == "")
                    ? color ?? "primary"
                    : loading?.ToString() ?? string.Empty);
            builder.AddAttribute(4, nameof(MProgressCircular.Width), loaderWidth);
            builder.CloseComponent();
        });
    }

    public static RenderFragment? GenRipple(bool ripple, string? @class, string style)
    {
        if (!ripple)
        {
            return null;
        }

        return builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", @class);
            builder.AddAttribute(1, "style", @style);
            builder.AddAttribute(3, "ripple");
            builder.CloseElement();
        };
    }

    public static RenderFragment? RenderIfNotNull(
        RenderFragment? fragment,
        string? css = null,
        string? style = null,
        string wrapperTag = "div")
    {
        if (fragment is null)
        {
            return null;
        }

        return (RenderFragment)(builder =>
        {
            builder.OpenElement(0, wrapperTag);
            builder.AddAttribute(1, "class", css);
            builder.AddAttribute(2, "style", style);
            builder.AddContent(3, fragment);
            builder.CloseElement();
        });
    }

    public static RenderFragment? RenderIfNotNull<TContext>(
        RenderFragment<TContext>? fragment,
        TContext context,
        string? css = null,
        string? style = null,
        string wrapperTag = "div")
    {
        if (fragment is null)
        {
            return null;
        }

        return (RenderFragment)(builder =>
        {
            builder.OpenElement(0, wrapperTag);
            builder.AddAttribute(1, "class", css);
            builder.AddAttribute(2, "style", style);
            builder.AddContent(3, fragment(context));
            builder.CloseElement();
        });
    }

    public static RenderFragment RenderFragmentOrText(
        RenderFragment? fragment,
        string? text,
        string? css = null,
        string? wrapperTag = "div",
        string? style = null)
    {
        var hasWrapper = !string.IsNullOrWhiteSpace(wrapperTag);

        return builder =>
        {
            if (hasWrapper)
            {
                builder.OpenElement(0, wrapperTag!);
                builder.AddAttribute(1, "class", css);
                builder.AddAttribute(2, "style", style);
            }

            if (fragment is not null)
            {
                builder.AddContent(3, fragment);
            }
            else
            {
                builder.AddContent(4, text);
            }

            if (hasWrapper)
            {
                builder.CloseElement();
            }
        };
    }

    public static RenderFragment RenderFragmentOrText<TContext>(
        RenderFragment<TContext>? fragment,
        TContext context,
        string? text,
        string? css = null,
        string? wrapperTag = "div")
    {
        var hasWrapper = !string.IsNullOrWhiteSpace(wrapperTag);

        return builder =>
        {
            if (hasWrapper)
            {
                builder.OpenElement(0, wrapperTag!);
                builder.AddAttribute(1, "class", css);
            }

            if (fragment is not null)
            {
                builder.AddContent(2, fragment(context));
            }
            else
            {
                builder.AddContent(3, text);
            }

            if (hasWrapper)
            {
                builder.CloseElement();
            }
        };
    }

    public static RenderFragment RenderText(string? text) => builder => builder.AddContent(0, text);
}