using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Shared.Examples.footers
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MFooter)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.OpenComponent<Index>(0);
            builder.CloseComponent();
        };

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
                { "Style", "width:100%" }
            };
        }
    }
}
