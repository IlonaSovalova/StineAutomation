Playwright automation framework (v 1.0) with C# for stine portal automation

StinePortalUIAutomation
├───Core
├───Pages
├───Tests
└───Utils
 -- InputData


## Prerequisite:
1. [Microsoft Visual Studio 2022 IDE](https://visualstudio.microsoft.com/).

## Local Setup:
1. Open project with VS.
2. Restore all packages.
3. Build project.
4. Run 'pwsh bin\Debug\netX\playwright.ps1 install' command from your project bin directory. 
6. Run test.

Note: if there is any problem with executing pwsh command then install lastet version of powershell. 
https://stackoverflow.com/questions/53849730/how-to-troubleshoot-the-error-the-term-pwsh-exe-is-not-recognized-as-the-name



## References:
- [Playwright Get started](https://playwright.dev/dotnet/docs/intro).


## Configuration

 "baseURL":  Specify Application base URL, for e.g "https://teststineportal.stineseed.com/",    
 "javaScriptEnabled": true - please set it true if the testing application uses java script otherwise false
 
  browser: 
  "name": "chromium" - Chromium is nothing but chrome, for Firefox - it is firefox and for Safari - it is Webkit
  "headed": "1" - if the value is 1 then it will execute headed mode and if it is zero then it excutes in headless
