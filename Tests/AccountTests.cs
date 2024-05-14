using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using StinePortalUIAutomation.Pages;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UIAutomationFramwork.Tests;


    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class AccountTests : TestBase
    {

        string accountBusinessName = "";

        [Test, Order(1)]
        public async Task AddAccount()
        {
            using var loginPage = new LoginPage(Page);
            using var dashBoardPage = new DashBoardPage(Page);
            using var seedGrowersPage = new SeedGrowerPage(Page);
            using var accountsPage = new AccountsPage(Page);
            Random random = new Random();

            await loginPage.Goto();
            await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
            await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
            await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
            accountBusinessName = inputData["businessName"].ToString() + random.Next(101, 99999).ToString("D4");
            await accountsPage.createAccount(accountBusinessName, inputData);
            accountBusinessName = accountsPage.GetBusinessName();
            await accountsPage.FilterList(accountsPage.GetBusinessName());
            await Expect(accountsPage.GetTableCellLocator(accountsPage.GetBusinessName())).ToBeVisibleAsync();
        }

        [Test]
        public async Task ViewAccount()
        {
            using var loginPage = new LoginPage(Page);
            using var dashBoardPage = new DashBoardPage(Page);
            using var seedGrowersPage = new SeedGrowerPage(Page);
            using var accountsPage = new AccountsPage(Page);

            await loginPage.Goto();
            await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
            await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
            await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
            await accountsPage.FilterList("autoBusi94030");
            await Expect(accountsPage.GetTableCellLocator("autoBusi94030")).ToBeVisibleAsync();
            await accountsPage.ViewAccountAndComment(inputData);
        }

        [Test, Order(2)]
        public async Task createOrderFromAccount()
        {
            using var loginPage = new LoginPage(Page);
            using var dashBoardPage = new DashBoardPage(Page);
            using var seedGrowersPage = new SeedGrowerPage(Page);
            using var accountsPage = new AccountsPage(Page);
            using var placeOrders = new PlaceOrdersPage(Page);

            await loginPage.Goto();
            await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
            await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Timeout = 30000 });
            await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), (inputData["subMenuName"].ToString()));
            await accountsPage.FilterList(accountBusinessName);
            await Expect(accountsPage.GetTableCellLocator(accountBusinessName)).ToBeVisibleAsync();
            await accountsPage.ClickVisibilityIcon(accountBusinessName);
            List<string> accountsDetails = await accountsPage.GetAccountDetails(inputData);
            await accountsPage.ClickAddButton("Orders");
            List<string> accountDetailsFromOrder = await placeOrders.GetAccountDetailsFromOrder(inputData);
            Assert.AreEqual(accountsDetails, accountDetailsFromOrder);
        }
    }
