namespace OData.Query.Dot.Net.Models
{
    public class ExpandObject : PlainObject
    {
        public ExpandObject()
        {

        }
        public ExpandObject(string key)
        {
            this.Add(key, new ExpandObject());
        }
        public ExpandObject(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }

    }
}
