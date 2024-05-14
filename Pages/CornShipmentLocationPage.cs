using Microsoft.Playwright;
using NUnit.Framework;
using StinePortalUIAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;


public class CornShipmentLocationPage : PageBase
{
    Random random = new Random();
    public string locationName { get; private set; }

    public CornShipmentLocationPage(IPage page) : base(page)
    {
    }

    public async Task AddLocation(dynamic inputData)
    {
        locationName = inputData["locationName"].ToString() + random.Next(101, 99999).ToString("D4");
        await EnterValueInTextField("Corn shipment location name", locationName);
        await page.WaitForTimeoutAsync(3000);
        await SelectAvalueFromSluchBox("Available Region(s)");
        await SelectAvalueFromSluchBox("Available ISR(s)");
        await ClickButton("Save");  
     }
}

