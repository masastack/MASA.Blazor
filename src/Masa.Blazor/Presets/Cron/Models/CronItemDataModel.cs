using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Presets.Cron.Models
{
    public class CronItemDataModel
    {
        public int? PeriodStart { get; set; } = 0;

        public int? PeriodEnd { get; set; } = 0;

        public int? StartFromPeriod { get; set; } = 0;

        public int StartEveryPeriod { get; set; } = 0;

        public List<int> SpecifyPeriods { get; set; } = new();

        public WeekNumbers SelectWeekNumber { get; set; }

        public DayOfWeek SelectDayOfWeek { get; set; }

        public int? NearestOfDay { get; set; }

        public int? LastPeriodOfWeek { get; set; }
    }
}
