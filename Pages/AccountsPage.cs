using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UIAutomationFramwork.Pages;

public class AccountsPage : PageBase
{

    Random random = new Random();
    private ILocator _ddnAccountType;
    private ILocator _btnAdd;
    private ILocator _txtAccountType;
    private ILocator _lblCornShipmentLocation;
    private ILocator _chkSameAsMailing;
    private ILocator _txtAdditionalAddressInformation;
    private ILocator _txtCounty;
    private ILocator _lblState;

    private string businessName;

    public string GetBusinessName()
    {
        return businessName;
    }
    public void SetBusinessName(string businessName)
    {
        this.businessName = businessName;
    }

    public AccountsPage(IPage page) : base(page)
    {
        _ddnAccountType = page.Locator("xpath=//mat-select//*[text()='Account Type']");
        _btnAdd = page.Locator("//*[text()='Contacts']/../..//button//span[contains(text(),'Add')]");
        _txtAccountType = page.Locator("xpath=//mat-label[text()='Account Type']/../following-sibling::stine-field//p");
        _lblCornShipmentLocation = page.Locator("xpath=//mat-label[text()='Corn Shipment Location']/../following-sibling::stine-field//p");
        _chkSameAsMailing = page.Locator("//label[text()= 'Same as Mailing']/../div/input[@type='checkbox']");
        _txtAdditionalAddressInformation = page.Locator("xpath=//mat-label[text()='Additional Address Information']/../following-sibling::stine-field//input");
        _txtCounty = page.Locator("xpath=//mat-label[text()='County']/../following-sibling::input");
        _lblState = page.Locator("xpath=//mat-label[text()='State']/..//following-sibling::mat-select//div/div/span/span");
    }


    public ILocator getAddButtonLocator(string headerName)
    {
        return page.Locator("//*[text()='"+ headerName + "']/../..//button//span[contains(text(),'Add')]");
    }

    public ILocator getShippingLocator(int count)
    {
        return page.Locator("xpath=(//stine-preview-contact//div/div//div)[" + count + "]");       
    }

    public ILocator getGeneralLocator(int count)
    {
        return page.Locator("xpath=//stine-account-info-preview//div//div[" + count + "]");          
    }

    public ILocator getShippingDataLocator(string locatorName)
    {
        return page.Locator("xpath=//mat-label[text()='" + locatorName + "']/../following-sibling::stine-field//input");
    }

    public async Task createAccount(string accountName, dynamic inputData)
    {
        await ClickButton("Add Account");
        await EnterValueInTextField("Account Name", inputData["accountName"].ToString());
        await _ddnAccountType.ClickAsync();
        string accountTypeValue = inputData["accountType"].ToString();
        await page.Locator("xpath=//mat-option[@aria-disabled='false']//span[contains(text(),'" + accountTypeValue + "')]").ClickAsync();
        await ClickButton("Next");
        await EnterValueInTextField("Business Name ", accountName);
        await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
        await EnterValueInTextField("City", inputData["city"].ToString());
        await SelectValueFromDropdDown("State", inputData["state"].ToString());
        await SelectFirstValueFromAutoCompleteDropdDown("County", inputData["county"].ToString());
        await EnterValueInTextField("Zip", inputData["zip"].ToString());
        await ClickAddButton("Contacts");
        await EnterValueInTextField("Name", inputData["name"].ToString());
        await EnterValueInTextField("Phone", inputData["phone"].ToString());
        await _btnAdd.ClickAsync();
        await ClickButton("Next");
        await page.WaitForTimeoutAsync(4000);
        await EnterValueInTextField("Business Name ", inputData["businessName"].ToString());
        await clickCheckBox("Same as Mailing");
        await ClickAddButton("Contacts");
        await EnterValueInTextField("Name", inputData["name"].ToString());
        await EnterValueInTextField("Phone", inputData["phone"].ToString());
        await _btnAdd.ClickAsync();
        await ClickButton("Next");
        await page.WaitForTimeoutAsync(2000);
        await EnterValueInTextField("Business Name ", inputData["businessName"].ToString());
        await clickCheckBox("Same as Mailing");
        await ClickButton("Next");
        await EnterValueInTextField(" Total Intended Corn Acres ", inputData["totalIntendedCornAcres"].ToString());
        await EnterValueInTextField("Intended Stine Corn Acres", inputData["totalStineCornAcres"].ToString());
        await EnterValueInTextField(" Total Intended Soybean Acres ", inputData["totalIntendedSoybeanAcres"].ToString());
        await ClickButton("Next");
        await ClickButton("Add ISR");
        await SelectFirstValueFromAutoCompleteDropdDown("ISR name", inputData["isr"].ToString());
        await ClickButton("Next");
        await ClickButton("Save");
    }

    public async Task ViewAccountAndComment(dynamic inputData)
    {
        await ClickVisibilityIcon("autoBusi94030");
        await ClickAddButton("Comments/Visit Notes");
        await EnterValueInTextField("Comment:", "comments for edit account");
        await getAddButtonLocator("Comments/Visit Notes").ClickAsync();
        await ClickButton("Save");  
    }

    public async Task<List<string>> GetAccountDetails(dynamic inputData)
    {
        List<string> accountDetailsList = new List<string>();
        
        accountDetailsList.Add(await getShippingDataLocator("Account Name").InputValueAsync());
        accountDetailsList.Add(await _txtAccountType.InnerTextAsync());
        accountDetailsList.Add(await _lblCornShipmentLocation.InnerTextAsync());
        await ClickTab("Shipping");
        Boolean flag = await _chkSameAsMailing.IsCheckedAsync();
        if (flag == true)
        {
            await ClickTab("Mailing");
        } 
        accountDetailsList.Add(await getShippingDataLocator("Street Address").InputValueAsync());
        accountDetailsList.Add(await getShippingDataLocator("Apt/Suite").InputValueAsync());
        accountDetailsList.Add(await getShippingDataLocator("City").InputValueAsync());
        accountDetailsList.Add(await _lblState.InnerTextAsync());
        accountDetailsList.Add(await _txtCounty.InputValueAsync());
        accountDetailsList.Add(await getShippingDataLocator("Zip").InputValueAsync());            
        accountDetailsList.Add(await _txtAdditionalAddressInformation.InputValueAsync());
        return accountDetailsList; 
       
    }

}