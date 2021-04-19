using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MContainer : BContainer
    {
        /// <summary>
        /// 是否使用流式容器
        /// </summary>
        [Parameter]
        public bool Fluid { get; set; }

        public override void SetComponentClass()
        {
            CssBuilder
                .Add("container")
                .AddIf("container--fluid", () => Fluid);
        }
    }
}
