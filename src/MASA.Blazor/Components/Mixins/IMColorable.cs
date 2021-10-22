using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMColorable : IColorable
    {
        public string ColorStyles()
        {
            if (string.IsNullOrEmpty(Color) is false && Regex.Match(Color, @"^(#|var\(--|(rgb|hsl)a?\()").Success)
            {
                return $"background-color:{Color};border-color:{Color};";
            }
            return "";
        }
    }
}
