using NUnit.Framework;
using UIAutomationFramwork.Pages;
using System.Threading.Tasks;

namespace UIAutomationFramwork.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTest : TestBase
{

    [Test]
    public async Task Login()
    {
        using var loginPage = new LoginPage(Page);
        using var dashBoardPage = new DashBoardPage(Page);

        await loginPage.Goto();
        await loginPage.Login(inputData["userName"].ToString(), inputData["password"].ToString());
        await Expect(dashBoardPage.GetWelcomeTextLocator()).ToBeVisibleAsync();
    }
}
