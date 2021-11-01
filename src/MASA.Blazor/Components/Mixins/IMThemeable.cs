using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMThemeable
    {
        public bool Dark { get; set; }

        public bool Light { get; set; }

        public bool IsCascadingDark { get; }

        public bool IsGloabDark { get; }
    }
}
