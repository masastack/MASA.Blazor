using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMThemeable : IThemeable
    {
        public string ThemeClasses()
        {
            if (IsDark) return "theme--dark";
            else return "theme--light";
        }
    }
}
