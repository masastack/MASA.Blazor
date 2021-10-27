using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMCard : ICard,IMLoadable, IMRoutable, IMSheet
    {
        bool Flat { get; }

        bool Hover { get; }

        string Img { get; }

        bool Raised { get; }
    }
}
