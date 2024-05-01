using OData.Query.Dot.Net.Enums;

namespace OData.Query.Dot.Net.Models
{
    public class FilterObject : PlainObject
    {
        //public FilterObject(Expression<Func<T, object>> propertyExpression, object initialValue)
        //{
        //    if (propertyExpression.Body is MemberExpression memberExpression)
        //    {
        //        this.Add(memberExpression.Member.Name, initialValue);
        //    }
        //    else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        //    {
        //        this.Add(unaryMemberExpression.Member.Name, initialValue);
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
        //    }
        //}
        public FilterObject()
        {

        }

        public FilterObject(string key)
        {
            this.Add(key, new FilterObject());
        }

        public FilterObject(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }
        public FilterObject(ComparisonOperator op, object initialValue)
        {
            this.Add(op.ToString(), initialValue);
        }
    }
}
