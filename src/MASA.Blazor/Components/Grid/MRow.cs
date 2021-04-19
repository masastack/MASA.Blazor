using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MRow : BRow
    {
        /// <summary>
        /// Removes the gutter between v-cols.
        /// </summary>
        [Parameter]
        public bool NoGutters { get; set; }

        /// <summary>
        /// Reduces the gutter between v-cols.
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public string AlignContent { get; set; }

        public override void SetComponentClass()
        {
            CssBuilder.Add("row")
                .AddIf("no-gutters", () => NoGutters)
                .AddIf("row--dense", () => Dense)
                //'start', 'end', 'center', 'baseline', 'stretch'
                .AddIf($"align-{Align}", () => !string.IsNullOrEmpty(Align))
                .AddIf($"justify-{Justify}", () => !string.IsNullOrEmpty(Justify))
                .AddIf($"align-content-{AlignContent}", () => !string.IsNullOrEmpty(AlignContent));
        }
    }
}
