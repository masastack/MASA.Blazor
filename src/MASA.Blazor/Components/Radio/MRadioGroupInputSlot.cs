using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MRadioGroupInputSlot : MInputSlot
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--radio-group__input");
                });

            base.SetComponentClass();

            SlotProvider
                .Apply<BInputSlot, MInputSlot>();
        }
    }
}
