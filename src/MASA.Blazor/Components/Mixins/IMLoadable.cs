using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMLoadable<TLoadable> : ILoadable<TLoadable, MProgressLinear>, IMColorable where TLoadable : ILoadable 
    {

    }
}
