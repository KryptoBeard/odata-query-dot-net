namespace OData.Query.Dot.Net.Models
{
    public class Expand : PlainObject
    {
        public Expand()
        {

        }
        public Expand(string key)
        {
            this.Add(key, new Expand());
        }
        public Expand(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }

    }
}
