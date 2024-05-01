namespace OData.Query.Dot.Net.Types
{
    public class Decimal : ODataValue
    {
        public object Value { get; set; }
        public Decimal(object value) { Type = "decimal"; Value = value; }
    }
}
