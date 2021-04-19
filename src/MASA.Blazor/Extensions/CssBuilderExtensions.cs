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
        public static BuilderBase AddTheme(this BuilderBase cssBuilder, bool dark)
        {
            cssBuilder.Add(() =>
             {
                 var suffix = dark ? "dark" : "light";
                 return $"theme--{suffix}";
             });

            return cssBuilder;
        }
    }
}
