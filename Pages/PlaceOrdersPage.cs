using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

public class PlaceOrdersPage : PageBase
{
       
    Random random = new Random();

    public string productcode { get; private set; }

    public PlaceOrdersPage(IPage page) : base(page)
    {
        
    }
    public ILocator getGeneralLocator(int count)
    {
        return page.Locator("xpath=//stine-account-info-preview//div//div[" + count + "]");
    }

    public ILocator getShippingLocator(int count)
    {
        return page.Locator("xpath=(//stine-preview-contact//div/div//div)[" + count + "]");
    }
    public async Task PlaceOrders(string accountName, string conditioner, string product, string packageType)
    {
        await SelectFirstValueFromAutoCompleteDropdDown("Account Name", accountName);
        await clickRadioButton("Conditioner");
        await SelectFirstValueFromAutoCompleteDropdDown("Conditioner", conditioner);
        await page.Locator("xpath=//mat-radio-button[contains(normalize-space(),'" + product + "')]").ClickAsync();
        await page.Locator("xpath=//*[not(contains(@class,'disabled'))]/following-sibling::*[normalize-space()='" + packageType + "']/../..").ClickAsync();
        string quantity = random.Next(101, 99999).ToString("D4");
        await EnterValueInTextField("Quantity", quantity);
        await ClickButton("Add to order");
        await ClickButton("Save");
    }    

    public async Task<List<string>> GetAccountDetailsFromOrder(dynamic inputData)
    {
        List<string> orderDetailsList = new List<string>();
        int gcount = await page.Locator("xpath = (//stine-account-info-preview//div//div)").CountAsync();
        for (int i = 1; i <= gcount; i++)
        {
            orderDetailsList.Add((await getGeneralLocator(i).InnerTextAsync()).Trim().Split(':')[1].Trim());
        }

        int count = await page.Locator("xpath = (//stine-preview-contact//div/div//div)").CountAsync();
        for (int i = 3; i <= count; i++)
        {
            orderDetailsList.Add((await getShippingLocator(i).InnerTextAsync()).Trim().Split(':')[1].Trim());
        }
        return orderDetailsList;
    }
}