using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using UIAutomationFramwork.Core;
using UIAutomationFramwork.Utils;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;


    public abstract class TestBase : PageTest
    {

        private static readonly PlaywrightConfiguration _playwrightConfiguration;
        public dynamic? inputData;
        static TestBase()
        {
            _playwrightConfiguration = new PlaywrightConfiguration();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("Browser", _playwrightConfiguration.browserOptions.Name);
            Environment.SetEnvironmentVariable("HEADED", _playwrightConfiguration.browserOptions.Headed);
        }

        [SetUp]
        public void SetupBeforeEachTest()
        {
            inputData = LoadInputData(TestContext.CurrentContext.Test.Name);
            Page.SetDefaultTimeout(30000);
            Page.SetDefaultNavigationTimeout(50000);
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return _playwrightConfiguration.browserNewContextOptions;
        }

        public static dynamic LoadInputData(string testName)
        {
            string inputDataJSONPath = JsonReader.GetFilePathFromInputData("Testdata.json");
            dynamic jsonData = JsonReader.ReadJSONFromFile(inputDataJSONPath);
            return jsonData[testName];
        }
    }
    


