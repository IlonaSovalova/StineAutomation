using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;
using UIAutomationFramwork.Tests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ProspectsTest : TestBase
{

    public string? prospectName;

    [Test, Order(1)]
    public async Task AddProspect()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        prospectName = inputData["businessName"].ToString() + random.Next(101, 9999).ToString("D4");
        await prospectsPage.AddProspect(prospectName,
                inputData["email"].ToString(),
                inputData["streetAddress"].ToString(),
                inputData["city"].ToString(),
                inputData["state"].ToString(),
                inputData["county"].ToString(),
                inputData["zip"].ToString());
        await prospectsPage.SelectValueFromDropdDown("Status", "Not Contacted");
        await Expect(prospectsPage.GetProspectTableCellLocator(prospectsPage.GetBusinessName())).ToBeVisibleAsync();

    }

    [Test, Order(2)]
    public async Task AssignISR()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());

        await Expect(prospectsPage.GetTableCellLocator(prospectName)).ToBeVisibleAsync();
        await prospectsPage.AssigningISR(inputData, prospectName);
        await Expect(prospectsPage.GetTableCellLocator(prospectName)).ToBeVisibleAsync();
        await Expect(inputData["prospectName"].ToString()).ToBeVisibleAsync();
    }

    [Test]
    public async Task VerifyIsrView()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("CRM");
        await dashBoardPage.SelectMenuOption("Prospects");
        await Expect(prospectsPage.GetAddProspectButtonLocator()).ToBeVisibleAsync();
    }

    [Test]
    public async Task VerifyRSAView()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("CRM");
        await Expect(dashBoardPage.GetMenuOptionLocator("Prospects")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("All Prospects")).ToBeVisibleAsync();
        await dashBoardPage.SelectMenuOption("Prospects");
        await Expect(prospectsPage.GetAddProspectButtonLocator()).ToBeHiddenAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("ISR")).ToBeVisibleAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("Region")).ToBeVisibleAsync();
    }

    [Test]
    public async Task VerifySalesDirectorView()

    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("CRM");
        await Expect(dashBoardPage.GetMenuOptionLocator("Prospects")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("All Prospects")).ToBeVisibleAsync();
        await dashBoardPage.SelectMenuOption("Prospects");
        await Expect(prospectsPage.GetAddProspectButtonLocator()).ToBeHiddenAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("ISR")).ToBeVisibleAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("Region")).ToBeVisibleAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("RSA")).ToBeVisibleAsync();
    }

    [Test]
    public async Task VerifyRSAcumISRView()

    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("CRM");
        await Expect(dashBoardPage.GetMenuOptionLocator("Prospects")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("All Prospects")).ToBeVisibleAsync();
        await dashBoardPage.SelectMenuOption("Prospects");
        //await Expect(prospectsPage.GetAddProspectButtonLocator()).ToBeVisibleAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("ISR")).ToBeVisibleAsync();
        await Expect(prospectsPage.GetTableHeaderLocators("Region")).ToBeVisibleAsync();
    }

    [Test]
    public async Task verifyFilterSearch()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var prospectsPage = new ProspectsPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());

        int businessStateCountyISRCount = await prospectsPage.SearchResults(inputData, inputData["businessName"].ToString(), "", inputData["state"].ToString(), inputData["county"].ToString(), inputData["isr"].ToString());
        Assert.AreEqual(int.Parse(await prospectsPage.getPaginationValue()), businessStateCountyISRCount);
        await prospectsPage.clickOnLink("clear all filters");

        int businessNameSatateCount = await prospectsPage.SearchResults(inputData, inputData["businessName"].ToString(), inputData["name"].ToString(), inputData["state"].ToString());
        Assert.AreEqual(int.Parse(await prospectsPage.getPaginationValue()), businessNameSatateCount);
        await prospectsPage.clickOnLink("clear all filters");

        int businessNameCount = await prospectsPage.SearchResults(inputData, inputData["businessName"].ToString());
        Assert.AreEqual(int.Parse(await prospectsPage.getPaginationValue()), businessNameCount);
        await prospectsPage.clickOnLink("clear all filters");
    }
   }