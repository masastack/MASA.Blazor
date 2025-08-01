using System.Linq.Expressions;

namespace Masa.Blazor
{
    public class ItemValue<TItem>(string? name, Func<TItem, object?>? valueExpression = null)
    {
        public string? Name { get; } = name;

        public Func<TItem, object?>? Factory { get; } = valueExpression ?? GetFactory(name);

        public object? Invoke(TItem item) => Factory?.Invoke(item);

        private static Func<TItem, object?>? GetFactory(string? name)
        {
            if (name is null)
            {
                return null;
            }

            try
            {
                var parameterExpression = Expression.Parameter(typeof(TItem), "item");
                var propertyExpression = Expression.Property(parameterExpression, name);
                var valueExpression = Expression.Convert(propertyExpression, typeof(object));

                var lambdaExpression = Expression.Lambda<Func<TItem, object>>(valueExpression, parameterExpression);
                return lambdaExpression.Compile();
            }
            catch (Exception)
            {
                throw new InvalidOperationException(
                    $"""
                     The property '{name}' does not exist on type '{typeof(TItem).FullName}'."
                     Please ensure that the property name is correct and that it exists in the type '{typeof(TItem).FullName}'.
                     If you are using a nested property, please use {nameof(DataTableHeader<TItem>.ValueExpression)} to specify the property accessor.
                     """
                );
            }
        }

        public static implicit operator string?(ItemValue<TItem> itemValue) => itemValue.Name;

        public static implicit operator ItemValue<TItem>(string name) => new(name);
    }
}