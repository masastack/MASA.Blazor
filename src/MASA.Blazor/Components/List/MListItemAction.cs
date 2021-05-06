using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MListItemAction : BListItemAction
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BListItemAction>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__action");
                });
        }
    }
}
