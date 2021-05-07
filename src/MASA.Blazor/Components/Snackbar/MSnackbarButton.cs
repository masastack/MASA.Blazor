using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSnackbarButton : MButton
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge<BButton>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-snack__btn");
                });
        }
    }
}
