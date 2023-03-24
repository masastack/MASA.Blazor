﻿using System.Linq.Expressions;
using System.Reflection;
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
            var m = selector.Compile()(t);
            var valueType = typeof(TProperty);
            var valueExpress = Expression.Constant(newValue, valueType);
            if (selector.Body is MemberExpression memberExpression)
            {
                var assignExpression = Expression.Assign(memberExpression, valueExpress);
                var lambda =
                   Expression.Lambda<Func<T, TProperty>>(assignExpression, selector.Parameters);
                lambda.Compile()(t);
            }
        }

        internal static void SetPropertyValue<T>(this T t, string name, object value)
        {
            Type type = t!.GetType();
            if (type == null)
            {
                return;
            }
            PropertyInfo? p = type?.GetProperty(name);
            if (p == null)
            {
                throw new ArgumentException(name);
            }
            var param_obj = Expression.Parameter(type!);
            var param_val = Expression.Parameter(typeof(object));
            var body_obj = Expression.Convert(param_obj, type!);
            var body_val = Expression.Convert(param_val, p.PropertyType);
            var setMethod = p.GetSetMethod(true);
            if (setMethod != null)
            {
                var body = Expression.Call(param_obj, setMethod, body_val);
                var setValue = Expression.Lambda<Action<T, object>>(body, param_obj, param_val).Compile();
                setValue(t, value);
            }
        }
    }
}
