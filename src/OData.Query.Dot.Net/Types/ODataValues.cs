namespace OData.Query.Dot.Net.Types
{
    public static class ODataValues
    {
        public static Raw CreateRaw(string value) => new Raw(value);
        public static Guid CreateGuid(string value) => new Guid(value);
        public static Duration CreateDuration(string value) => new Duration(value);
        public static Binary CreateBinary(string value) => new Binary(value);
        public static Json CreateJson(object value) => new Json(value);
        public static Alias CreateAlias(string name, object value) => new Alias(name, value);
        public static Decimal CreateDecimal(string value) => new Decimal(value);
    }
}
