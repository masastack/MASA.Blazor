using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMeasurable
    {
        public StringNumber Height { get; }

        public StringNumber MaxHeight { get; }

        public StringNumber MaxWidth { get; }

        public StringNumber MinHeight { get; }

        public StringNumber MinWidth { get; }

        public StringNumber Width { get; }
    }
}
