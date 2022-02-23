namespace Masa.Blazor.Helpers
{
    public static class ColorHelper
    {
        public static (string @class, string @style) ToCss(string color)
        {
            var @class = string.Empty;
            var @style = string.Empty;
            if (!string.IsNullOrWhiteSpace(color))
            {
                var color_variant = color.Split(" ");
                var c = color_variant[0];
                if (!string.IsNullOrWhiteSpace(c))
                {
                    if (c.StartsWith("#"))
                    {
                        style += $"color: {c}";
                    }
                    else
                    {
                        @class += $"{c}--text";
                    }

                    if (color_variant.Length == 2)
                    {
                        var v = color_variant[1];
                        // TODO: 是否需要正则表达式验证格式
                        // {darken|lighten|accent}-{1|2}

                        if (!string.IsNullOrWhiteSpace(v))
                        {
                            @class += $" text--{v}";
                        }
                    }
                }
            }

            return (@class, @style);
        }
    }
}
