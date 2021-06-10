using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTimelineItem : BTimelineItem
    {
        [Parameter]
        public string Color { get; set; } = "primary";

        [Parameter]
        public bool FillDot { get; set; }

        [Parameter]
        public bool HideDot { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string IconColor { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-timeline-item";

            CssProvider
                .AsProvider<BTimelineItem>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--fill-dot", () => FillDot)
                        .AddIf($"{prefix}--before", () => BTimeline.Reverse ? Right : Left)
                        .AddIf($"{prefix}--after", () => BTimeline.Reverse ? Right : Left)
                        .AddTheme(Dark);
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__body");
                })
                .Apply("dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__dot")
                        .AddIf($"{prefix}__dot--small", () => Small)
                        .AddIf($"{prefix}__dot--large", () => Large);
                })
                .Apply("inner-dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__inner-dot")
                        .AddBackgroundColor(Color);
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__divider");
                })
                .Apply("opposite", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__opposite");
                });

            Attributes.Add("data-app", true);
        }
    }
}
