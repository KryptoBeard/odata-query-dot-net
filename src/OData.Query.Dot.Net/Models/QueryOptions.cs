using OData.Query.Dot.Net.Types;
using System.Collections.Generic;

namespace OData.Query.Dot.Net.Models
{
    public class QueryOptions<T> : ExpandOptions<T>
    {
        public string search { get; set; }
        public PlainObject transform { get; set; }
        public int skip { get; set; }
        public string skiptoken { get; set; }
        public object key { get; set; }
        public object count { get; set; }
        public string action { get; set; }
        public object func { get; set; }
        public string format { get; set; }
        public List<Alias> aliases { get; set; }
    }
}
