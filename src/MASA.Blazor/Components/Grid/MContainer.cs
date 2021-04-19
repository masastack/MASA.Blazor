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
        /// 是否删除视图最大宽度大小的断点
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
