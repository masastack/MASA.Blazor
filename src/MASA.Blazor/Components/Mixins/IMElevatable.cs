using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMElevatable : IElevatable
    {
        public string ElevationClasses()
        {
            if (Elevation is null) return "";
            if (int.TryParse(Elevation.ToString(), out var number))
            {
                return $"elevation-{number}";
            }
            else return "";
        }
    }
}
