namespace OData.Query.Dot.Net.Types
{
    public class Raw : ODataValue
    {
        public object Value { get; set; }
        public Raw(object value) { Type = "raw"; Value = value; }
    }
}
