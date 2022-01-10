using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface ISkeletonLoader : IElevatable, IMeasurable, IThemeable
    {
        bool Boilerplate { get; }

        bool Loading { get; }

        bool Tile { get; }

        string Transition { get; }

        string Type { get; }

        Dictionary<string, string> Types { get; }
    }
}
