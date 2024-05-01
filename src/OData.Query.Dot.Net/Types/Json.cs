namespace OData.Query.Dot.Net.Types
{
    public class Json : ODataValue
    {
        public object Value { get; set; }
        public Json(object value) { Type = "json"; Value = value; }
    }
}
