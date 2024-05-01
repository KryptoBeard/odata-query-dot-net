namespace OData.Query.Dot.Net
{
    public static class ODataConstants
    {
        public static readonly string[] ComparisonOperators = new[] { "eq", "ne", "gt", "ge", "lt", "le" };
        public static readonly string[] LogicalOperators = new[] { "and", "or", "not" };
        public static readonly string[] CollectionOperators = new[] { "any", "all" };
        public static readonly string[] BooleanFunctions = new[] { "startswith", "endswith", "contains" };
        public static readonly string[] SupportedExpandProperties = new[] { "expand", "levels", "select", "skip", "top", "count", "orderby", "filter" };
    }
}
