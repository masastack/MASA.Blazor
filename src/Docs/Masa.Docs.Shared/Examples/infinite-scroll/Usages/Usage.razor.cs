using Masa.Blazor.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Shared.Examples.infinite_scroll
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override Type UsageWrapperType => typeof(UsageWrapper);

        public Usage() : base(typeof(MInfiniteScroll)) { }
    }
}
