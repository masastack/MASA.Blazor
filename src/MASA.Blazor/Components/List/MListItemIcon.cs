using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MListItemIcon : BListItemIcon
    {
        public override void SetComponentClass()
        {
            CssBuilder
                .Add("m-list-item__icon");
        }
    }
}
