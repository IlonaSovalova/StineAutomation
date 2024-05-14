using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UserAccessTests : TestBase
{

    [Test]
    public async Task AdminAccess()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("Administration");

        await Expect(dashBoardPage.GetMenuOptionLocator("Company Year Ranges")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("Sales Regions")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("Users")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("Import Prospect")).ToBeVisibleAsync();
    }


    [Test]
    public async Task RsaAccess()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("Administration");
        await Expect(dashBoardPage.GetMenuOptionLocator("Import Prospect")).ToBeVisibleAsync();
        await dashBoardPage.ExpandMenu("CRM");      
        await Expect(dashBoardPage.GetMenuOptionLocator("Prospects")).ToBeVisibleAsync(); 
        await Expect(dashBoardPage.GetMenuOptionLocator("All Prospects")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("Manage Prospects")).ToBeVisibleAsync(); 
        await Expect(dashBoardPage.GetMenuOptionLocator("Accounts")).ToBeVisibleAsync();

    }
    [Test]
    public async Task IsrAccess()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());

        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("CRM");
        await Expect(dashBoardPage.GetMenuOptionLocator("Prospects")).ToBeVisibleAsync();
        await Expect(dashBoardPage.GetMenuOptionLocator("Accounts")).ToBeVisibleAsync();

    }
    [Test]
    public async Task ControllerAccess()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());

        await dashBoardPage.clickMenu();
        await dashBoardPage.ExpandMenu("Administration");
        await Expect(dashBoardPage.GetMenuOptionLocator("Company Year Ranges")).ToBeVisibleAsync();
        await dashBoardPage.ExpandMenu("CRM");
        await Expect(dashBoardPage.GetMenuOptionLocator("Accounts")).ToBeVisibleAsync();

    }
}