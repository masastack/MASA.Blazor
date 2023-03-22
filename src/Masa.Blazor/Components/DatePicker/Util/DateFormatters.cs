using System;

namespace Masa.Blazor
{
    public static class DateFormatters
    {

        private static readonly string[] _month = new string[]
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        public static string Week(DayOfWeek dayOfWeek, I18n i18n)
        {
            var dayOfWeekStr = i18n.T($"$masaBlazor.dayOfWeek.{dayOfWeek}");
            if (i18n?.Culture?.Name == "zh-CN")
                dayOfWeekStr = $"星期{dayOfWeekStr}";
            return dayOfWeekStr;
        }

        public static string Month(int month, I18n i18n)
        {
            var monthStr = i18n.T($"$masaBlazor.month.{_month[month - 1]}");
            if (i18n?.Culture?.Name == "en-US")
                monthStr = monthStr[..3];
            return monthStr;
        }

        public static Func<DateOnly, string> Year(I18n i18N)
        {
            return i18N.Culture.Name switch
            {
                "en-US" => date => $"{date.Year}",
                "zh-CN" => date => $"{date.Year}年",
                _ => throw new NotSupportedException($"locale:{i18N.Culture.Name}"),
            };
        }

        public static Func<DateOnly, string> Day(I18n i18N)
        {
            return i18N.Culture.Name switch
            {
                "en-US" => date => $"{date.Day}",
                "zh-CN" => date => $"{date.Day}",
                _ => throw new NotSupportedException($"locale:{i18N.Culture.Name}"),
            };
        }

        public static Func<DateOnly, string> Date(I18n i18N)
        {
            return i18N.Culture.Name switch
            {
                "en-US" => date => $"{_month[date.Month - 1]} {date.Year}",
                "zh-CN" => date => $"{date.Year}年 {date.Month}月",
                _ => throw new NotSupportedException($"locale:{i18N.Culture.Name}"),
            };
        }

        public static Func<DateOnly, string> Month(I18n i18N)
        {
            return i18N.Culture.Name switch
            {
                "en-US" => date => $"{_month[date.Month - 1][..3]}",
                "zh-CN" => date => $"{date.Month}月",
                _ => throw new NotSupportedException($"locale:{i18N.Culture.Name}"),
            };
        }
    }
}
