using System;
using System.Linq.Expressions;

namespace OData.Query.Dot.Net
{
    public class Utilities
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                return unaryMemberExpression.Member.Name;
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }
    }
}
