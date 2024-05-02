using OData.Query.Dot.Net.Enums;
using OData.Query.Dot.Net.Models;

namespace OData.Query.Dot.Net.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var myFilter = new TypedFilter<SamplePerson>(x => x.FirstName, BooleanFunctions.startswith, "HI")
            .AddFilter(x => x.PhonNumber, ComparisonOperator.eq, "123")
            .Or(new OrExpression<SamplePerson>
            {
                Expression = (f => f.LastName),
                Operation = ComparisonOperator.eq,
                Value = "Doe"
            });

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

    [Fact]
    public void Test2()
    {

    }
}