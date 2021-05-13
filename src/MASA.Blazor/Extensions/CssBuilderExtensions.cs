using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core.CssProcess;

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

        public static CssBuilder AddColor(this CssBuilder cssBuilder, string color, bool isText)
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
    }
}
