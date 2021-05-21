using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core.CssProcess;
using OneOf;

namespace BlazorComponent
{
    public static class CssBuilderExtensions
    {
        public static CssBuilder AddTheme(this CssBuilder cssBuilder, bool dark)
        {
            cssBuilder.Add(() =>
            {
                var suffix = dark ? "dark" : "light";
                return $"theme--{suffix}";
            });

            return cssBuilder;
        }

        public static CssBuilder AddElevation(this CssBuilder cssBuilder, int? elevation)
        {
            cssBuilder
                .AddIf($"elevation-{elevation}", () => elevation != null);

            return cssBuilder;
        }

        public static CssBuilder AddBackgroundColor(this CssBuilder cssBuilder, string color)
        {
            return cssBuilder.AddColor(color, false, () => true);
        }

        public static CssBuilder AddColor(this CssBuilder cssBuilder, string color, bool isText = false)
        {
            return cssBuilder.AddColor(color, isText, () => true);
        }

        public static CssBuilder AddColor(this CssBuilder cssBuilder, string color, bool isText, Func<bool> func)
        {
            if (string.IsNullOrEmpty(color) || color.StartsWith("#"))
            {
                return cssBuilder;
            }

            if (isText)
            {
                var colors = color.Split(" ");
                var firstColor = colors[0];

                cssBuilder
                    .AddIf($"{firstColor}--text", func);

                if (colors.Length == 2)
                {
                    // TODO: 是否需要正则表达式验证格式，Vuetify没有
                    // {darken|lighten|accent}-{1|2}

                    var secondColor = colors[1];
                    cssBuilder
                        .AddIf($"text--{secondColor}", func);
                }
            }
            else
            {
                cssBuilder
                    .AddIf(color, func);
            }

            return cssBuilder;
        }

        public static CssBuilder AddTextColor(this CssBuilder cssBuilder, string color, Func<bool> func)
        {
            return cssBuilder.AddColor(color, true, func);
        }

        public static CssBuilder AddTextColor(this CssBuilder cssBuilder, string color)
        {
            return cssBuilder.AddColor(color, true, () => true);
        }

        public static CssBuilder AddRounded(this CssBuilder cssBuilder, bool tile, OneOf<bool, string> rounded)
        {
            if (tile)
            {
                cssBuilder.Add("rounded-0");
            }
            else if (rounded.IsT1)
            {
                var values = rounded.AsT1.Split(' ');

                foreach (var val in values)
                {
                    cssBuilder.Add(val);
                }
            }
            else if (rounded.IsT0 && rounded.AsT0)
            {
                cssBuilder.Add("rounded");
            }

            return cssBuilder;
        }
    }
}
