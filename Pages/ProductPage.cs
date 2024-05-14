using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UIAutomationFramwork.Pages;

 public class ProductPage : PageBase
    {
        Random random= new Random();
        public string productcode { get; private set; }
        
        public ProductPage(IPage page) : base(page)
        {
        }

        public async Task AddProduct(dynamic inputData)
        {
            productcode= inputData["ProductCode"].ToString() + random.Next(101,99999).ToString("D4");
            await EnterValueInTextField("Product Code", productcode);
            await EnterValueInTextField("Product Description", inputData["ProductDescription"].ToString()+ random.Next(101, 99999).ToString("D4"));
            await clickRadioButton("Soybean");
            await clickCheckBox("Bag");
            await WaitForInvisibilityOfSpinner();
            await page.WaitForTimeoutAsync(2000);
            await ClickButton("Save");
            
        }
    }

