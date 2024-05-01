using System.Collections.Generic;

namespace OData.Query.Dot.Net.Interfaces
{
    public interface IPlainObject
    {
        Dictionary<string, object> Properties { get; }
    }

}
