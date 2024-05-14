
using Microsoft.Playwright;
using Newtonsoft.Json;
using UIAutomationFramwork.Utils;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonReader = UIAutomationFramwork.Utils.JsonReader;
using NUnit.Framework;

namespace UIAutomationFramwork.Core;

public class BrowserOptions
{
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = BrowserType.Chromium;
    [JsonPropertyName("headed")]
    public string Headed { get; set; } = "0";

}
public class PlaywrightConfiguration
{
    public readonly BrowserOptions browserOptions;
    public readonly BrowserNewContextOptions browserNewContextOptions;

    
    public PlaywrightConfiguration()
    {
        browserOptions = new BrowserOptions()
        {           
            Name = TestContext.Parameters["Browser"].ToString(),
            Headed = "1",           
        };

        browserNewContextOptions = new BrowserNewContextOptions()
        {
            BaseURL = TestContext.Parameters["Env"].ToString(),
            JavaScriptEnabled = true,
            ViewportSize = new ViewportSize() {Height= 824, Width=1536 }         
        };

    }
    
}