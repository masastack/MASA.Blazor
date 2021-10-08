using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    internal partial class MTableLoading : BTableLoading
    {
        protected override void SetComponentClass()
        {
            CssProvider
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
                .Apply<BProgressLinear, MProgressLinear>(props =>
                {
                    props[nameof(MProgressLinear.Color)] = "primary";
                    props[nameof(MProgressLinear.Indeterminate)] = true;
                });
        }
    }
}
