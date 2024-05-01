using System.Collections.Generic;

namespace OData.Query.Dot.Net.Models
{
    public class PlainObject : Dictionary<string, object>
    {
        public PlainObject()
        {

        }

        public PlainObject(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }
    }
}
