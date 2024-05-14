using Microsoft.Playwright;
using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;
namespace StinePortalUIAutomation.Pages
{
    public class InventoryPage : PageBase
    {
        Random random = new Random();
        private string binCode;
        public ILocator btnAddInventory;
        public InventoryPage(IPage page) : base(page)
        {
            btnAddInventory = page.Locator("xpath=//button[@ng-reflect-message='Add Inventory']");
        }
        public string GetBinCode()
        {
            return binCode;
        }
        public void setBinCode(string binCode)
        {
            this.binCode = binCode;
        }

        public async Task AddInventory(dynamic inputData)
        {

            await SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
            string binCode = random.Next(101, 99999).ToString("D4");
            setBinCode(binCode);
            await EnterValueInTextField("Bin Code", binCode);
            await EnterValueInTextField("Guaranteed Bulk Bushels", inputData["GuaranteedBulkBushels"].ToString());
            await SelectFirstValueFromAutoCompleteDropdDown("Conditioner Code", inputData["ConditionerCode"].ToString());
            await SelectFirstValueFromAutoCompleteDropdDown("Grower Code", inputData["Grower"].ToString());
            await ClickButton("Add");
            await WaitForInvisibilityOfSpinner();
        }

        public ILocator getCompanyYearDdnOptions(string year)
        {
            return page.Locator("xpath=//span[contains(text(),'" + year + "')]/..");
        }
        public async Task clickDropdown(string placeHolder)
        {
            await page.Locator("xpath=//mat-label[text()='" + placeHolder + "']/../../preceding-sibling::mat-select | //mat-label[text()='" + placeHolder + "']/..//following-sibling::mat-select").ClickAsync();
        }
         
    }
}

