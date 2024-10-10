using System.Linq.Expressions;

namespace Masa.Blazor.MasaTable.FilterDialogs;

public record FilterExpressionMeta<TItem>(
    Func<TItem, object> PropertyAccessor,
    string Func,
    string Expected,
    ColumnType ColumnType,
    string ColumnId);

public class FilterExpressionGenerator<TItem> : ExpressionHelper<TItem>
{
    public Func<TItem, bool>? GetWhereExpression(
        string operatorType,
        IEnumerable<FilterExpressionMeta<TItem>> filters
    )
    {
        var expressions = new List<Expression>();

        foreach (var (propertyAccessor, func, expected, expectedType, key) in filters)
        {
            var type = expectedType switch
            {
                ColumnType.Number => typeof(decimal),
                ColumnType.Date => typeof(DateTime),
                _ => typeof(string)
            };

            expressions.Add(GetExpression2(expected, func, propertyAccessor, key, type));
        }

        if (expressions.Count == 0)
        {
            return null;
        }

        var combinedExpression = operatorType == "and"
            ? expressions.Aggregate(Expression.AndAlso)
            : expressions.Aggregate(Expression.OrElse);

        return Expression.Lambda<Func<TItem, bool>>(combinedExpression, ParameterExpression).Compile();
    }
}