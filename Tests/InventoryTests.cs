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

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class InventoryTests : TestBase
{

    [Test]
    public async Task AddInventory()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var inventoryPage = new InventoryPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await inventoryPage.ClickButton("Add Inventory");
        await inventoryPage.AddInventory(inputData);
        await inventoryPage.EnterValueInTextField("Filter", inventoryPage.GetBinCode());
        await Expect(inventoryPage.GetTableCellLocator(inventoryPage.GetBinCode())).ToBeVisibleAsync();       
    }


    [Test]
    public async Task AddInventoryPastYearsValidation()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var inventoryPage = new InventoryPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await inventoryPage.ClickButton("Add Inventory");
        await inventoryPage.clickDropdown("Company Year");
        Assert.AreEqual(await inventoryPage.GetAttributeValue(inventoryPage.getCompanyYearDdnOptions("2020"), "aria-disabled"),"true");
    }

    [Test]
    public async Task AddInventoryFutureYearsValidation()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var inventoryPage = new InventoryPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await inventoryPage.ClickButton("Add Inventory");
        await inventoryPage.clickDropdown("Company Year");
        Assert.AreEqual(await inventoryPage.GetAttributeValue(inventoryPage.getCompanyYearDdnOptions("2025"), "aria-disabled"), "false");
    }

    [Test]
    public async Task verifyUniqueBinCodeForCompanyYear()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var inventoryPage = new InventoryPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await inventoryPage.ClickButton("Add Inventory");
        await inventoryPage.SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        string binCode = random.Next(101, 99999).ToString("D4");
        await inventoryPage.EnterValueInTextField("Bin Code", binCode);
        await inventoryPage.WaitForInvisibilityOfSpinner();
        await inventoryPage.EnterValueInTextField("Guaranteed Bulk Bushels", inputData["GuaranteedBulkBushels"].ToString());
        await inventoryPage.WaitForInvisibilityOfSpinner();
        await inventoryPage.SelectFirstValueFromAutoCompleteDropdDown("Conditioner Code", inputData["ConditionerCode"].ToString());
        await inventoryPage.SelectFirstValueFromAutoCompleteDropdDown("Grower Code", inputData["Grower"].ToString());
        await inventoryPage.ClickButton("Add");
        await inventoryPage. WaitForInvisibilityOfSpinner();
        await inventoryPage.EnterValueInTextField("Filter", binCode);
        await inventoryPage.ClickButton("Add Inventory");
        await inventoryPage.SelectValueFromDropdDown("Company Year","2024");
        await inventoryPage.EnterValueInTextField("Bin Code", binCode);
    }



}
