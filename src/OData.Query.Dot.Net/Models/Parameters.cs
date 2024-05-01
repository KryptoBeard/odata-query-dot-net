namespace OData.Query.Dot.Net.Models
{
    public class Parameters
    {
        public Parameters()
        {

        }
        public bool Count { get; set; } = false;

        public int? Top { get; set; } = null;

        public int? Skip { get; set; } = null;

        public string? Filter { get; set; } = string.Empty;

        public string? Select { get; set; } = string.Empty;

        public string? OrderBy { get; set; } = string.Empty;

        public string? Expand { get; set; } = string.Empty;
    }
}
