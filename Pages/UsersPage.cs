using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace UIAutomationFramwork.Pages;

public class UsersPage : PageBase
{
    private readonly ILocator _ddnStateProvince;
    private readonly ILocator _txtCounty;
    private readonly ILocator _ddnJob;
    private readonly ILocator _btnAdd;
    private string userEmail;
    Random random = new Random();

    public string GetUserName()
    {
        return userEmail;
    }
    public void SetUserName(string userName)
    {
        this.userEmail = userName;
    }

    public UsersPage(IPage page) : base(page)
    {
        _ddnStateProvince = page.Locator("xpath=(//mat-select)[1]");
        _ddnJob = page.Locator("xpath=//mat-select[contains(@ng-reflect-name,'job')]");
        _txtCounty = page.GetByLabel("County");
        _btnAdd = page.Locator("xpath=//button[@ng-reflect-disabled='false']//span[contains(text(),'Add')]");
    }

    public async Task SetCounty(string county)
    {
        await _txtCounty.FillAsync(county);
        await page.Locator("xpath=//span[contains(text(),'" + county + "')]").ClickAsync();
    }

    public async Task AddUser(dynamic inputData)
    {
        await ClickButton("Add User");
        string userName = inputData["name"].ToString() + random.Next(101, 99999).ToString("D4") +"@stineseed.com";
        SetUserName(userName);
        await EnterValueInTextField("User Name - Email", userName);
        await EnterValueInTextField("First Name", inputData["firstName"].ToString());
        await EnterValueInTextField("Last Name", inputData["lastName"].ToString());
        await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
        await EnterValueInTextField("City", inputData["city"].ToString());       
        await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
        await SetCounty(inputData["county"].ToString());
        await EnterValueInTextField("Zip", inputData["zip"].ToString());
        await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
        await SelectValueFromDropdDown(_ddnJob, inputData["job"].ToString());
        await _btnAdd.ClickAsync();
    }

   
   
}
