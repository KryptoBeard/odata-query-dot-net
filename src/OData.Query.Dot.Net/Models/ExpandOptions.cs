using System.Collections.Generic;

namespace OData.Query.Dot.Net.Models
{
    public class ExpandOptions<T> : Dictionary<string, object> { }


    //public delegate IQueryable<TResult> Select<T, TResult>(IQueryable<T> source, Expression<Func<T, TResult>> selector);
    //public delegate IQueryable<T> Filter<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate);
    //public delegate IQueryable<T> OrderBy<T>(IQueryable<T> source, Expression<Func<T, object>> keySelector, bool ascending = true);
    //public delegate IQueryable<T> Expand<T>(IQueryable<T> source, string navigationPropertyPath);

}
