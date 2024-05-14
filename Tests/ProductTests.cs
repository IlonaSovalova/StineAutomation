using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

namespace UIAutomationFramwork.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

    public class ProductTests : TestBase
    {
        [Test]
        public async Task AddProduct()
        {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var productPage= new ProductPage(Page);
        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await productPage.ClickButton("Add Product");
        await productPage.AddProduct(inputData);
        await productPage.FilterList(productPage.productcode);        
        await Expect(productPage.GetTableCellLocator(productPage.productcode)).ToBeVisibleAsync();
    }   
    }
