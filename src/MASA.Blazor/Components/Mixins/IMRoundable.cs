using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMRoundable 
    {
        StringBoolean Rounded { get; }

        bool Tile { get; }    
    }
}
