using OpenQA.Selenium;
using TMS_Selenium.Library;


namespace TMS_SpecFlow_Testing.Pages
{
    public class BasePage
    {
        public void GoTo(string url)
        {
            BrowserFactory.WebDriver.Url = url;
        }

    }
}
