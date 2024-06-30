using OpenQA.Selenium;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.Pages;


namespace DemoQA_Selenium.Pages
{
    public class SearchBookPage : BasePage
    {
        //Web Element
        private Element searchBar = new Element(By.Id("searchBox"));
        string headerLocator = "//div[contains(@role,'columnheader')]/div[contains(@class,'header')]";
        private string searchResultRow = "//div[@role='rowgroup']/div[@role='row']";
        private string cellLocator = "//div[@role='gridcell']";
        private Element nextButton = new Element(By.XPath("//button[text()= 'Next']"));
        private Element noRowsFoundMessage = new Element(By.XPath("//div[text()='No rows found']"));

        //Page Method
        public void EnterSearchKeyword(string keyword)
        {
            searchBar.ClearText();
            searchBar.InputText(keyword);
        }


        public int FindIndexOfHeaderColumn(string headerName)
        {
            // Get all column header elements
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

        public bool VerifySearchResult(string keyword)
        {
            int titleIndex = FindIndexOfHeaderColumn("Title");
            int authorIndex = FindIndexOfHeaderColumn("Author");
            int publisherIndex = FindIndexOfHeaderColumn("Publisher");

            bool allRowsContainKeyword = true;

            if (titleIndex == -1 || authorIndex == -1 || publisherIndex == -1)
            {
                throw new Exception("One or more column headers not found.");
            }

            do
            {
                // Get all rows in the current page
                var rows = BrowserFactory.WebDriver.FindElements(By.XPath(searchResultRow));

                foreach (var row in rows)
                {
                    // Get cells in the row
                    var cells = row.FindElements(By.XPath(cellLocator));

                    string titleText = cells.ElementAt(titleIndex).Text;
                    string authorText = cells.ElementAt(authorIndex).Text;
                    string publisherText = cells.ElementAt(publisherIndex).Text;

                    // Check if any of the columns contain the keyword
                    if (!IsKeywordInText(keyword, titleText) && !IsKeywordInText(keyword, authorText) && !IsKeywordInText(keyword, publisherText))
                    {
                        allRowsContainKeyword = false;
                        break;
                    }

                }
                // Check if Next button is disabled
                if (nextButton.IsDisabled())
                {
                    break;
                }
                nextButton.ClickOnElement();
            } while (true);

            if (allRowsContainKeyword)
            {
                return true; //Return true when all rows match the keyword
            }

            if (noRowsFoundMessage.IsElementDisplayed()) //No result matched
            {
                return true;
            }

            return allRowsContainKeyword;
        }

        private bool IsKeywordInText(string keyword, string text)
        {
            return !string.IsNullOrWhiteSpace(text) && text.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
