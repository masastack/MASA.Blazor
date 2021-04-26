using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MTableFooterButton:BButton
    {
        [Parameter]
        public string IconName { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> HandlePageChange { get; set; }
    }
}
