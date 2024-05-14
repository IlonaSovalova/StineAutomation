
using Microsoft.Playwright;
using System.Threading.Tasks;
using System;
using UIAutomationFramwork.Pages;
using FluentAssertions.Equivalency;
using System.Runtime.CompilerServices;
using System.Data.SqlTypes;

public class BaggingApprovalPage : PageBase
{
    private readonly ILocator _btnAdd;
    private readonly ILocator _btnEdit;

    Random random = new Random();
    private string lotNumber;
    private string ticketNumber;
    private string elevatorName;

    public BaggingApprovalPage(IPage page) : base(page)
    {
        _btnAdd = page.Locator("xpath=(//mat-icon[text()='add'])[1]");
        _btnEdit = page.Locator("xpath=(//mat-icon[text()='edit'])[1]");
    }

    public string GetLotNumber()
    {
        return lotNumber;
    }
    public void SetLotNumber(string lotNumber)
    {
        this.lotNumber = lotNumber;
    }
    public string GetTicketNumber()
    {
        return ticketNumber;
    }
    public void SetTicketNumber(string ticketNumber)
    {
        this.ticketNumber = ticketNumber;
    }
    public string GetElevatorName()
    {
        return elevatorName;
    }
    public void SetElevatorName(string elevatorName)
    {
        this.elevatorName = elevatorName;
    }


    public async Task AddBagging(dynamic inputData)
    {
        await SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await EnterValueInTextField("Bin Code", inputData["bincode"].ToString());
        await page.WaitForTimeoutAsync(2000);
        await _btnAdd.ClickAsync();
        await SelectValueFromDropdDown("Package Type", inputData["package"].ToString());
        string bagunits = random.Next(101, 999).ToString("D3");
        await EnterValueInTextField("Units to Bag", bagunits);
        SetLotNumber(random.Next(101, 99999).ToString("D5"));
        await EnterValueInTextField("Lot Number", GetLotNumber());
        string germ = random.Next(1, 9).ToString("D1");
        await EnterValueInTextField("Germ %", germ);
        await ClickButton("Add");
        await ClickAddButton("Comments");
        await EnterValueInTextField("Comment:", inputData["comment"].ToString());
        await ClickButton("Add");
        await ClickButton("Save");
    }

    public async Task EditDeliveredInventory(dynamic inputData)
    {
        await _btnEdit.ClickAsync();       
        await ClickTab("Delivered Inventory");
        await ClickButton("Add Delivered Inventory");
        SetTicketNumber(random.Next(101, 999).ToString("D3"));       
        await EnterValueInTextField("Ticket Number", GetTicketNumber());
        await ClickAddButton("Germs");
        await SelectValueFromDropdDown("Germs Type", inputData["germsType"].ToString());
        await ClickButton("Add");
        await ClickAddButton("Comments");
        await EnterValueInTextField("Comment:", inputData["comment"].ToString());
        await ClickButton("Add");
        await ClickButton("Save");
    }

    public async Task EditDeliveredtoElevator(dynamic inputData)
    {
        //await _btnEdit.ClickAsync();
        await ClickTab("Delivered to Elevator");
        await ClickButton("Add Delivered to Elevator");
        string number = (random.Next(101, 99999).ToString("D5"));
        await EnterValueInTextField("Reference Number", number);
        await EnterValueInTextField("Delivered Bulk Bushels", number);
        SetElevatorName("Test" + random.Next(101, 999).ToString("D3"));
        await EnterValueInTextField("Elevator Name", GetElevatorName());       
        await ClickAddButton("Comments");
        await EnterValueInTextField("Comment:", inputData["comment"].ToString());
        await ClickButton("Add");
        await ClickButton("Save");
    }
}