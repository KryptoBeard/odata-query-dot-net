namespace OData.Query.Dot.Net.Types
{
    public class Guid : ODataValue
    {
        public object Value { get; set; }
        public Guid(object value) { Type = "guid"; Value = value; }
    }
}
