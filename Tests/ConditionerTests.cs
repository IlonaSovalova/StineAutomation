using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using StinePortalUIAutomation.Pages;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ConditionerTests : TestBase
{

    [Test]
    public async Task AddConditioner()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var conditioneDetailPage = new ConditionerDetailPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await conditioneDetailPage.AddConditioner(inputData);
        await conditioneDetailPage.FilterList(conditioneDetailPage.GetConditionerName());
        await Expect(conditioneDetailPage.GetTableCellLocator(conditioneDetailPage.GetConditionerName())).ToBeVisibleAsync();
    }

    [Test]
    public async Task validatePaginationInInventory()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var userPage = new UsersPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await userPage.selectAValueFromPaginator("50");
        Assert.AreEqual(userPage.GetDataTabletRowCount().Result, int.Parse("50"));
    }

    [Test]
    public async Task addAgreementsAndProducts()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var conditioneDetailPage = new ConditionerDetailPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await conditioneDetailPage.ClickButton("Add Conditioner");
        await conditioneDetailPage.addAgreements(inputData);
        await conditioneDetailPage.addProducts(inputData);
        await Expect(conditioneDetailPage.GetTableCellLocator(conditioneDetailPage.GetEstimatedAcres())).ToBeVisibleAsync();        
        await conditioneDetailPage.selectPackageTypesOffered(inputData);
        await conditioneDetailPage.ClickButton("Save");
    }    
}