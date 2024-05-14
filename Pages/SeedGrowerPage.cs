using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

namespace StinePortalUIAutomation.Pages
{
    public class SeedGrowerPage : PageBase
    {

        private readonly ILocator _ddnStateProvince;
        private readonly ILocator _txtCounty;
        private readonly ILocator _txtConditioner;
        private readonly ILocator _btnComment;


        private string seedGrowerName;
        private string commentDesc;
        private string estimatedAcres;
        Random random = new Random();

        public string GetSeedGrowerName()
        {
            return seedGrowerName;
        }

        public void setSeedGrowerName(string seedGrowerName)
        {
            this.seedGrowerName = seedGrowerName;
        }

        public SeedGrowerPage(IPage page) : base(page)
        {
            _ddnStateProvince = page.Locator("xpath=(//mat-select)[1]");
            _txtCounty = page.GetByLabel("County");
            _txtConditioner = page.Locator("xpath =//h2[text()='Agreements']/../..//input[@placeholder='Conditioner']");
            _btnComment = page.Locator("xpath=//h2[text()='Agreements']/../..//h2[text()='Comments']/..//mat-icon[text()='add']");           
        }

        public async Task SetCounty(string county)
        {
            await _txtCounty.FillAsync(county);
            await page.Locator("xpath=//span[contains(text(),'" + county + "')]").ClickAsync();
        }

        public async Task SetConditioner(string conditioner)
        {
           await _txtConditioner.FillAsync(conditioner);
           await page.Locator("xpath=//span[contains(text(),'" + conditioner + "')]").ClickAsync();
        }
        public string GetCommentDesc()
        {
            return commentDesc;
        }
        public void SetCommentDesc(string commentDesc)
        {
            this.commentDesc = commentDesc;
        }
        public string GetEstimatedAcres()
        {
            return estimatedAcres;
        }
        public void SetEstimatedAcres(string estimatedAcres)
        {
            this.estimatedAcres = estimatedAcres;
        }

        public async Task enterAcres(string name, string value)
        {
            await page.Locator("xpath =//mat-label[text()='" + name + "']/../following-sibling::stine-field//input|//mat-label[text()='" + name + "']/../following-sibling::input").FillAsync(value);
            await page.WaitForTimeoutAsync(2000);
        }

        public async Task addGrower(dynamic inputData)
        {
            await ClickButton("Add Grower");
            string growerName = inputData["name"].ToString() + random.Next(101, 99999).ToString("D4");
            setSeedGrowerName(growerName);
            await EnterValueInTextField("Grower Name", growerName);
            string condCode = inputData["code"].ToString() + random.Next(101, 99999).ToString("D4");
            await EnterValueInTextField("Code", condCode);
            await EnterValueInTextField("Phone", inputData["phone"].ToString());
            await EnterValueInTextField("Fax", inputData["fax"].ToString());
            await EnterValueInTextField("Email Address", inputData["email"].ToString());
            await SelectFirstValueFromAutoCompleteDropdDown("Conditioner", "au_");
            await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
            await EnterValueInTextField("City", inputData["city"].ToString());
            await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
            await SetCounty(inputData["county"].ToString());
            await EnterValueInTextField("Zip", inputData["zip"].ToString()); ;
            await ClickTab("Billing");
           // await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
            //await EnterValueInTextField("City", inputData["city"].ToString());
            //await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
            //await SetCounty(inputData["county"].ToString());
           // await EnterValueInTextField("Zip", inputData["zip"].ToString());
            await ClickTab("Shipping");
            // await EnterValueInTextField("Street Address", inputData["streetAddress"].ToString());
            //await EnterValueInTextField("City", inputData["city"].ToString());
            //await SelectValueFromDropdDown(_ddnStateProvince, inputData["state"].ToString());
            //await SetCounty(inputData["county"].ToString());
            //await EnterValueInTextField("Zip", inputData["zip"].ToString()); ;
            await ClickButton("Save");
        }

        public async Task addProducts(dynamic inputData)
        {
            await ClickAddButton("Products");
            await SelectFirstValueFromAutoCompleteDropdDown("Product", inputData["product"].ToString());
            await page.WaitForTimeoutAsync(2000);
            SetEstimatedAcres(random.Next(101, 999).ToString("D3"));
            await enterAcres("Estimated Acres", GetEstimatedAcres());
            await enterAcres("Actual Acres", random.Next(101, 999).ToString("D3"));
            await ClickButton("Add");
        }

        public async Task addAgreements(dynamic inputData)
        {
            await ClickAddButton("Agreements");            
            string contract = random.Next(101, 99999).ToString("D5");
            await EnterValueInTextField("Contract Number", contract);
            string acres = random.Next(101, 999).ToString("D3");    
            await EnterValueInTextField("Acres", acres);
            await SetConditioner(inputData["conditioner"].ToString());          
            await ClickButton("Add");
        }

        public async Task addComments(dynamic inputData)
        {
            await _btnComment.ClickAsync();            
            SetCommentDesc("Auto_" + random.Next(101, 999).ToString("35"));
            await EnterValueInTextField("Comment:", GetCommentDesc());           
            await ClickButton("Add");
        }
    }
}
