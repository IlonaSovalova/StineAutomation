using Microsoft.Playwright;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public abstract class PageBase : IDisposable, IAsyncDisposable
{

    protected readonly IPage page;
    protected static readonly ILogger log;
    private ILocator _btnDelete => page.Locator("xpath=//button//span[text()='Delete']");
    private ILocator _txtFilter => page.Locator("xpath=//input[@aria-label='filter']");
    private ILocator _btnMenu => page.Locator("xpath=//mat-icon[text()='menu']");
    private ILocator _lblRowCount => page.Locator("xpath=//div[@class='mat-paginator-range-label']");

    public PageBase(IPage page)
    {
        this.page = page;
        this.page.Load += Page_Load;
        this.page.Close += Page_Close;
        this.page.Console += Page_Console;
        this.page.PageError += Page_PageError;
        this.page.Crash += Page_Crash;
    }
    static PageBase()
    {
        LogManager.LoadConfiguration("nlog.config");
        log = LogManager.GetCurrentClassLogger();
    }
    private void Page_Crash(object? sender, IPage e)
    {
        log.Debug($"Crashed page URL is {e.Url}");

    }
    private void Page_PageError(object? sender, string e)
    {
        log.Error(e);

    }
    private void Page_Console(object? sender, IConsoleMessage e)
    {
        log.Debug(e.ToString());
    }
    private void Page_Load(object? sender, IPage e)
    {
        log.Debug($"Loaded page URL is {e.Url}");
    }
    private void Page_Close(object? sender, IPage e)
    {
        log.Debug($"Closed page URL is {e.Url}");
    }

    public void Dispose()
    {
        this.page.Load -= Page_Load;
        this.page.Close -= Page_Close;
        this.page.Console -= Page_Console;
        this.page.PageError -= Page_PageError;
        this.page.Crash -= Page_Crash;
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;

    }

    ValueTask IAsyncDisposable.DisposeAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// method to menu and sub menu options from hamburger menu
    /// </summary>
    /// <param name="menuOpn"> menu option name for e.g Administration</param>
    /// <param name="subOpn">Sub menu option for e.g. users</param>
    /// <returns>task</returns>
    public async Task SelectMenuOption(string menuOpn, string subOpn)
    {
        await _btnMenu.ClickAsync();
        await page.Locator("xpath=//div[contains(text(),'" + menuOpn + "')]/../following-sibling::div/mat-icon").ClickAsync(); //Commenting this line as there is change in the menu selection 
        await page.Locator("xpath=//*[text()='" + subOpn + "']").ClickAsync();
        await page.WaitForTimeoutAsync(2000);

    }

    /// <summary>
    /// method to click edit (pencil) icon in any screen
    /// </summary>
    /// <param name="recordName"> record which needs to edited for e.g. userName for list screen</param>
    /// <returns></returns>
    public async Task ClickEditIcon(string recordName)
    {
        await page.Locator("xpath=//td[contains(text(),'" + recordName + "')]/following-sibling::td//mat-icon[text()='edit']").ClickAsync();
    }

    /// <summary>
    /// method to click visibility (eye icon) icon in any screen
    /// </summary>
    /// <param name="recordName"> record which needs to edited for e.g. userName for list screen</param>
    /// <returns></returns>
    public async Task ClickVisibilityIcon(string recordName)
    {
        await page.Locator("xpath=//td[contains(text(),'" + recordName + "')]/following-sibling::td//mat-icon[text()='visibility']").ClickAsync();
    }


    /// <summary>
    /// method to delete a record from list screen
    /// </summary>
    /// <param name="recordName">record name to deleted</param>
    /// <returns></returns>
    public async Task ClickDeleteIcon(string recordName)
    {
        await page.WaitForTimeoutAsync(5000);
        //await page.GetByRole(AriaRole.Button).Filter(new LocatorFilterOptions() { HasTextString = text }).ClickAsync();
        await page.Locator("xpath=//*[contains(text(),'" + recordName + "')]/following-sibling::td//mat-icon[text()='delete']").ClickAsync(new LocatorClickOptions { Force = true });
        await _btnDelete.ClickAsync();
    }

    /// <summary>
    /// method to filter the list based on the given keyword
    /// </summary>adio
    /// <param name="keyword"> filter key word </param>
    /// <returns></returns>

    public async Task FilterList(string keyword)
    {
        await page.WaitForTimeoutAsync(3000); //Need to delete
        await WaitForInvisibilityOfSpinner();
        await _txtFilter.WaitForAsync(new LocatorWaitForOptions { Timeout = 60000 });
        await _txtFilter.FillAsync(keyword);
    }

    /// <summary>
    /// Method to select value from mat-select
    /// </summary>
    /// <param name="locator">mat- select (drop down locator)</param>
    /// <param name="locatorValue">mat option to select</param>
    /// <returns></returns>
    public async Task SelectValueFromDropdDown(ILocator locator, string locatorValue)
    {
        await page.WaitForTimeoutAsync(1000);
        await locator.ClickAsync();
        await page.Locator("xpath=//mat-option[@aria-disabled='false']//span[contains(text(),'" + locatorValue + "')]").ClickAsync();
    }

    /// <summary>
    /// Method to click Button based on the given text
    /// </summary>
    /// <param name="text">button text</param>
    /// <returns></returns>
    public async Task ClickButton(string text)
    {
        await page.Locator("xpath=//span[normalize-space()='" + text + "']").ClickAsync();
        await page.WaitForTimeoutAsync(2000);

    }

    /// <summary>
    /// Method to click Add Button (+ icon) based on the given header
    /// </summary>
    /// <param name="header">header text for e.g. contacts</param>
    /// <returns></returns>
    public async Task ClickAddButton(string header)
    {
        await page.Locator("xpath=//h2[text()='" + header + "']/..//mat-icon[text()='add']").ClickAsync();
        await page.WaitForTimeoutAsync(2000);
    }


    /// <summary>
    /// Method to enter text in given text field
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task EnterValueInTextField(string labelTxt, string value)
    {
        string locator = "xpath =//mat-label[text()='" + labelTxt + "']/../following-sibling::stine-field//input|//mat-label[text()='" + labelTxt + "']/../following-sibling::input";
        await page.Locator(locator).ClickAsync();
        await page.Locator(locator).FillAsync(value);

        //await page.GetByLabel(labelTxt).FillAsync("production@stineseed.com");
    }


    /// <summary>
    /// method to get table row count
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetTableRowCount()
    {
        await _lblRowCount.WaitForAsync(new LocatorWaitForOptions { Timeout = 30000 });
        string rowcountTxt = _lblRowCount.TextContentAsync().ToString().Split("of")[1].Trim();
        return int.Parse(rowcountTxt);
    }

    /// <summary>
    /// Method to return all error message in the page
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<string>> GetErrorMessages()
    {
        List<string> errorMessageList = new List<string>();
        var errorMessages = page.Locator("xpath=//mat-error");
        var count = await errorMessages.CountAsync();
        for (int i = 0; i < count; ++i)
            errorMessageList.Add(await errorMessages.Nth(i).TextContentAsync());
        return errorMessageList;
    }

    /// <summary>
    /// Method to get Error message
    /// </summary>
    /// <param name="locator">element Locator</param>
    /// <returns></returns>
    public async Task<string> GetErrorMessage(ILocator locator)
    {
        return await locator.TextContentAsync();
    }

    /// <summary>
    /// Method to wait for progress spinner invisibility
    /// </summary>
    /// <returns></returns>

    public async Task WaitForInvisibilityOfSpinner()
    {
        //await page.WaitForSelectorAsync("mat-spinner", new PageWaitForSelectorOptions() { State = WaitForSelectorState.Visible, Timeout = 3000 }); 
        await page.WaitForSelectorAsync("mat-spinner", new PageWaitForSelectorOptions() { State = WaitForSelectorState.Hidden });
    }

    /// <summary>
    /// Method to get Table cell locator values based on the given text
    /// </summary>
    /// <param name="regionName"></param>
    /// <returns></returns>
    public ILocator GetTableCellLocator(string uniqueValue)
    {
        return page.Locator("xpath=//td[contains(text(),'" + uniqueValue + "')]");
    }

    /// Method to get menu option locator
    /// </summary>
    /// <param name="uniqueValue"></param>
    /// <returns></returns>
    public ILocator GetMenuOptionLocator(string menuOptionName)
    {
        return page.Locator("xpath=//*[text()='" + menuOptionName + "']");
    }

    /// Method to expand main menu
    /// </summary>
    /// <param name="uniqueValue"></param>
    /// <returns></returns>
    public async Task ExpandMenu(string menuOptionName)
    {
        await page.Locator("xpath=//div[contains(text(),'" + menuOptionName + "')]/../following-sibling::div/mat-icon").ClickAsync();
    }


    /// Method to click menu options
    /// </summary>
    /// <param name="uniqueValue"></param>
    /// <returns></returns>
    public async Task SelectMenuOption(string subMenuOption)
    {
        await page.Locator("xpath=//div[(text()='" + subMenuOption + "')]/preceding-sibling::mat-icon").ClickAsync();
    }

    /// <summary>
    /// Method to enter text in given text field
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task EditValueInTextField(string labelTxt, string value)
    {
        string locator = "xpath =//mat-label[text()='" + labelTxt + "']/../following-sibling::stine-field//input|//mat-label[text()='" + labelTxt + "']/../../preceding-sibling::input";
        await page.Locator(locator).FillAsync("");
        await page.Locator(locator).ClickAsync();
        await page.Locator(locator).FillAsync(value);
    }


    /// <summary>
    /// Method to get preospect Table cell locator values based on the given text
    /// </summary>
    /// <param name="regionName"></param>
    /// <returns></returns>
    public ILocator GetProspectTableCellLocator(string uniqueValue)
    {
        return page.Locator("xpath=//td[text()=' " + uniqueValue + "']");
    }

    /// Method to select value from mat-select
    /// </summary>
    /// <param name="locator">mat- select (drop down locator)</param>
    /// <param name="locatorValue">mat option to select</param>
    /// <returns></returns>
    public async Task SelectValueFromDropdDown(string placeHolder, string locatorValue)
    {
        await page.WaitForTimeoutAsync(1000);
        await page.Locator("xpath=//mat-label[text()='" + placeHolder + "']/../../preceding-sibling::mat-select | //mat-label[text()='" + placeHolder + "']/..//following-sibling::mat-select").ClickAsync();
        await page.Locator("xpath=//mat-option[@aria-disabled='false']//span[contains(text(),'" + locatorValue + "')]").ClickAsync();
    }
    /// <summary>
    /// Method to click tabs in the application
    /// </summary>
    /// <param name="tabName"></param>
    /// <returns></returns>
    public async Task ClickTab(string tabName)
    {
        await page.Locator("xpath=//div[@role='tab']//span/span[normalize-space()='" + tabName + "']").ClickAsync();
        await page.WaitForTimeoutAsync(1000);
    }

    /// Method to select value from autocomplete dropdown
    /// </summary>
    /// <param name="locator">mat- select (drop down locator)</param>
    /// <param name="locatorValue">mat option to select</param>
    /// <returns></returns>
    public async Task SelectFirstValueFromAutoCompleteDropdDown(string placeHolder, string value)
    {
        await page.WaitForTimeoutAsync(1000);
        await page.Locator("xpath=//input[@ng-reflect-placeholder='" + placeHolder + "'] | //mat-label[text()='" + placeHolder + "']/../following-sibling::input").FillAsync(value);
        await page.Locator("xpath=(//mat-option[@aria-disabled='false'])[1]").ClickAsync();
    }

    /// <summary>
    /// Method to click Radio button or check box
    /// </summary>
    /// <param name="text">button text</param>
    /// <returns></returns>
    public async Task clickRadioButton(string text)
    {
        await page.Locator("xpath=//mat-radio-button[normalize-space()='" + text + "']").ClickAsync();
    }

    /// <summary>
    /// Method to click Check box or check box
    /// </summary>
    /// <param name="text">button text</param>
    /// <returns></returns>
    public async Task clickCheckBox(string text)
    {
        await page.Locator("xpath=//mat-checkbox[normalize-space()='" + text + "']").ClickAsync();
    }


    /// Method to select value from autocomplete dropdown
    /// </summary>
    /// <param name="locator">mat- select (drop down locator)</param>
    /// <param name="locatorValue">mat option to select</param>
    /// <returns></returns>
    public async Task SelectAvalueFromSluchBox(string slushBoxName)
    {
        await page.WaitForTimeoutAsync(1000);
        await page.Locator("xpath=(//stine-slush-box[@ng-reflect-label-left='" + slushBoxName + "']//div[@cdkdrag and not(@ng-reflect-disabled)])[1]").ClickAsync();
        await page.Locator("xpath=//stine-slush-box[@ng-reflect-label-left='" + slushBoxName + "']//mat-icon[text()='chevron_right']").ClickAsync();
    }
    /// <summary>
    /// Method to click icon on the read only field
    /// </summary>
    /// <param name="labelTxt">Label text to identify locator</param>
    /// <param name="value">updated text</param>
    /// <returns></returns>
    public async Task ClickEditButtonInTextField(string labelTxt, string value)
    {

        string locator = "xpath =//mat-label[text()='" + labelTxt + "']/../following-sibling::stine-field//button";
        await page.Locator(locator).HoverAsync();
        await page.Locator(locator).ClickAsync();
        await page.Locator(locator).FillAsync(value);
    }
    /// <summary>
    /// Method to get attribute value of a locator
    /// </summary>
    /// <param name="locator"> Ilocator </param>
    /// <param name="attributeValue"> a</param>
    /// <returns></returns>
    public async Task<string> GetAttributeValue(ILocator locator, string attributeValue)
    {
        return await locator.GetAttributeAsync(attributeValue);

    }
    public async Task selectAValueFromPaginator(string value)
    {
        string locator = "xpath=//mat-paginator//mat-select";
        await page.Locator(locator).ClickAsync();
        await page.Locator("xpath=//mat-option[@ng-reflect-value='" + value + "']").ClickAsync();
        await WaitForInvisibilityOfSpinner();
    }

    public async Task<int> GetDataTabletRowCount()
    {
        var errorMessages = page.Locator("xpath=//tbody/tr");
        return await errorMessages.CountAsync();
    }

    /// <summary>
    /// Method to verify same as mailing check box checked or not
    /// </summary>
    /// <returns> boolean value </returns>
    public async Task<Boolean> verifySameAsMailingCheckBoxStatus()
    {
       return await page.Locator("xpath=//label[text()= 'Same as Mailing']/../div/input[@type='checkbox']").IsCheckedAsync();
        
    }

    /// <summary>
    /// Method to verify label present or not
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task<Boolean> verifyInputLabelText(string labelTxt)
    {
        return await page.Locator("xpath =//mat-label[text()='" + labelTxt + "']/../following-sibling::stine-field//input|//mat-label[text()='" + labelTxt + "']/../following-sibling::input").IsVisibleAsync();
    }

    /// <summary>
    /// Method to verify Radio button label
    /// </summary>
    /// <param name="text">button text</param>
    /// <returns></returns>
    public async Task<Boolean> verifyRadioButtonLabel(string text)
    {
        return await page.Locator("xpath=//mat-radio-button[normalize-space()='" + text + "']").IsVisibleAsync();
    }

    /// <summary>
    /// Method to verify label
    /// </summary>
    /// <param name="text">button text</param>
    /// <returns></returns>
    public async Task<Boolean> verifyLabelText(string text)
    {
        return await page.Locator("xpath=//div/strong[text()='" + text + "']|//div/mat-card-title[text()='" + text + "']").IsVisibleAsync();
    }

}

