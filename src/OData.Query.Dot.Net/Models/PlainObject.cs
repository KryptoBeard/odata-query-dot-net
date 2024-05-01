using System.Collections.Generic;

namespace OData.Query.Dot.Net.Models
{
    public class PlainObject : Dictionary<string, object>
    {
        public string LatestKey { get; set; } = string.Empty;
        public PlainObject()
        {

        }

        public PlainObject(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }

        public new void Add(string key, object value)
        {
            if (this.ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }
    }
}
