using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MForm : BForm
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BForm>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-form");
                });
        }
    }
}
