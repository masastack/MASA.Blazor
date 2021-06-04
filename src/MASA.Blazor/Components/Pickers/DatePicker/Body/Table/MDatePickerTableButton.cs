using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerTableButton : MButton
    {
        [Parameter]
        public bool IsCurrent { get; set; }

        protected override bool HasBackgroud => base.HasBackgroud || IsActive;

        protected override void OnParametersSet()
        {
            if (!IsCurrent && !IsActive)
            {
                Color = null;
            }
        }
    }
}
