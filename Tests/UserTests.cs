using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UserTests : TestBase
{

    [Test, Order(1)]
    public async Task AddUser()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var userPage = new UsersPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await userPage.AddUser(inputData);
        await userPage.FilterList(userPage.GetUserName());
        await Expect(userPage.GetTableCellLocator((string)inputData["firstName"] + " " + (string)inputData["lastName"])).ToBeVisibleAsync();
        //await userPage.ClickDeleteIcon(inputData["firstName"].ToString()+" "+ inputData["lastName"].ToString());
        //await Expect(userPage.GetTableCellLocator((string)inputData["firstName"] + " " + (string)inputData["lastName"])).ToBeHiddenAsync();
    }

    [Test, Order(2)]
    public async Task EditUser()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var userPage = new UsersPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await userPage.AddUser(inputData);
        await userPage.FilterList(userPage.GetUserName());
        await userPage.ClickEditIcon(inputData["firstName"].ToString() + " " + inputData["lastName"].ToString());
        string updatedFirstName= inputData["updatedFirstName"].ToString()+ random.Next(101, 99999).ToString("D3");
        string updatedLastName = inputData["updatedLastName"].ToString() + random.Next(101, 99999).ToString("D3");
        await userPage.EditValueInTextField("First Name", updatedFirstName);
        await userPage.EditValueInTextField("Last Name", updatedLastName);
        await userPage.ClickButton("Save");
        await userPage.FilterList(updatedFirstName + " " + updatedLastName);
        await Expect(userPage.GetTableCellLocator(updatedFirstName + " " + updatedLastName)).ToBeVisibleAsync();
        //await userPage.ClickDeleteIcon(inputData["updatedFirstName"].ToString() + " " + inputData["updatedLastName"].ToString());
        //await Expect(userPage.GetTableCellLocator((string)inputData["updatedFirstName"] + " " + (string)inputData["updatedLastName"])).ToBeHiddenAsync();
    }

    [Test]
    public async Task validatePaginationInUsers()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var userPage = new UsersPage(Page);
        Random random = new Random();

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
        await userPage.selectAValueFromPaginator("50");
        Assert.AreEqual(userPage.GetDataTabletRowCount().Result, int.Parse("50"));
    }
}