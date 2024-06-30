using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.Pages;


namespace DemoQA_Selenium.Pages
{
    public class ProfilePage : BasePage
    {
        //Web Element
        private Element userNameValue = new Element(By.Id("userName-value"));
        private Element searchBar = new Element(By.Id("searchBox"));
        private Element deleteIcon = new Element(By.Id("delete-record-undefined"));
        string headerLocator = "//div[contains(@role,'columnheader')]/div[contains(@class,'header')]";
        private string searchResultRow = "//div[@role='rowgroup']/div[@role='row']";
        private string cellLocator = "//div[@role='gridcell']";
        private Element modalDelete = new Element(By.CssSelector("div.modal-content"));
        private Element modalOkButton = new Element(By.Id("closeSmallModal-ok"));
        private Element noRowsFoundMessage = new Element(By.XPath("//div[text()='No rows found']"));
        //Method
        public void IsLoginSuccessfully(string username)
        {
            Assert.IsTrue(userNameValue.IsElementDisplayed(), "Login unsuccessfully.");
            Assert.AreEqual(userNameValue.GetText(), username, "Username is displayed incorrectly.");
        }

        public int FindIndexOfHeaderColumn(string headerName)
        {
            var headerElements = BrowserFactory.WebDriver.FindElements(By.XPath(headerLocator));

            for (int i = 0; i < headerElements.Count; i++)
            {

                if (headerElements[i].Text.Equals(headerName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        public void SearchBook(string keyword)
        {
            searchBar.ClearText();
            searchBar.InputText(keyword);
        }

        public void DeleteBook(string keyword)
        {
            SearchBook(keyword);

            int titleIndex = FindIndexOfHeaderColumn("Title");

            if (titleIndex == -1)
            {
                throw new Exception("Title column not found.");
            }

            var rows = BrowserFactory.WebDriver.FindElements(By.XPath(searchResultRow));

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.XPath(cellLocator));

                string titleText = cells[titleIndex].Text;

                if (titleText.Equals(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    deleteIcon.ClickOnElement();

                    if (modalDelete.IsElementDisplayed())
                    {
                        modalOkButton.ClickOnElement();
                        break;
                    }
                }

            }

        }

        public void VerifyDeleteBookSuccessfully(string keyword)
        {
            var alert = WaitForAlert();
            Assert.IsNotNull(alert, "Alert not displayed.");
            Assert.AreEqual("Book deleted.", alert.Text, "Alert message mismatch.");
            alert.Accept();

            SearchBook(keyword);
            //Verify that deleted book is not shown in the search result
            bool noResults = noRowsFoundMessage.IsElementDisplayed();
            Assert.IsTrue(noResults, "The book still exists after deletion.");
        }

        private IAlert WaitForAlert()
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
