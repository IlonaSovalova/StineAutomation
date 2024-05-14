#pragma warning disable NUnit2005 // Consider using Assert.That(actual, Is.EqualTo(expected)) instead of Assert.AreEqual(expected, actual)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace API.Testing;

public class TestBase : PlaywrightTest
{
    // test environment
    readonly string BaseURL = "https://devstineportalapi.stineseed.com";
    readonly string UserName = "admin@stineseed.com";
    readonly string Password = "Admin@12345";

    //
    string Token;
    protected IAPIRequestContext Request;

    [SetUp]
    public async Task Setup()
    {
        this.Token = await GetToken(UserName, Password);
        await CreateAPIRequestContext();
    }

    private async Task<string> GetToken(string username, string password)
    {
        // fetch Token
        var headers = new Dictionary<string, string>(){
            {"Accept", "application/json"}
        };
        IAPIRequestContext request = await this.Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = this.BaseURL,
            ExtraHTTPHeaders = headers,
        });

        var data = new Dictionary<string, object> {
            { "email", username },
            { "password", password },
        };
        var response = await request.PostAsync("./api/user/v1/login", new() { DataObject = data });
        var token = await response.JsonAsync();
        if( token.HasValue ) {
            return token.Value.GetProperty("token").ToString();
        }
        else { return string.Empty; }
    }

    private async Task CreateAPIRequestContext()
    {
        var headers = new Dictionary<string, string>(){
            {"Accept", "application/json"},
            {"Authorization", "Bearer " + this.Token},
        };
        //Console.WriteLine($"Token: {Token}");

        Request = await this.Playwright.APIRequest.NewContextAsync(new()
        {
            // All requests we send go to this API endpoint.
            BaseURL = this.BaseURL,
            ExtraHTTPHeaders = headers,
        });
    }

    [TearDown]
    public async Task TearDownAPITesting()
    {
        if (Request != null)
        {
            await Request.DisposeAsync();
        }
    }

    /// <summary>
    /// Returns json file as ByteArrayContent suitable for POST/PUT requests.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>Json as ByteArrayContent</returns>
    public static ByteArrayContent GetJsonData(string fileName)
    {
        var bytes = System.IO.File.ReadAllBytes(fileName);
        var byteContent = new ByteArrayContent(bytes);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return byteContent;
    }

    /// <summary>
    /// Reads JSON object from a file.
    /// </summary>
    /// <param name="fileName">File name.</param>
    /// <returns>JSON object.</returns>
    public static dynamic ReadJsonObject(string fileName)
    {
        dynamic jsonObject = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(fileName));
        return jsonObject;
    }

    /// <summary>
    /// Extracts and returns json object from given response.
    /// </summary>
    /// <param name="response">Received response.</param>
    /// <returns>JSON object.</returns>
    public static async Task<dynamic> GetJson(IAPIResponse response)
    {
        // ensure json
        bool isJson = response.Headers.TryGetValue("content-type", out string? contentType);
        Assert.True(isJson);
        Assert.True(contentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase));
        //
        var text = await response.TextAsync();
        return JsonConvert.DeserializeObject(text);
    }
}
