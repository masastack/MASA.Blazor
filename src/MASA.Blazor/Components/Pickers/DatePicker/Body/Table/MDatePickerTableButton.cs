using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerTableButton : MButton
    {
        [Parameter]
        public bool IsCurrent { get; set; }

        protected override bool HasBackground => base.HasBackground || IsActive;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (!IsCurrent && !IsActive)
            {
                Color = null;
            }
        }
    }
}
