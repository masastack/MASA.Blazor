using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;

namespace Masa.Docs.Indexing
{
    internal static class IndexHelper
    {

        internal static void AssertNotNullOrEmpty(this object? arg, string paramName)
        {
            switch (arg)
            {
                case string strValue when string.IsNullOrEmpty(strValue):
                case string s when string.IsNullOrWhiteSpace(s):
                case ICollection { Count: 0 }:
                case Array { Length: 0 }:
                case IEnumerable e when !e.GetEnumerator().MoveNext():
                case null:
                    throw new ArgumentNullException(paramName, "value is null or empty, please take a check.");
            }
        }

        internal static string CharCodeAt(this string character)
        {
            string coding = "";
            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1));
                //Fetching binary encoded content  
                string lowCode = Convert.ToString(bytes[1], 16);
                if (lowCode.Length == 1)
                {
                    lowCode = "0" + lowCode;
                }

                if (lowCode == "00")
                {
                    lowCode = "";
                }
                string highCode = Convert.ToString(bytes[0], 16);
                if (highCode.Length == 1)
                {
                    highCode = "0" + highCode;
                }
                if (highCode == "00")
                {
                    highCode = "";
                }
                coding += (lowCode + highCode);
            }
            return coding;
        }

        internal static string ToHashAnchor(this string str)
        {
            var slug = str.Trim().ToLower();
            slug = Regex.Replace(slug, @"[\s,.[\]{}()/]+", "-");
            slug = Regex.Replace(slug, @"[^a-z0-9 -]", m => m.Value.CharCodeAt());
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
