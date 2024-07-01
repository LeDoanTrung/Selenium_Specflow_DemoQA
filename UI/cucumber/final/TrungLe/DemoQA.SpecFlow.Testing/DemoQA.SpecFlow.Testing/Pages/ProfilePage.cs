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
        private Element _searchBar = new Element(By.Id("searchBox"));
        private Element _modalDelete = new Element(By.CssSelector("div.modal-content"));
        private Element _modalOkButton = new Element(By.Id("closeSmallModal-ok"));
        private Element _noRowsFoundMessage = new Element(By.XPath("//div[text()='No rows found']"));
        private Element _deletedIcon(string title) 
        { 
            return new Element(By.XPath($"//a[text()='{title}']/ancestor::div[@role='row']//span[contains(@id,'delete')]")); 
        }
        private Element _bookTitle (string title)
        {
            return new Element(By.XPath($"//a[text()='{title}']"));
        }


        //Method
        public void SearchBook(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
        }

        public bool IsBookExist(string title)
        {
            return _bookTitle(title).IsElementExist();
        }
        public void ClickOnDeleteIcon(string title)
        {
            if (IsBookExist(title))
            {
                _deletedIcon(title).ClickOnElement();
            }
        }

        public void ClickOnOkButton()
        {
            _modalOkButton.ClickOnElement();
        }

        public void CloseAlertMessage(string expectedMessage)
        {
            var alert = WaitForAlert();
            alert.Should().NotBeNull("Alert not displayed.");
            alert.Text.Should().Be(expectedMessage, "Alert message mismatch.");
            alert.Accept();
        }

        public void VerifyAddedBookInformation()
        {
            //TO DO: Implement code to verify added book's information 
        }
    }
}
