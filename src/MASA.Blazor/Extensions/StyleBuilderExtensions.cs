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

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color, Func<bool> func)
        {
            return styleBuilder.AddColor(color, true, func);
        }

        public static StyleBuilder AddTextColor(this StyleBuilder styleBuilder, string color)
        {
            return styleBuilder.AddColor(color, true, () => true);
        }
    }
}
