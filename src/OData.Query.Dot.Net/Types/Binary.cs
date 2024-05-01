namespace OData.Query.Dot.Net.Types
{
    public class Binary : ODataValue
    {
        public object Value { get; set; }
        public Binary(object value) { Type = "binary"; Value = value; }
    }
}
