using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;


public class AddRegionPage : PageBase
{
    private readonly ILocator _ddnState;
    private readonly ILocator _ddnCounty;
    Random random = new Random();
    private string regionName;

    public string GetRegionName()
    {
        return regionName;
    }
    public void SetRegionName(string regionName)
    {
        this.regionName = regionName;
    }
    public AddRegionPage(IPage page) : base(page)
    {
        _ddnState = page.Locator("xpath=(//mat-select)[1]");
        _ddnCounty = page.Locator("xpath=(//mat-select)[2]");
    }

    public async Task AddRegion(string regionName, string state, string county)
    {
        string regName = regionName + random.Next(101, 99999).ToString("D4");
        SetRegionName(regName);
        await EnterValueInTextField("Region Name",regName);
        await page.WaitForTimeoutAsync(1000);
        await SelectValueFromDropdDown(_ddnState,state);
        await SelectValueFromDropdDown(_ddnCounty, county);
        await ClickButton("Add");
        await ClickButton("Save");
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
    
}

