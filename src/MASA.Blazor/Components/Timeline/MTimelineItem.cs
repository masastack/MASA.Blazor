using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTimelineItem : BTimelineItem, IThemeable, ITimelineItem
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

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-timeline-item";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--fill-dot", () => FillDot)
                        .AddIf($"{prefix}--before", () => BTimeline.Reverse ? Right : Left)
                        .AddIf($"{prefix}--after", () => BTimeline.Reverse ? Right : Left)
                        .AddTheme(IsDark);
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

            AbstractProvider
                .ApplyTimelineItemDefault()
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Color)] = IconColor;
                    props[nameof(MIcon.Dark)] = !IsDark;
                    props[nameof(MIcon.Small)] = Small;
                });
        }
    }
}
