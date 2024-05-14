using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using StinePortalUIAutomation.Pages;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class SeedGrowerTests : TestBase
{

    [Test]
    public async Task AddSeedGrowers()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var seedGrowersPage = new SeedGrowerPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
/*        await seedGrowersPage.addGrower(inputData);
        await seedGrowersPage.FilterList(seedGrowersPage.GetSeedGrowerName());
        await Expect(seedGrowersPage.GetTableCellLocator(seedGrowersPage.GetSeedGrowerName())).ToBeVisibleAsync();*/
       
        await seedGrowersPage.ClickButton("Add Grower");
        await seedGrowersPage.addAgreements(inputData);
        await seedGrowersPage.addComments(inputData);
        await Expect(seedGrowersPage.GetTableCellLocator(seedGrowersPage.GetCommentDesc())).ToBeVisibleAsync();

        await seedGrowersPage.addProducts(inputData);
        await Expect(seedGrowersPage.GetTableCellLocator(seedGrowersPage.GetEstimatedAcres())).ToBeVisibleAsync();
        await seedGrowersPage.ClickButton("Save");
    }

    [Test]
    public async Task VerifyConditionerCodesInListScreen()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var seedGrowersPage = new SeedGrowerPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await seedGrowersPage.addGrower(inputData);
        await seedGrowersPage.FilterList(seedGrowersPage.GetSeedGrowerName());
        await Expect(seedGrowersPage.GetTableCellLocator(seedGrowersPage.GetSeedGrowerName())).ToBeVisibleAsync();
    }


    [Test]
    public async Task validatePaginationInSeedGrowers()
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
    public async Task verifySameAsMailingCheckBoxStatus()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var seedGrowersPage = new SeedGrowerPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));       
        await seedGrowersPage.ClickButton("Add Grower");
        await seedGrowersPage.ClickTab("Billing");
        Assert.IsTrue(await seedGrowersPage.verifySameAsMailingCheckBoxStatus());
        await seedGrowersPage.ClickTab("Shipping");
        Assert.IsTrue(await seedGrowersPage.verifySameAsMailingCheckBoxStatus());
    }
}
 