using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public static class StringNumberExtensions
    {
        public static string ToUnit(this StringNumber stringNumber, string unit = "px")
        {
            if (stringNumber == null)
            {
                return $"0{unit}";
            }

            return stringNumber.Match(
                t0 => t0,
                t1 => $"{t1}{unit}",
                t2 => $"{t2}{unit}"
                );
        }
    }
}
