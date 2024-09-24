using System.Reflection;

namespace Masa.Blazor.MasaTable;

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

public static class ExpressionHelper<TItem>
{
    private static readonly MethodInfo? _startsWithMethod;
    private static readonly MethodInfo? _endsWithMethod;
    private static readonly MethodInfo? _containsMethod;
    private static readonly MethodInfo? _emptyMethod;

    private static readonly ConcurrentDictionary<string, Expression> _propertySelectors;
    private static readonly ParameterExpression _parameterExpression = Expression.Parameter(typeof(TItem), "u");

    static ExpressionHelper()
    {
        _propertySelectors = new ConcurrentDictionary<string, Expression>();
        _startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        _endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        _containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        _emptyMethod = typeof(string).GetMethod("IsNullOrEmpty")!;
    }

    public static Func<TItem, bool> GetExpression(
        IEnumerable<(Func<TItem, object> propertySelector, string func, string expected, string key)> filters,
        string operatorType)
    {
        var expressions = new List<Expression>();

        foreach (var (propertySelector, func, expected, key) in filters)
        {
            switch (func)
            {
                case "StartsWith":
                    expressions.Add(CreateStartsWithExpression(propertySelector, expected, key));
                    break;
                case "NotStartsWith":
                    expressions.Add(Expression.Not(CreateStartsWithExpression(propertySelector, expected, key)));
                    break;
                case "EndsWith":
                    expressions.Add(CreateEndsWithExpression(propertySelector, expected, key));
                    break;
                case "NotEndsWith":
                    expressions.Add(Expression.Not(CreateEndsWithExpression(propertySelector, expected, key)));
                    break;
                case "Contains":
                    expressions.Add(Call(propertySelector, expected, key, _containsMethod!));
                    break;
                case "NotContains":
                    expressions.Add(Expression.Not(Call(propertySelector, expected, key, _containsMethod!)));
                    break;
                case "Empty":
                    expressions.Add(Call(propertySelector, key, _emptyMethod!));
                    break;
                case "NotEmpty":
                    expressions.Add(Expression.Not(Call(propertySelector, key, _emptyMethod!)));
                    break;
            }
        }

        var combinedExpression = operatorType == "and"
            ? expressions.Aggregate(Expression.AndAlso)
            : expressions.Aggregate(Expression.OrElse);

        return Expression.Lambda<Func<TItem, bool>>(combinedExpression, _parameterExpression).Compile();
    }

    private static MethodCallExpression CreateStartsWithExpression(Func<TItem, object> propertySelector, string value,
        string key)
        => Call(propertySelector, value, key, _startsWithMethod!);

    private static MethodCallExpression CreateEndsWithExpression(Func<TItem, object> propertySelector, string value,
        string key)
        => Call(propertySelector, value, key, _endsWithMethod!);

    private static Expression GetOrAddInvocationExpression(Func<TItem, object> propertySelector, string key)
    {
        var fullKey = typeof(TItem).FullName + "." + key;
        return _propertySelectors.GetOrAdd(fullKey, _ =>
        {
            var propertyAccess = Expression.Invoke(Expression.Constant(propertySelector), _parameterExpression);
            var asString = Expression.Convert(propertyAccess, typeof(string));
            return asString;
        });
    }

    private static MethodCallExpression Call(Func<TItem, object> propertySelector, string value, string key,
        MethodInfo method)
    {
        var invocationExpression = GetOrAddInvocationExpression(propertySelector, key);
        return Expression.Call(invocationExpression, method, Expression.Constant(value));
    }

    private static MethodCallExpression Call(Func<TItem, object> propertyAccess, string key, MethodInfo method)
    {
        var invocationExpression = GetOrAddInvocationExpression(propertyAccess, key);
        return Expression.Call(invocationExpression, method);
    }
}