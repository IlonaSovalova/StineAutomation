using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.CY;

[TestFixture]
public class CYTests : TestBase
{
    [Test(Description = "GET a CY")]
    public async Task Test_GetCY()
    {
        var response = await Request.GetAsync("./api/company-year/v1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET a CY Id")]
    public async Task Test_GetCYId()
    {
        var response = await Request.GetAsync("./api/company-year/v1/66");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET a CY Last Year")]
    public async Task Test_GetCYLastYear()
    {
        var response = await Request.GetAsync("./api/company-year/v1/GetLastYear");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET a CY Previous Last Year")]
    public async Task Test_GetCYPreviousLastYear()
    {
        var response = await Request.GetAsync("./api/company-year/v1/getPreviousLastCompanyYear");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET a CY Next Year")]
    public async Task Test_GetCYNextYear()
    {
        var response = await Request.GetAsync("./api/company-year/v1/2024/NextYear");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET a CY Autocomplete")]
    public async Task Test_GetCYAutocompete()
    {
        var response = await Request.GetAsync("./api/company-year/v1/autocomplete");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }
}