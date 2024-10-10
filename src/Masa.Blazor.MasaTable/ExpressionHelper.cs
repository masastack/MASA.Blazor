using System.Reflection;

namespace Masa.Blazor.MasaTable;

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

public class ExpressionHelper<TItem>
{
    private readonly MethodInfo? _startsWithMethod;
    private readonly MethodInfo? _endsWithMethod;
    private readonly MethodInfo? _containsMethod;
    private readonly MethodInfo? _emptyMethod;

    private readonly ConcurrentDictionary<string, Expression> _propertySelectors;

    protected ParameterExpression ParameterExpression { get; init; }

    public ExpressionHelper()
    {
        _propertySelectors = new ConcurrentDictionary<string, Expression>();
        _startsWithMethod = typeof(string).GetMethod("StartsWith", [typeof(string)]);
        _endsWithMethod = typeof(string).GetMethod("EndsWith", [typeof(string)]);
        _containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
        _emptyMethod = typeof(string).GetMethod("IsNullOrEmpty", [typeof(string)]);

        ParameterExpression = Expression.Parameter(typeof(TItem), "u");
    }

    protected Expression GetExpression2(string expected, string func, Func<TItem, object> propertySelector,
        string key, Type type)
    {
        Expression expectedExpression;

        if (type == typeof(decimal))
        {
            expectedExpression = Expression.Constant(Convert.ToDecimal(expected));
        }
        else
        {
            expectedExpression = Expression.Constant(expected);
        }

        return func switch
        {
            "StartsWith" => CreateStartsWithExpression(propertySelector, expected, key),
            "NotStartsWith" => Expression.Not(CreateStartsWithExpression(propertySelector, expected, key)),
            "EndsWith" => CreateEndsWithExpression(propertySelector, expected, key),
            "NotEndsWith" => Expression.Not(CreateEndsWithExpression(propertySelector, expected, key)),
            "Contains" => Call(propertySelector, expected, key, _containsMethod!),
            "NotContains" => Expression.Not(Call(propertySelector, expected, key, _containsMethod!)),
            "Equal" => Expression.Equal(GetProperty(propertySelector, key), expectedExpression),
            "NotEqual" => Expression.NotEqual(GetProperty(propertySelector, key), expectedExpression),
            "Empty" => Expression.Call(_emptyMethod, GetProperty(propertySelector, key)),
            "NotEmpty" => Expression.Not(Expression.Call(_emptyMethod, GetProperty(propertySelector, key))),
            _ => throw new NotSupportedException($"The function {func} is not supported.")
        };
    }

    private Expression CreateStartsWithExpression(Func<TItem, object> propertySelector, string value,
        string key)
    {
        return Call(propertySelector, value, key, _startsWithMethod!);
    }

    private Expression CreateEndsWithExpression(Func<TItem, object> propertySelector, string value,
        string key)
        => Call(propertySelector, value, key, _endsWithMethod!);

    private Expression Call(Func<TItem, object> propertySelector, string value, string key,
        MethodInfo method, bool notNullRequired = true)
    {
        var property = GetProperty(propertySelector, key);
        var callExpression = Expression.Call(property, method, Expression.Constant(value));

        if (notNullRequired)
        {
            var nullCheck = Expression.NotEqual(property, Expression.Constant(null));
            var conditionalExpression = Expression.Condition(nullCheck, callExpression, Expression.Constant(false));
            return conditionalExpression;
        }

        return callExpression;
    }

    private MethodCallExpression Call(Func<TItem, object> propertyAccess, string key, MethodInfo method)
    {
        var invocationExpression = GetProperty(propertyAccess, key);
        return Expression.Call(invocationExpression, method);
    }

    private Expression GetProperty(Func<TItem, object> propertySelector, string key, Type? type = null)
    {
        var fullKey = typeof(TItem).FullName + "." + key;
        return _propertySelectors.GetOrAdd(fullKey, _ =>
        {
            var propertyAccess = Expression.Invoke(Expression.Constant(propertySelector), ParameterExpression);
            var stringExpression = Expression.Call(propertyAccess, typeof(object).GetMethod("ToString")!);

            if (type is not null)
            {
                return Expression.Call(type.GetMethod("Parse", [typeof(string)]), stringExpression);
            }

            return stringExpression;
        });
    }
}