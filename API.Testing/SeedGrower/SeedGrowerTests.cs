using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.SeedGrower;

[TestFixture]
public class SeedGrowerTests : TestBase
{
    [Test(Description = "GET all growers")]
    public async Task Test_GetGrowers()
    {
        var response = await Request.GetAsync("./api/grower/v1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET grower Id")]
    public async Task Test_GetGrowerId()
    {
        var response = await Request.GetAsync("./api/grower/v1/36");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "DELETE grower ID")]
    public async Task Test_DeleteGrowerId()
    {
        var response = await Request.DeleteAsync("./api/grower/v1/58");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET grower name exists")]
    public async Task Test_GetGrowerNameExists()
    {
        var response = await Request.GetAsync("./api/grower/v1/name/Amazon/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
        Assert.AreEqual("True", data.Value.ToString());
    }

    [Test(Description = "GET grower code exists")]

     public async Task Test_GetGrowerCodeExists()
        {
            var response = await Request.GetAsync("./api/grower/v1/code/AZZ/exists");
            Assert.True(response.Ok);

            var data = await response.JsonAsync();
            Console.WriteLine("Data: " + data);
            Assert.AreEqual("True", data.Value.ToString());
        }

    [Test(Description = "GET grower autocomplete")]

    public async Task Test_GetGrowerAutocomplete()
    {
        var response = await Request.GetAsync("./api/grower/v1/autocomplete");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET grower CY Code ProductId autocomplete")]

    public async Task Test_GetGrowerCYCodeProductIdAutocomplete()
    {
        var response = await Request.GetAsync("./api/grower/v1/2024/AZZ/1/autocomplete");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }
}