namespace System;

public static class StringExtensions
{
    public static string? ToUpperFirst(this string? str)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length == 1)
        {
            return str.ToUpper();
        }

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
