using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MCalendarDaily : BCalendarDaily, ICalendarDaily
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark => Dark ?
            true :
            (Light ? false : Themeable != null && Themeable.IsDark);

        protected double _scrollPush = 0;

        public ElementReference ScrollAreaElement { get; set; }

        public ElementReference PaneElement { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                       .Add("m-calendar-daily")
                       .AddTheme(IsDark);
                })
                .Apply("head", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__head");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"margin-right:{_scrollPush}px");
                })
                .Apply("intervals", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__intervals-head");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(IntervalWidth);
                })
                .Apply("headDay", cssBuilder =>
                {
                    var timestamp = cssBuilder.Data as CalendarTimestamp;
                    cssBuilder
                        .Add("m-calendar-daily_head-day")
                        .AddIf("m-present", () => timestamp?.Present ?? false)
                        .AddIf("m-past", () => timestamp?.Past ?? false)
                        .AddIf("m-future", () => timestamp?.Future ?? false);
                })
                .Apply("headWeekday", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily_head-weekday");
                }, styleBuilder =>
                {
                    var timestamp = styleBuilder.Data as CalendarTimestamp;
                    styleBuilder
                        .AddTextColor((timestamp?.Present ?? false) ? Color : string.Empty);
                })
                .Apply("headDayLabel", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily_head-day-label");
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__body");
                })
                .Apply("scrollArea", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__scroll-area");
                })
                .Apply("pane", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__pane");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(BodyHeight);
                })
                .Apply("dayContainer", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__day-container");
                })
                .Apply("intervalsBody", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__intervals-body");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(IntervalWidth);
                })
                .Apply("intervalsLabel", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__interval");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(IntervalHeight);
                })
                .Apply("intervalsText", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__interval-text");
                })
                .Apply("day", cssBuilder =>
                {
                    var timestamp = cssBuilder.Data as CalendarTimestamp;
                    cssBuilder
                        .Add("m-calendar-daily__day")
                        .AddIf("m-present", () => timestamp?.Present ?? false)
                        .AddIf("m-past", () => timestamp?.Past ?? false)
                        .AddIf("m-future", () => timestamp?.Future ?? false);
                })
                .Apply("dayIntervals", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-daily__day-interval");
                }, styleBuilder =>
                {
                    var timestamp = styleBuilder.Data as CalendarTimestamp;
                    //var styler = IntervalStyle(timestamp);
                    //TODO ...styler(interval),
                    styleBuilder
                        .AddHeight(IntervalHeight);
                });
            
            AbstractProvider
                .ApplyCalendarDailyDefault()
                .Apply<BButton, MButton>(props =>
                {
                    props.TryGetValue("ItemIndex", out var itemIndexStr);
                    var itemIndex = Convert.ToInt32(itemIndexStr);
                    var day = Days?[itemIndex];

                    props[nameof(MButton.Color)] = (day?.Present ?? false) ? Color : "transparent";
                    props[nameof(MButton.Fab)] = true;
                    props[nameof(MButton.Depressed)] = true;
                    //TODO getMouseEventHandlers
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var area = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, ScrollAreaElement);
                var pane = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, PaneElement);

                _scrollPush = area != null && pane != null ? (area.OffsetWidth - pane.OffsetWidth) : 0;
            }
        }

        public string GenIntervalLabel(CalendarTimestamp interval)
        {
            var @short = ShortIntervals;
            var shower = ShowIntervalLabel ?? ShowIntervalLabelDefault;
            var show = shower(interval);
            var label = show ? IntervalFormatter(interval, @short) : null;

            return label;
        }

        public List<CalendarTimestamp> GenDayIntervals(int index)
        {
            var intervals = Intervals();

            return intervals.Count > index ? intervals[index] : null;
        }
    }
}
