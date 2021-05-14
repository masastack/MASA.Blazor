using MASA.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class StyleBuilderExtensions
    {
        public static StyleBuilder AddColor(this StyleBuilder styleBuilder, string color, bool isText)
        {
            return styleBuilder.AddColor(color, isText, () => true);
        }

        public static StyleBuilder AddColor(this StyleBuilder styleBuilder, string color, bool isText, Func<bool> func)
        {
            if (string.IsNullOrEmpty(color) || !color.StartsWith("#"))
            {
                return styleBuilder;
            }

            if (isText)
            {
                styleBuilder
                .AddIf($"color: {color}", func);
            }
            else
            {
                styleBuilder
                        .AddIf($"background-color: {color}", func);
            }

            return styleBuilder;
        }

        public static StyleBuilder AddBackgroundColor(this StyleBuilder styleBuilder, string color)
        {
            if (string.IsNullOrEmpty(color) || !color.StartsWith("#"))
            {
                return styleBuilder;
            }

            styleBuilder
                .Add($"background-color:{color}")
                .Add($"border-color:{color}");

            return styleBuilder;
        }

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color, Func<bool> func)
        {
            return styleBuilder.AddColor(color, true, func);
        }

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color)
        {
            return styleBuilder.AddColor(color, true, () => true);
        }

        public static StyleBuilder AddHeight(this StyleBuilder styleBuilder, StringNumber height)
        {
            return styleBuilder
                        .AddSize("height", height);
        }

        private static StyleBuilder AddSize(this StyleBuilder styleBuilder, string name, StringNumber size)
        {
            return styleBuilder
                        .AddIf(() => $"{name}: {size.ToUnit()}", () => size != null);
        }

        public static StyleBuilder AddWidth(this StyleBuilder styleBuilder, StringNumber width)
        {
            return styleBuilder
                        .AddSize("width", width);
        }

        public static StyleBuilder AddMinWidth(this StyleBuilder styleBuilder, StringNumber minWidth)
        {
            return styleBuilder
                        .AddSize("min-width", minWidth);
        }

        public static StyleBuilder AddMaxWidth(this StyleBuilder styleBuilder, StringNumber maxWidth)
        {
            return styleBuilder
                        .AddSize("max-width", maxWidth);
        }

        public static StyleBuilder AddMinHeight(this StyleBuilder styleBuilder, StringNumber minHeight)
        {
            return styleBuilder
                        .AddSize("min-height", minHeight);
        }

        public static StyleBuilder AddMaxHeight(this StyleBuilder styleBuilder, StringNumber maxHeight)
        {
            return styleBuilder
                        .AddSize("max-height", maxHeight);
        }
    }
}
