using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;
using UIAutomationFramwork.Tests;
namespace UIAutomationFramwork.Tests { 

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BaggingApprovalTest : TestBase
{

    [Test]
    public async Task AddBaggingApproval()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var baggingApprovalPage = new BaggingApprovalPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await baggingApprovalPage.ClickButton("Add Bagging Approval");
        await baggingApprovalPage.AddBagging(inputData);
        await baggingApprovalPage.SelectFirstValueFromAutoCompleteDropdDown("Lot Number", baggingApprovalPage.GetLotNumber());
        await Expect(baggingApprovalPage.GetTableCellLocator(baggingApprovalPage.GetLotNumber())).ToBeVisibleAsync();
    }

    [Test]
    public async Task EditBaggingApproval()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);
        using var baggingApprovalPage = new BaggingApprovalPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await dashBoardPage.SelectMenuOption(inputData["menuName"].ToString(), inputData["subMenuName"].ToString());
        await baggingApprovalPage.SelectFirstValueFromAutoCompleteDropdDown("Company Year", "2024");

        await baggingApprovalPage.EditDeliveredInventory(inputData);
        await baggingApprovalPage.FilterList(baggingApprovalPage.GetTicketNumber());
        await Expect(baggingApprovalPage.GetTableCellLocator(baggingApprovalPage.GetTicketNumber())).ToBeVisibleAsync();

        //EditDeliveredtoElevator
        await baggingApprovalPage.EditDeliveredtoElevator(inputData);
        await baggingApprovalPage.FilterList(baggingApprovalPage.GetElevatorName());
        await Expect(baggingApprovalPage.GetTableCellLocator(baggingApprovalPage.GetElevatorName())).ToBeVisibleAsync();
    }
}

}
