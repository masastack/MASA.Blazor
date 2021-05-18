using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MWindow : BWindow
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window")
                        .Add("m-item-group");
                })
                .Apply("container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window__container");
                });
        }
    }
}
