using OData.Query.Dot.Net.Enums;

namespace OData.Query.Dot.Net.Models
{
    public class Filter : PlainObject
    {
        public Filter()
        {

        }

        public Filter(string key)
        {
            this.Add(key, new Filter());
        }

        public Filter(string initialKey, object initialValue)
        {
            this.Add(initialKey, initialValue);
        }
        public Filter(ComparisonOperator op, object initialValue)
        {
            this.Add(op.ToString(), initialValue);
        }
        public Filter(BooleanFunctions op, object initialValue)
        {
            this.Add(op.ToString(), initialValue);
        }
    }
}
