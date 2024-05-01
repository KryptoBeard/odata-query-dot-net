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
        public TypedFilter<T> AddFilter(LogicalOperator logicalOperator, Expression<Func<T, object>> propertyExpression, ComparisonOperator op, object value)
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
        public TypedFilter<T> AddFilter(LogicalOperator logicalOperator, Expression<Func<T, object>> propertyExpression, BooleanFunctions op, ODataValue value)
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
}
