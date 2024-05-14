using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UIAutomationFramwork.Pages;

public class LoginPage: PageBase
{
    public LoginPage(IPage page) : base(page)
    {
    }

    public async Task Goto()
    {
        await page.GotoAsync(TestContext.Parameters["Env"].ToString(),new PageGotoOptions {Timeout =360000});
    }

    public async Task Login(string userName, string password)
    {
        await EnterValueInTextField("Email",userName);
        await EnterValueInTextField("Password",password);
        await ClickButton("Login");
    }
}
