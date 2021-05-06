using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MListItemSubtitle : BListItemSubtitle
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BListItemSubtitle>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__subtitle");
                });
        }
    }
}
