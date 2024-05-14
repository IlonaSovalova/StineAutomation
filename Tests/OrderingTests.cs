using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class OrderingTests : TestBase
{
    [Test]
    public async Task PlaceOrdersByProduct()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var ordersPage = new PlaceOrdersPage(Page);
        using var manageOrdersPage = new MangeOrdersPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Account Name", inputData["accountName"].ToString());
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await ordersPage.clickRadioButton(inputData["conditioner"].ToString());
        await ordersPage.clickRadioButton(inputData["packageType"].ToString());
        string quantity = random.Next(101, 99999).ToString("D4");
        await ordersPage.EnterValueInTextField("Quantity", quantity);
        await ordersPage.ClickButton("Add to order");

        await ordersPage.ClickButton("Save");
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Account name", inputData["accountName"].ToString());
        await manageOrdersPage.verifyOrderCreation(quantity);
    }

    [Test]
    public async Task verifyPackageTypeError()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var ordersPage = new PlaceOrdersPage(Page);
        using var manageOrdersPage = new MangeOrdersPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());      
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await ordersPage.clickRadioButton(inputData["conditioner"].ToString());
        await ordersPage.clickRadioButton(inputData["packageType"].ToString());
        string quantity = random.Next(101, 99999).ToString("D4");
        await ordersPage.EnterValueInTextField("Quantity", quantity);
        await ordersPage.ClickButton("Add to order");       
        List<string> errorMessageList = new List<string>(); 
        errorMessageList = (List<string>)await ordersPage.GetErrorMessages();           
        Assert.IsTrue(errorMessageList.Take(1).Contains("Must be a multiple of 50 for Corn"));
    }

    [Test]
    public async Task PlaceOrdersByConditioner()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var ordersPage = new PlaceOrdersPage(Page);
        using var manageOrdersPage = new MangeOrdersPage(Page);
        Random random = new Random();

        string quantity = random.Next(101, 99999).ToString("D4");
        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await ordersPage.PlaceOrders(inputData["accountName"].ToString(), inputData["conditioner"].ToString(), inputData["product"].ToString(), inputData["packageType"].ToString());
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Account name", inputData["accountName"].ToString());
        await manageOrdersPage.verifyOrderCreation(quantity);
    }
    [Test]
    public async Task verifyPlaceOrderScreen()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var ordersPage = new PlaceOrdersPage(Page);    

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Account Name"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Business Name"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Name"));
        Assert.IsTrue(await ordersPage.verifyLabelText("Find product information by:"));
        Assert.IsTrue(await ordersPage.verifyRadioButtonLabel("Product"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Product"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Quantity"));
        Assert.IsTrue(await ordersPage.verifyLabelText("Conditioner:"));
        await ordersPage.clickRadioButton("Conditioner");
        Assert.IsTrue(await ordersPage.verifyRadioButtonLabel("Conditioner"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Conditioner"));
        Assert.IsTrue(await ordersPage.verifyInputLabelText("Quantity"));
        Assert.IsTrue(await ordersPage.verifyLabelText("Products:"));
        await ordersPage.SelectFirstValueFromAutoCompleteDropdDown("Account Name", inputData["accountName"].ToString());
        Assert.IsTrue(await ordersPage.verifyLabelText("General"));
        Assert.IsTrue(await ordersPage.verifyLabelText("Shipping Contact Information"));
    }
}
