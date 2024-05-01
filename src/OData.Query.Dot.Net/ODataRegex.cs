using System.Text.RegularExpressions;

namespace OData.Query.Dot.Net
{
    public static class ODataRegex
    {
        public static readonly Regex FunctionRegex = new Regex(@"\((.*)\)");
        public static readonly Regex IndexOfRegex = new Regex(@"(?<!indexof)\((\w+)\)");
    }
}
