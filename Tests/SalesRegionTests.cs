using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System;
using System.Threading.Tasks;

namespace UIAutomationFramwork.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class SalesRegionTests : TestBase
{

    [Test]
    public async Task AddRegion()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var addRegionPage = new AddRegionPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await addRegionPage.ClickButton("Add Region");
        await addRegionPage.AddRegion(inputData["regionName"].ToString(), inputData["state"].ToString(), inputData["county"].ToString());
        await addRegionPage.FilterList(addRegionPage.GetRegionName());
        await Expect(addRegionPage.GetTableCellLocator(addRegionPage.GetRegionName())).ToBeVisibleAsync();
        await addRegionPage.ClickDeleteIcon(addRegionPage.GetRegionName());
        await Expect(addRegionPage.GetTableCellLocator(addRegionPage.GetRegionName())).ToBeHiddenAsync();
    }
}

 