using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MListItemTitle : BListItemTitle
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BListItemTitle>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__title");
                });
        }
    }
}
