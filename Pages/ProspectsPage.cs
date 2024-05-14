using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;


public class ProspectsPage : PageBase
{
    private readonly ILocator _btnAddProspect;
       
    private readonly ILocator _ddnState;
    private readonly ILocator _txtCounty;   
    private readonly ILocator _rdoSuggestedAddress;
    public readonly ILocator _btnNextPage;
    public readonly ILocator _txtPaginator;
    private readonly ILocator _ddnState_Province;
    private readonly ILocator _txtBusinessName;

    Random random = new Random();
    private string businessName;
    public string GetBusinessName()
    {
        return businessName;
    }
    public void SetBusinessName(string businessName)
    {
        this.businessName = businessName;
    }
    public ProspectsPage(IPage page) : base(page)
	{
        _btnAddProspect = page.Locator("xpath=//span[contains(text(), 'Add Prospect')]");               
        _ddnState = page.Locator("xpath=(//mat-select[@id = 'stateProvince'])");
        _txtCounty = page.GetByLabel("County");
        _rdoSuggestedAddress = page.Locator("xpath=//mat-label[contains(text(), 'Address according to USPS:')]/../mat-card-content/mat-radio-button/label/span/span[@class='mat-radio-inner-circle']");
        _txtPaginator = page.Locator("xpath=//div[@class='mat-mdc-paginator-range-actions']/div");
        _ddnState_Province = page.Locator("xpath=//mat-select[@id = 'mat-select-0']");
            
    }

    public async Task enterSearchName(string name, string value)
    {      
        await page.Locator("xpath =//mat-label[text()='" + name + "']/../following-sibling::stine-field//input|//mat-label[text()='" + name + "']/../following-sibling::input").FillAsync(value);
        await page.WaitForTimeoutAsync(4000);
    }

    public async Task<string> getPaginationValue()
    {
        return (await _txtPaginator.InnerTextAsync()).Split("of")[1].Trim();
    }

    public async Task clickOnLink(string linkName)
    {
        await page.Locator("xpath=//a[contains(text(), '" + linkName + "')]").ClickAsync();
        await page.WaitForTimeoutAsync(2000);
    }

    public async Task clickBusinessCheckBox(string businessName)
    {        
        await page.Locator("xpath=//td[text()=' " + businessName+ " ']/..//td//mat-checkbox").ClickAsync();
    }

    public async Task AddProspect(
        string businessName,        
        string email,        
        string streetAddress,
        string city,
        string state,
        string county,
        string zip)
    {
        await ClickButton("Add Prospect");    
        await WaitForInvisibilityOfSpinner();
      
        SetBusinessName(businessName);
        await EnterValueInTextField("Business Name", businessName);
        await EnterValueInTextField("Email", email);
        await EnterValueInTextField("Street Address", streetAddress);
        await EnterValueInTextField("City", city);
        await SelectValueFromDropdDown(_ddnState, state);        
        await _txtCounty.FillAsync(county);
        await page.Locator("xpath=//span[contains(text(),'" + county + "')]").ClickAsync();
        await EnterValueInTextField("Zip", zip);
        await ClickButton("Add");
        await page.WaitForTimeoutAsync(2000);
        if (await _rdoSuggestedAddress.IsVisibleAsync())
        {
            await _rdoSuggestedAddress.ClickAsync();
        }       
    }
    public ILocator GetAddProspectButtonLocator()
    {
        return _btnAddProspect;
    }
    public ILocator GetTableHeaderLocators(string headerName)
    {
        return page.Locator("xpath=//th//div[contains(text(),'"+headerName+"')]");
    }



    public async Task AssigningISR(dynamic inputData, string businessName)
    {
        await clickBusinessCheckBox(businessName);
        await SelectFirstValueFromAutoCompleteDropdDown("ISR", inputData["isr"].ToString());
        await ClickButton("Save");
        await ClickButton("Yes");
    }

    public async Task<int> SearchResults(dynamic inputData, string businessName, string name="", string state="", string county="", string isr="")
    {       
        await selectAValueFromPaginator("100");
        await page.WaitForTimeoutAsync(2000);
        string xpath = "xpath = //table//tr//td[contains(text(), '" + businessName + "') or contains(text(), '" + businessName.ToLower() + "')]";

        if (businessName != "")
        {
            await enterSearchName("Business Name", businessName);           
        }
        if (name != "")
        {
            await enterSearchName("Name", name);
            xpath = xpath + "/following-sibling::td[contains(text(), '" + name + "')]";
        }
        if (state != "")
        {
            await SelectValueFromDropdDown(_ddnState_Province, state);
            xpath = xpath + "/following-sibling::td[contains(text(), '" + state + "')]";
        }
        if (county != "")
        {
            await SelectValueFromDropdDown("County", county);
            xpath = xpath + "/following-sibling::td[contains(text(), '" + county + "')]";
        }
        if (isr != "")
        {
            await SelectFirstValueFromAutoCompleteDropdDown("Filter ISR", isr);
            xpath = xpath + "/following-sibling::td[contains(text(), '" + isr + "')]";
        }            
        var orderRows = page.Locator(xpath);
        return await orderRows.CountAsync();
    }
}