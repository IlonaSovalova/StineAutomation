using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


public class ConditionerDetailPage : PageBase
{
   
    private readonly ILocator _ddnStateProvince;
    private readonly ILocator _txtCounty;
    private readonly ILocator _btnContactsAdd;
    private readonly ILocator _btnCommentsAdd;
    private string conditionerName;
    private string estimatedAcres;
    Random random = new Random();

    public string GetConditionerName()
    {
        return conditionerName;
    }
    public void setConditionerName(string regionName)
    {
        this.conditionerName = regionName;
    }
    public string GetEstimatedAcres()
    {
        return estimatedAcres;
    }
    public void SetEstimatedAcres(string estimatedAcres)
    {
        this.estimatedAcres = estimatedAcres;
    }

    public ConditionerDetailPage(IPage page) : base(page)
    {
        _ddnStateProvince = page.Locator("xpath=(//mat-select)[1]");
        _txtCounty = page.GetByLabel("County");
        _btnContactsAdd = page.Locator("//*[contains(text(),'Contacts')]/..//mat-icon[contains(text(),'add')]");
        _btnCommentsAdd = page.Locator("//*[contains(text(),'Comments')]/..//mat-icon[contains(text(),'add')]");
        
    }
       
    public async Task SetCounty(string county)
    {
        await _txtCounty.FillAsync(county);
        await page.Locator("xpath=//span[contains(text(),'" + county + "')]").ClickAsync();
    }
   
    public async Task AddConditioner(dynamic inputData)
    {
        await ClickButton("Add Conditioner");
        string condName = inputData["name"].ToString() + random.Next(101, 99999).ToString("D4");
        setConditionerName(condName);
        await EnterValueInTextField("Conditioner Name", condName);
        string condCode= inputData["code"].ToString() + random.Next(101, 99999).ToString("D4");
        await EnterValueInTextField("Code", condCode);      
        await EnterValueInTextField("Phone",inputData["phone"].ToString());
        await EnterValueInTextField("Fax",inputData["fax"].ToString());
        await EnterValueInTextField("Email Address", inputData["email"].ToString());
        await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
        await EnterValueInTextField("City", inputData["city"].ToString());
        await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
        await SetCounty(inputData["county"].ToString());
        await EnterValueInTextField("Zip",inputData["zip"].ToString());
        await AddContacts(inputData);
        await ClickTab("Billing");
        await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
        await EnterValueInTextField("City", inputData["city"].ToString());
        await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
        await SetCounty(inputData["county"].ToString());
        await EnterValueInTextField("Zip", inputData["zip"].ToString());
        await AddContacts(inputData);
        await ClickTab("Shipping");
        await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
        await EnterValueInTextField("City", inputData["city"].ToString());
        await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
        await SetCounty(inputData["county"].ToString());
        await EnterValueInTextField("Zip", inputData["zip"].ToString());
        await AddContacts(inputData);
        await AddComments(inputData["comments"].ToString());
        await ClickButton("Save");
    }
    public async Task AddContacts(dynamic inputData)
    {
        await _btnContactsAdd.ClickAsync();
        await EnterValueInTextField("Name", inputData["contactName"].ToString());
        await EnterValueInTextField("Phone", inputData["contactPhone"].ToString());
        await EnterValueInTextField("Email", inputData["contactEmail"].ToString());
    }

    public async Task AddComments(string comments)
    {
        await _btnCommentsAdd.ClickAsync();
        await EnterValueInTextField("Comment:", comments);
    }

    public async Task enterAcres(string name, string value)
    {
        await page.Locator("xpath =//mat-label[text()='" + name + "']/../following-sibling::stine-field//input|//mat-label[text()='" + name + "']/../following-sibling::input").FillAsync(value);
        await page.WaitForTimeoutAsync(2000);
    }

    public async Task addAgreements(dynamic inputData)
    {
        await ClickAddButton("Agreements");
        String randomNum3 = random.Next(101, 999).ToString("D3");
        String randomNum2 = random.Next(1, 99).ToString("D2");
        await EnterValueInTextField("Acres", randomNum3);
        await EnterValueInTextField("Bushels/Acre", randomNum2);
        await EnterValueInTextField("% Take Obligation", randomNum2);
        await EnterValueInTextField("Price", randomNum3);
        await EnterValueInTextField("Units", randomNum2);        
        await ClickButton("Add");
    }

    public async Task addProducts(dynamic inputData)    
    {
        await ClickAddButton("Products");
        await SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
        await page.WaitForTimeoutAsync(2000);
        await SelectFirstValueFromAutoCompleteDropdDown("Grower Code", inputData["grower"].ToString());
        await page.WaitForTimeoutAsync(2000);
        string acres = random.Next(101, 999).ToString("D3");
        SetEstimatedAcres(acres);

        await enterAcres("Estimated Acres", GetEstimatedAcres());
        await enterAcres("Actual Acres", random.Next(101, 999).ToString("D3"));
        await ClickButton("Add");
    }
    public async Task selectPackageTypesOffered(dynamic inputData)
    {
        await clickCheckBox(inputData["PackageTypeOffered"].ToString());
    }

        
}