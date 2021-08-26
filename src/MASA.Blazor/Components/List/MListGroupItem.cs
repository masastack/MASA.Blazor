using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Components.List
{
    internal class MListGroupItem : MListItem
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
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
