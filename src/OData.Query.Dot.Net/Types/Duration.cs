namespace OData.Query.Dot.Net.Types
{
    public class Duration : ODataValue
    {
        public object Value { get; set; }
        public Duration(object value) { Type = "duration"; Value = value; }
    }
}
