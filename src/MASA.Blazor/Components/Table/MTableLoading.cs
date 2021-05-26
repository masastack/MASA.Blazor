using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MTableLoading : BTableLoading
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BTableLoading>()
                .Apply("progress", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__progress");
                })
                .Apply("column", cssBuilder =>
                {
                    cssBuilder
                        .Add("column");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("padding:0");
                });

            AbstractProvider
                .Apply<BProcessLinear, MProcessLinear>(properties =>
                {
                    properties[nameof(MProcessLinear.Color)] = "primary";
                    properties[nameof(MProcessLinear.Indeterminate)] = true;
                });
        }
    }
}
