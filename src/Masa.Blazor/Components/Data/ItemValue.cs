using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Masa.Blazor
{
    public class ItemValue<TItem>
    {
        public ItemValue(string? name = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        internal ItemValue(Func<TItem, object?> factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public string? Name { get; }

        [field: AllowNull, MaybeNull]
        public Func<TItem, object?> Factory
        {
            get
            {
                if (field != null) return field;

                try
                {
                    var parameterExpression = Expression.Parameter(typeof(TItem), "item");
                    var propertyExpression = Expression.Property(parameterExpression, Name);
                    var valueExpression = Expression.Convert(propertyExpression, typeof(object));

                    var lambdaExpression = Expression.Lambda<Func<TItem, object>>(valueExpression, parameterExpression);
                    field = lambdaExpression.Compile();
                }
                catch (Exception)
                {
                    throw new ArgumentException(
                        $"The property '{Name}' does not exist on type '{typeof(TItem).FullName}'.", nameof(Name));
                }

                return field;
            }
        }

        public object? Invoke(TItem item) => Factory.Invoke(item);

        public static implicit operator string(ItemValue<TItem> itemValue) => itemValue.Name;

        public static implicit operator ItemValue<TItem>(string name) => new(name);
    }
}