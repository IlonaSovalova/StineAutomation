using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.Conditioner;

[TestFixture]
public class ConditionerTests : TestBase
{

    [Test(Description = "GET conditioners")]
    public async Task Test_GetConditioners()
    {
        var response = await Request.GetAsync("./api/conditioner/v1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET active conditioner CY")]
    public async Task Test_GetActiveConditionerCY()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/active/2025");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET all conditioners")]
    public async Task Test_GetAllConditioners()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/all");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET conditioner ID")]
    public async Task Test_GetConditionerId()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/241");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "DELETE conditioner ID")]
    public async Task Test_DeleteConditionerId()
    {
        var response = await Request.DeleteAsync("./api/conditioner/v1/241");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET conditioner ID contacts")]
    public async Task Test_GetConditionerIdContacts()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/204/contacts");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET conditioner code exists")]
    public async Task Test_GetConditionerCodeExists()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/code/204/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
        Assert.AreEqual("True", data.Value.ToString());
    }

    [Test(Description = "GET conditioner name exists")]
    public async Task Test_GetConditionerNameExists()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/code/Aaron1/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
        Assert.AreEqual("True", data.Value.ToString());
    }

    [Test(Description = "Max Volume Report")]
    public async Task Test_GetMaxVolumeReport()
    {
        var response = await Request.GetAsync("./api/conditioner/v1/max-volume-report");
        Assert.True(response.Ok);

        var data = await response.TextAsync();
        Console.WriteLine("Data: " + data);
    }
}
