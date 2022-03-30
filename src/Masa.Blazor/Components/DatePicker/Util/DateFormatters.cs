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

        public static string Month(int month)
        {
            return _month[month - 1];
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
