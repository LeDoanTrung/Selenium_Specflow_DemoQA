using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TMS_Selenium.Library;


namespace TMS_SpecFlow_Testing.Pages
{
    public class BasePage
    {
        public void GoTo(string url)
        {
            BrowserFactory.WebDriver.Url = url;
        }

        public void RefreshPage()
        {
            BrowserFactory.WebDriver.Navigate().Refresh();
        }
        public IAlert WaitForAlert()
        {
            try
            {
                return BrowserFactory.Wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

    }
}
