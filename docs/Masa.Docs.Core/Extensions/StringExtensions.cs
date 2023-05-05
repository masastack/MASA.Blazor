namespace Masa.Docs.Core;

public static class StringExtensions
{
    public static string ToPascal(this string name)
    {
        name = name.Trim('-');
        return string.Join("", name.Split('-').Select(item => char.ToUpper(item[0]) + item.Substring(1)));
    }

    public static string ToKebab(this string name)
    {
        var split = GeneratedRegexes.SingleCamelCase().Split(name).Select(s => s.Trim('-'));
        return string.Join("-", split).ToLowerInvariant();
    }

    public static string CharCodeAt(this string character, int index)
    {
        return (character[index] + "").CharCodeAt();
    }

    public static string CharCodeAt(this string character)
    {
        string coding = "";
        for (int i = 0; i < character.Length; i++)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(character.Substring(i, 1));
            //取出二进制编码内容  
            string lowCode = Convert.ToString(bytes[1], 16);
            //取出低字节编码内容（两位16进制）  
            if (lowCode.Length == 1)
            {
                lowCode = "0" + lowCode;
            }
            string hightCode = Convert.ToString(bytes[0], 16);

            //取出高字节编码内容（两位16进制）  
            if (hightCode.Length == 1)
            {
                hightCode = "0" + hightCode;
            }
            coding += (lowCode + hightCode);
        }
        return coding;
    }

    public static string? HashToAnchorString(this string? str)
    {
        if (str == null) return null;
        var slug = str.Trim().ToLower();
        slug = Regex.Replace(slug, @"[\s,.[\]{}()/]+", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9 -]", delegate (Match m)
        {
            return m.Value.CharCodeAt();
        });
        slug = Regex.Replace(slug, @"-{2,}", "-");
        slug = Regex.Replace(slug, @"^-*|-*$", "");
        if (Regex.Match(slug[0].ToString(), @"[^a-z]").Success)
        {
            slug = "section-" + slug;
        }
        return HttpUtility.UrlEncode(slug);
    }
}
