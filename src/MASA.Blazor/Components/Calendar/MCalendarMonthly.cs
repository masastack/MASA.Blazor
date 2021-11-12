using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCalendarMonthly : MCalendarWeekly, ICalendarMonthly
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-calendar-monthly")
                        .Add("m-calendar-weekly");
                });
        }

        public override CalendarTimestamp ParsedStart() =>
            CalendarTimestampUtils.GetStartOfMonth(CalendarTimestampUtils.ParseTimestamp(Start));

        public override CalendarTimestamp ParsedEnd() =>
            CalendarTimestampUtils.GetendOfMonth(CalendarTimestampUtils.ParseTimestamp(End));
    }
}
