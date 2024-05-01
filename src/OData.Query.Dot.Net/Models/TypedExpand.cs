using System;
using System.Linq.Expressions;

namespace OData.Query.Dot.Net.Models
{
    public class TypedExpand<T> : PlainObject
    {
        public TypedExpand()
        {

        }
        public TypedExpand(Expression<Func<T, object>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, new Expand());
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, new Expand());
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }
        public TypedExpand(Expression<Func<T, object>> propertyExpression, Expand expand)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                this.Add(memberExpression.Member.Name, expand);
            }
            else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                this.Add(unaryMemberExpression.Member.Name, expand);
            }
            else
            {
                throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
            }
        }

        public TypedExpand<T> AddExpand(Expression<Func<T, object>> propertyExpression, Expand? nestedExpand = null)
        {
            string propertyName = Utilities.GetPropertyName(propertyExpression);

            nestedExpand ??= new Expand();

            // Add the expand to the dictionary
            this.Add(propertyName, nestedExpand);

            return this;
        }

        //public TypedExpand<T> AddExpand(Expression<Func<T, object>> propertyExpression, Expand nestedExpands = null)
        //{
        //    if (propertyExpression.Body is MemberExpression memberExpression)
        //    {
        //        this.Add(memberExpression.Member.Name, new Expand());
        //    }
        //    else if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        //    {
        //        this.Add(unaryMemberExpression.Member.Name, new Expand());
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Expression must be a member expression", nameof(propertyExpression));
        //    }



        //    return this;
        //}

    }
}
