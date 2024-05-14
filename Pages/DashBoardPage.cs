using Microsoft.Playwright;
using System.Threading.Tasks;



public class DashBoardPage : PageBase
{
    
    private readonly ILocator _txtWelcome;
    private readonly ILocator _btnMenu;

    public DashBoardPage(IPage page) : base(page)
    {
        _txtWelcome = page.Locator("xpath=//h1[contains(text(),'Welcome')]");
        _btnMenu= page.Locator("xpath=//mat-icon[text()='menu']");
    }       
    public ILocator GetWelcomeTextLocator()
    {
        return _txtWelcome;
    }
    public async Task clickMenu() 
    {
        await _btnMenu.ClickAsync();
    }

}


