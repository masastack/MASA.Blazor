using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal class MCascaderMenu : MMenu
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Merge<BPopover>(attrs =>
                {
                    attrs[nameof(BPopover.PreventDefault)] = true;
                    attrs[nameof(BPopover.Style)] = Value ? "display:flex;" : "";
                });
        }
    }
}
