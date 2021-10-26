using MASA.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (string.IsNullOrEmpty(color) || (!color.StartsWith("#") && !color.StartsWith("rgb")))
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
            if (IsCssColor(color))
            {
                styleBuilder
                 .Add($"background-color:{color}")
                 .Add($"border-color:{color}");
            }

            return styleBuilder;
        }

        public static StyleBuilder AddTop(this StyleBuilder styleBuilder, StringNumber top, Func<bool> func)
        {
            return styleBuilder.AddIf($"top:{top.ToUnit()}", func);
        }

        public static StyleBuilder AddRight(this StyleBuilder styleBuilder, StringNumber right, Func<bool> func)
        {
            return styleBuilder.AddIf($"right:{right.ToUnit()}", func);
        }

        private static bool IsCssColor(string color)
        {
            return !string.IsNullOrEmpty(color) && Regex.Match(color, @"^(#|var\(--|(rgb|hsl)a?\()").Success;
        }

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color, Func<bool> func)
        {
            return styleBuilder.AddColor(color, true, func);
        }

        public static StyleBuilder AddTransition(this StyleBuilder styleBuilder, string transition)
        {
            return styleBuilder.AddIf($"transition:{transition}", () => transition != null);
        }

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color)
        {
            if (IsCssColor(color))
            {
                styleBuilder
                    .Add($"color:{color}")
                    .Add($"caret-color:{color}");
            }

            return styleBuilder;
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

        public static StyleBuilder AddColorable(this StyleBuilder styleBuilder, IColorable colorable)
        {
            return styleBuilder.Add(() => ColorStyles());

            string ColorStyles()
            {
                if (string.IsNullOrEmpty(colorable.Color) is false && Regex.Match(colorable.Color, @"^(#|var\(--|(rgb|hsl)a?\()").Success)
                {
                    return $"background-color:{colorable.Color};border-color:{colorable.Color};";
                }
                return "";
            }
        }

        public static StyleBuilder AddSheet(this StyleBuilder styleBuilder, IMSheet sheet)
        {
            return styleBuilder.AddColorable(sheet)
                               .Add(() => VSheetStyles());


            string VSheetStyles()
            {
                var styles = "";
                if (sheet.Height is not null) styles += $"{ConvertToUnit("height", sheet.Height)};";
                if (sheet.MinHeight is not null) styles += $"{ConvertToUnit("min-height", sheet.MinHeight)};";
                if (sheet.MinWidth is not null) styles += $"{ConvertToUnit("min-width", sheet.MinWidth)};";
                if (sheet.MaxHeight is not null) styles += $"{ConvertToUnit("max-height", sheet.MaxHeight)};";
                if (sheet.MaxWidth is not null) styles += $"{ConvertToUnit("max-width", sheet.MaxWidth)};";
                if (sheet.Width is not null) styles += $"{ConvertToUnit("width", sheet.Width)};";

                return styles;

                string ConvertToUnit(string name, StringNumber size)
                {
                    return $"{name}: {size.ToUnit()}";
                }
            }
        }
    }
}
