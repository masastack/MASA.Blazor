using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Components.List
{
    public class MListGroupItem : MListItem
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge<BListItem>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-group__header");
                });
        }

        protected override void OnAfterRender(bool firstRender)
        {
        }
    }
}
