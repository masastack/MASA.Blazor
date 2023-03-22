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

        private static readonly string[] _month_CN = new string[]
        {
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月",
        };

        public static string Week(DayOfWeek week, string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }
            if (locale == "zh-CN")
                return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(week);
            else
                return week.ToString();
        }

        public static string Month(int month, string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            return locale switch
            {
                "en-US" => _month[month - 1],
                "zh-CN" => _month_CN[month - 1],
                _ => throw new NotSupportedException($"locale:{locale}"),
            };
        }

        public static Func<DateOnly, string> Year(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            return locale switch
            {
                "en-US" => date => $"{date.Year}",
                "zh-CN" => date => $"{date.Year}年",
                _ => throw new NotSupportedException($"locale:{locale}"),
            };
        }

        public static Func<DateOnly, string> Day(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            return locale switch
            {
                "en-US" => date => $"{date.Day}",
                "zh-CN" => date => $"{date.Day}",
                _ => throw new NotSupportedException($"locale:{locale}"),
            };
        }

        public static Func<DateOnly, string> Date(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            return locale switch
            {
                "en-US" => date => $"{_month[date.Month - 1]} {date.Year}",
                "zh-CN" => date => $"{date.Year}年 {date.Month}月",
                _ => throw new NotSupportedException($"locale:{locale}"),
            };
        }

        public static Func<DateOnly, string> Month(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            return locale switch
            {
                "en-US" => date => $"{_month[date.Month - 1][..3]}",
                "zh-CN" => date => $"{date.Month}月",
                _ => throw new NotSupportedException($"locale:{locale}"),
            };
        }
    }
}
