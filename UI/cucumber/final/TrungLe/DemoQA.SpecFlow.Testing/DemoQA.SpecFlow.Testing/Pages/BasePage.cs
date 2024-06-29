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


        protected Element DynamicElement(string locator, params string[] parameters)
        {
            string formattedLocator = String.Format(locator, parameters);
            Element element = new Element(By.XPath(formattedLocator));
            return element;
        }

        protected void SelectElementWithDynamicLocator(string locator, params string[] parameters)
        {
            DynamicElement(locator, parameters).ClickOnElement();
        }

        protected string SelectTextWithDynamicLocator(string locator, params string[] parameters)
        {
            return DynamicElement(locator, parameters).GetText();
        }

        public void NavigateToCreateProjectPage(string menuItem = "Create Project")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void NavigateToSearchPage(string menuItem = "Search Project")
        {
            MenuTab.SelectMenuItem(menuItem);
        }
    }
}
