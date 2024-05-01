using OData.Query.Dot.Net.Enums;
using OData.Query.Dot.Net.Models;

namespace OData.Query.Dot.Net.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var myFilter = new FilterObject
        {
            {nameof(SamplePerson.FirstName), new FilterObject(ComparisonOperator.eq, "John")}
        };

        var myExpands = new ExpandObject
        {
            {"Addresses", new ExpandObject
                {
                    {"expand", new ExpandObject("State", new ExpandObject("expand",new ExpandObject("County"))) }
                }
            }
        };


        var result = ODataQueryBuilder.BuildQuery<SamplePerson>(myFilter, myExpands);
    }
}