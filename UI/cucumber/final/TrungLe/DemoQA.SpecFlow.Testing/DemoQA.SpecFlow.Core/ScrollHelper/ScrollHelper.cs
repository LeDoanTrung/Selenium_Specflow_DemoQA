using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using TMS_Selenium.Library;

namespace DemoQA.SpecFlow.Core.ScrollHelper
{
    public class ScrollHelper
    {
        public static void ScrollToElement(IWebDriver driver, Element element)
        {
            try
            {
                IWebElement webElement = element.GetWebElement(driver);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", webElement);

                Actions actions = new Actions(driver);
                actions.MoveToElement(webElement).Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Scrolling to element failed: " + ex.Message);
            }
        }
    }
}
