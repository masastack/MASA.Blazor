using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MTableHeader : BTableHeader
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BTableHeader>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table-header");
                });
        }
    }
}
