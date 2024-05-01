namespace OData.Query.Dot.Net.Types
{
    public class Alias : ODataValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Alias(string name, object value) { Type = "alias"; Name = name; Value = value; }
    }
}
