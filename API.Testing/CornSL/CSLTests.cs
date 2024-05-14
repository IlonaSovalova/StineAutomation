using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.CSL;

[TestFixture]
public class CSLTests : TestBase
{
    [Test(Description = "GET all CSL")]
    public async Task Test_GetCSL()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET CY ID CSL")]
    public async Task Test_GetCSLCyId()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/year/2024/id/39");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET ID Assigment CSL")]
    public async Task Test_GetCSLIdAssigment()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/id/39/assignment");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET ID Autocomplete CSL")]
    public async Task Test_GetCSLIdAutocomplete()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/id/39/assignment");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET ID Year CSL")]
    public async Task Test_GetCSLIdYear()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/year/2024");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET ID Year Exists CSL")]
    public async Task Test_GetCSLYearExists()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/year/2023/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET ID Delete CSL")]
    public async Task Test_GetCSLIdDelete()
    {
        var response = await Request.GetAsync("./api/corn-shipment-location/v1/42");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }
}