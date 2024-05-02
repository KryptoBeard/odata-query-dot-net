using OData.Query.Dot.Net.Enums;
using OData.Query.Dot.Net.Types;
using System;
using System.Linq.Expressions;

namespace OData.Query.Dot.Net.Models
{
    public class TypedFilter<T> : PlainObject
    {
        public TypedFilter(Expression<Func<T, object>> propertyExpression, ComparisonOperator op, ODataValue value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }
        public TypedFilter(Expression<Func<T, object>> propertyExpression, ComparisonOperator op, object value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }
        public TypedFilter(Expression<Func<T, object>> propertyExpression, BooleanFunctions op, ODataValue value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }
        public TypedFilter(Expression<Func<T, object>> propertyExpression, BooleanFunctions op, object value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }

        public TypedFilter<T> AddFilter(LogicalOperator logicalOperator, Expression<Func<T, object>> propertyExpression, ComparisonOperator op, ODataValue value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(logicalOperator.ToString(), new Filter(memberExpression.Member.Name, new Filter(op.ToString(), value)));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(logicalOperator.ToString(), new Filter(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value)));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }

            return this;
        }
        public TypedFilter<T> AddFilter(Expression<Func<T, object>> propertyExpression, ComparisonOperator op, object value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }

            return this;
        }


        public TypedFilter<T> Or(params OrExpression<T>[] ors)
        {
            var topOr = new Filter(string.Empty);
            var val = topOr[string.Empty] as Filter;
            foreach (var or in ors)
            {
                if (or.Expression.Body is MemberExpression memberExpression)
                {
                    val.Add(memberExpression.Member.Name, new Filter(or.Operation.ToString(), or.Value));
                }
                else if (or.Expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
                {
                    val.Add(unaryMemberExpression.Member.Name, new Filter(or.Operation.ToString(), or.Value));
                }
                else
                {
                    throw new ArgumentException("Expression must be a member expression", nameof(or.Expression));
                }
            }
            this.Add(LogicalOperator.or.ToString(), topOr);

            return this;
        }

        public TypedFilter<T> AddFilter(LogicalOperator logicalOperator, Expression<Func<T, object>> propertyExpression, BooleanFunctions op, object value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(logicalOperator.ToString(), new Filter(memberExpression.Member.Name, new Filter(op.ToString(), value)));
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(logicalOperator.ToString(), new Filter(unaryMemberExpression.Member.Name, new Filter(op.ToString(), value)));
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }

            return this;
        }
    }

    public class OrExpression<T>
    {
        public Expression<Func<T, object>> Expression { get; set; }
        public ComparisonOperator Operation { get; set; }
        public object Value { get; set; }
    }
}
