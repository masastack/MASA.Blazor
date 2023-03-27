using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;

namespace Masa.Docs
{
    internal static class IndexHelper
    {
        internal static void AssertParamNotNull(this object? arg, string paramName)
        {
            if (arg is null)
            {
                throw new ArgumentNullException(paramName, "Argument is null, please check config.");
            }
        }

        internal static string CharCodeAt(this string character, int index)
        {
            return (character[index] + "").CharCodeAt();
        }

        internal static string CharCodeAt(this string character)
        {
            string coding = "";
            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1));
                //Fetching binary encoded content  
                string lowCode = System.Convert.ToString(bytes[1], 16);
                if (lowCode.Length == 1)
                {
                    lowCode = "0" + lowCode;
                }
                string hightCode = System.Convert.ToString(bytes[0], 16);
                if (hightCode.Length == 1)
                {
                    hightCode = "0" + hightCode;
                }
                coding += (lowCode + hightCode);
            }
            return coding;
        }

        internal static string? HashToAnchorString(this string str)
        {
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

        internal static void SetPropertyValue<T, TProperty>(this T t, Expression<Func<T, TProperty?>> selector, TProperty? newValue)
        {
            var valueType = typeof(TProperty);
            var param_val = Expression.Parameter(typeof(T));
            var valueExpress = Expression.Constant(newValue, valueType);
            if (selector.Body is MemberExpression memberExpression)
            {
                memberExpression = Expression.Property(Expression.Constant(t), memberExpression.Member.Name);
                var assignExpression = Expression.Assign(memberExpression, valueExpress);
                var lambda =
                   Expression.Lambda<Func<TProperty>>(assignExpression);
                lambda.Compile()();
            }
            throw new ArgumentOutOfRangeException(nameof(selector), "only support member expression");
        }

        internal static void SetPropertyValue<T, TProperty>(this T t, string propertyOrFieldName, TProperty? newValue)
        {
            var valueType = typeof(TProperty);
            var valueExpress = Expression.Constant(newValue, valueType);
            MemberExpression member = Expression.Property(Expression.Constant(t), propertyOrFieldName);
            var assignExpression = Expression.Assign(member, valueExpress);
            var lambda =
               Expression.Lambda<Func<TProperty>>(assignExpression);
            lambda.Compile()();
        }
    }
}
