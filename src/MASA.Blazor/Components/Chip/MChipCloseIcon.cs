using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MChipCloseIcon : MIcon
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge<BIcon>(cssBuilder =>
                {
                    cssBuilder
                       .Add("m-chip__close");
                });
        }
    }
}
