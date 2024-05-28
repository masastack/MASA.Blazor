using System.Runtime;

namespace BlazorComponent
{
    public static class StringNumberExtensions
    {
        public static string ToUnit(this StringNumber? stringNumber, string unit = "px")
        {
            if (stringNumber == null)
            {
                return $"0{unit}";
            }

            return stringNumber.Match(
                t0 => t0,
                t1 => $"{t1}{unit}",
                t2 => $"{t2}{unit}"
            );
        }

        public static string? ToUnitOrNull(this StringNumber? stringNumber, string unit = "px")
        {
            return stringNumber == null ? null : stringNumber.ToUnit(unit);
        }

        // TODO: ConvertToUnit更接近vuetify源码
        // TODO: 是否可以把上面的ToUnit删掉

        public static string? ConvertToUnit(this StringNumber? stringNumber, string unit = "px")
        {
            if (stringNumber == null)
            {
                return null;
            }

            return stringNumber.Match(
                t0 => string.IsNullOrWhiteSpace(t0) ? null : t0,
                t1 => $"{t1}{unit}",
                t2 => $"{t2}{unit}"
            );
        }

        public static int ToInt32(this StringNumber? stringNumber, int @default = 0)
        {
            return stringNumber == null ? @default : stringNumber.ToInt32();
        }
    }
}
