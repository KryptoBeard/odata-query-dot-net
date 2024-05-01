using OData.Query.Dot.Net.Enums;
using OData.Query.Dot.Net.Models;
using OData.Query.Dot.Net.Types;

namespace OData.Query.Dot.Net.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var myFilter = new TypedFilter<SamplePerson>(x => x.FirstName, BooleanFunctions.startswith, new Raw("Hi"))
            .AddFilter(LogicalOperator.or, x => x.LastName, ComparisonOperator.eq, new Raw("Hello"));

        var myExpands = new TypedExpand<SamplePerson>()
            .AddExpand(f => f.Addresses, new Expand() { { "expand", new Expand() { { "State", new Expand() }, { "County", new Expand() } } } });



        //var myExpands = new Expand
        //{
        //    {"Addresses", new Expand
        //        {
        //            {"expand", new Expand("State", new Expand("expand",new Expand("County"))) }
        //        }
        //    }
        //};


        var result = ODataQueryBuilder.BuildQuery<SamplePerson>(myFilter, myExpands);
    }
}