using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMVSheet: IVSheet, IMRoundable, IMThemeable, IMColorable, IMElevatable, IMeasurable
    {
        public string VSheetClasses()
        {
            var composite = new List<string>();
            composite.Add("m-sheet");
            if (Outlined) composite.Add("m-sheet--outlined");
            else if (Shaped) composite.Add("m-sheet--shaped");
            composite.Add(ThemeClasses());
            composite.Add(ElevationClasses());
            composite.Add(RoundedClasses());
            return String.Join(" ", composite);
        }

        public string VSheetStyles()
        {
            var styles = ColorStyles();
            if (Height is not null) styles += $"{ConvertToUnit("height", Height)};";
            if (MinHeight is not null) styles += $"{ConvertToUnit("min-height", MinHeight)};";
            if (MinWidth is not null) styles += $"{ConvertToUnit("min-width", MinWidth)};";
            if (MaxHeight is not null) styles += $"{ConvertToUnit("max-height", MaxHeight)};";
            if (MaxWidth is not null) styles += $"{ConvertToUnit("max-width", MaxWidth)};";
            if (Width is not null) styles += $"{ConvertToUnit("width", Width)};";

            return styles;

            string ConvertToUnit(string name, StringNumber size)
            {
                return $"{name}: {size.ToUnit()}";
            }
        }
    }
}
