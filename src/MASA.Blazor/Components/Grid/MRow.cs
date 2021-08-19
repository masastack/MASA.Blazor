using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneOf;

namespace MASA.Blazor
{
    public partial class MRow : BRow
    {
        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignLg { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignMd { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignSm { get; set; }

        /// <summary>
        /// 'start' | 'center' | 'end' | 'baseline ' | 'stretch '
        /// </summary>
        [Parameter]
        public StringEnum<AlignTypes> AlignXl { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentLg { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentMd { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentSm { get; set; }

        /// <summary>
        /// 'start', 'end', 'center','space-between', 'space-around', 'stretch'
        /// </summary>
        [Parameter]
        public StringEnum<AlignContentTypes> AlignContentXl { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyLg { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyMd { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifySm { get; set; }

        /// <summary>
        /// 'start' | 'end' | 'center' | 'space-around' | 'space-between'
        /// </summary>
        [Parameter]
        public StringEnum<JustifyTypes> JustifyXl { get; set; }

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
        
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BRow>(cssBuilder =>
                {
                    cssBuilder
                        .Add("row")
                        .AddIf("no-gutters", () => NoGutters)
                        .AddIf("row--dense", () => Dense)
                        //'start', 'end', 'center', 'baseline', 'stretch'
                        .AddIf($"align-{Align}", () => Align != null)
                        .AddIf($"align-lg-{AlignLg}", () => AlignLg != null)
                        .AddIf($"align-md-{AlignMd}", () => AlignMd != null)
                        .AddIf($"align-sm-{AlignSm}", () => AlignSm != null)
                        .AddIf($"align-xl-{AlignXl}", () => AlignXl != null)
                        .AddIf($"justify-{Justify}", () => Justify != null)
                        .AddIf($"justify-lg-{JustifyLg}", () => JustifyLg != null)
                        .AddIf($"justify-md-{JustifyMd}", () => JustifyMd != null)
                        .AddIf($"justify-sl-{JustifySm}", () => JustifySm != null)
                        .AddIf($"justify-xl-{JustifyXl}", () => JustifyXl != null)
                        .AddIf($"align-content-{AlignContent}", () => AlignContent != null)
                        .AddIf($"align-content-lg-{AlignContentLg}", () => AlignContentLg != null)
                        .AddIf($"align-content-md-{AlignContentMd}", () => AlignContentMd != null)
                        .AddIf($"align-content-sm-{AlignContentSm}", () => AlignContentSm != null)
                        .AddIf($"align-content-xl-{AlignContentXl}", () => AlignContentXl != null);
                });
        }
    }
}