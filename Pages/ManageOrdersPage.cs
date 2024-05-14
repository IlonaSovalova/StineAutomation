using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

public class MangeOrdersPage : PageBase
{



    Random random = new Random();
    public string productcode { get; private set; }

    public MangeOrdersPage(IPage page) : base(page)
    {

    }

    public async Task verifyOrderCreation(string quantity)
    {
        var orderRows = page.Locator("xpath=//tbody//tr[not(contains(@class,'expanded'))]");
        for (int i = 0; i < await orderRows.CountAsync(); i++)
        {
            await orderRows.Nth(i).ClickAsync();
            await page.Locator("xpath=//div[text()='" + quantity + "']").IsVisibleAsync();
        }
    }
}