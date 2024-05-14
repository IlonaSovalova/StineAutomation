using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.Traits;

[TestFixture]
public class TraitsTests: TestBase
{
    [Test(Description = "GET all traits")]
    public async Task Test_GetTraits()
    {
        var response = await Request.GetAsync("./api/traits/v1/list");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET trait by ID")]
    public async Task Test_GetTraitId()
    {
        var response = await Request.GetAsync("./api/traits/v1/2");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }


    [Test(Description = "GET trait Autocomplete")]
    public async Task Test_GetTraitAutocomplete()
    {
        var response = await Request.GetAsync("./api/traits/v1/autocomplete");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "Create a new trait")]
    public async Task Test_PostTrait()
    {
        // get body from file
        var payload = GetJsonData("Traits\\DataTraits.json");
        var response = await Request.PostAsync("./api/traits/v1", new() { DataObject = payload });
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }


}




