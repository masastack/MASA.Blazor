using System.Linq.Expressions;
using System.Reflection;

namespace BlazorComponent
{
    internal class MemberAccessVisitor : ExpressionVisitor
    {
        public List<PropertyInfo> PropertyInfos { get; } = new();

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member is PropertyInfo propertyInfo)
            {
                if (!PropertyInfos.Any(prop => prop.PropertyType == propertyInfo.PropertyType && prop.Name == propertyInfo.Name))
                {
                    PropertyInfos.Add(propertyInfo);
                }
            }

            return base.VisitMember(node);
        }
    }
}
