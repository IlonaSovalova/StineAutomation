using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;
using UIAutomationFramwork.Tests;
namespace UIAutomationFramwork.Tests;
public class CornShipmentLocationTests : TestBase
{
    [Test]
    public async Task AddLocation()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var cornShipmentLocationPage = new CornShipmentLocationPage(Page);
        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await cornShipmentLocationPage.ClickButton("Add Location");
        await cornShipmentLocationPage.AddLocation(inputData);
        await cornShipmentLocationPage.EnterValueInTextField("Filter",cornShipmentLocationPage.locationName);
        await Expect(cornShipmentLocationPage.GetTableCellLocator(cornShipmentLocationPage.locationName)).ToBeVisibleAsync();
    }
}