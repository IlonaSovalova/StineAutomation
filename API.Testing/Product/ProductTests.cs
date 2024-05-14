using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Testing.Product;

[TestFixture]
public class ProductTests: TestBase
{
    [Test(Description = "GET all products")]
    public async Task Test_GetProducts()
    {
        var response = await Request.GetAsync("./api/product/v1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET one product")]
    public async Task Test_GetOneProduct()
    {
        var response = await Request.GetAsync("./api/product/v1/1");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "Code exists for product")]
    public async Task Test_CodeExistsOneProduct()
    {
        var code = "39EC22";
        var response = await Request.GetAsync("./api/product/v1/code/" + code + "/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
        Assert.AreEqual("True", data.Value.ToString());
    }
    [Test(Description = "Code does not exist for product")]
        public async Task Test_CodeNotExistsOneProduct()
        {
            var wrongcode = "40000";
        var response = await Request.GetAsync("./api/product/v1/code/" + wrongcode + "/exists");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
        Assert.AreEqual("False", data.Value.ToString());

    }

    [Test(Description = "GET Description")]
    public async Task Test_Description()
    {
        var description = "Soy";
        var response = await Request.GetAsync("./api/product/v1/description/"+description+"");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "GET Conditioners Autocomplete")]
    public async Task Test_ConditionerAutocomplete()
    {
        var response = await Request.GetAsync("./api/product/v1/conditioners/autocomplete");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "POST Advanced Search")]
    public async Task Test_AdvancedSearch()
    {
        // query args (params)
        var args = new Dictionary<string, object>(){
            {"companyYearId", "93"}
        };
        var data = new Dictionary<string, object> {
            { "criteria", new string[0] },
            { "queryParams", new Dictionary<string, object>{
                { "filter", "" },
                { "sort", "active" },
                { "pageIndex", 0 },
                { "pageSize", 10 },
                { "descending", false },
                { "expand", true }
            }}
        };

        var response = await Request.PostAsync("./api/product/v1/advancedSearch", new() { Params= args, DataObject=data});
        if (!response.Ok) { Console.WriteLine(response); }
        Assert.True(response.Ok);
    }


    [Test(Description = "GET Product ordered-shipped")]
    public async Task Test_GetOrderedShipped()
    {
        var response = await Request.GetAsync("./api/product/v1/oredred-shipped");
        // not implemented yet
        Assert.Fail();
    }

    [Test(Description = "GET Product Yearly Totals")]
    public async Task Test_GetYearlyTotals()
    {
        var productId = "1";
        var year = "2024";
        var response = await Request.GetAsync("./api/product/v1/"+productId+"/yearly-totals/"+year+"");
        Assert.True(response.Ok);

        dynamic responseData = await GetJson(response);
        int companyYearId = responseData.companyYearId;
        int productId1 = responseData.productId;
        double orderedQuantity = responseData.orderedQuantity;
        string providerName = responseData.acresTotals[0].providerName;
        double currentYearEstimateAcres = responseData.acresTotals[0].currentYearEstimateAcres;
        double budgetedYearEstimateAcres = responseData.acresTotals[0].budgetedYearEstimateAcres;
        Assert.AreEqual(273, orderedQuantity);
        Assert.AreEqual("AgHub Genetics", providerName);
        Assert.AreEqual(800, budgetedYearEstimateAcres);
        Assert.AreEqual(4500, currentYearEstimateAcres);
    }

    [Test(Description = "GET Preferred Name")]
    public async Task Test_GetPreferredName()
    {
        var response = await Request.GetAsync("./api/product/v1/preferredName");
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "Create a new product")]
    public async Task Test_CreateNewProduct()
    {
        // get body from file
        var payload = GetJsonData("Product\\DataProducts.json");
        var response = await Request.PostAsync("./api/product/v1", new() {DataObject = payload });
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }

    [Test(Description = "Modify a product")]
    public async Task Test_ModifyProduct()
    {
        // get body from file
        var payload = GetJsonData("Product\\DataProducts.json");
        var response = await Request.PutAsync("./api/product/v1", new() { DataObject = payload });
        Assert.True(response.Ok);

        var data = await response.JsonAsync();
        Console.WriteLine("Data: " + data);
    }
}
