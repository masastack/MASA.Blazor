namespace Masa.Blazor.Extensions
{
    public static class StringEnumExtensions
    {
        public static string ToString<TEnum>(this StringEnum<TEnum> stringEnum, Func<string> firstFunc,
            params (string name, StringEnum<TEnum> @enum)[] list) where TEnum : Enum
        {
            if (stringEnum is not null)
            {
                if (stringEnum.IsT0)
                {
                    return firstFunc.Invoke();
                }

                var item = list.FirstOrDefault(x => x.@enum.Equals(stringEnum));
                if (!item.Equals(default))
                {
                    return item.name;
                }
            }

            return "";
        }
    }
}