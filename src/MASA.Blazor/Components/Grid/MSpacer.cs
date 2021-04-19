using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSpacer : BSpacer
    {
        public override void SetComponentClass()
        {
            CssBuilder.Add("spacer");
        }
    }
}
