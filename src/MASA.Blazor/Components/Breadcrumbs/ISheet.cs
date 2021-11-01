using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface ISheet: IRoundable, IThemeable, IColorable, IElevatable, IMeasurable
    {
        bool Outlined { get; }

        bool Shaped { get; }

        string Tag { get; }
    }
}
