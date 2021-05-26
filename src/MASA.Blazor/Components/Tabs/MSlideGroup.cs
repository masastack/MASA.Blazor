using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSlideGroup : BSlideGroup
    {
        protected override void SetComponentClass()
        {
            var prefix = "m-slide-group";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group")
                        .Add(prefix);
                })
                .Apply("wrap", cssBuilder =>
                 {
                     cssBuilder
                         .Add($"{prefix}__wrapper");
                 });

            AbstractProvider
                .Apply<BSlideGroupSlot, MSlideGroupSlot>();
        }
    }
}
