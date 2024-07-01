using DemoQA_Selenium.Pages;
using NUnit.Framework;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class AddBookToCollectionStepDefinitions
    {
        private BookStorePage _bookStorePage;
        private readonly ScenarioContext _scenarioContext;

        public AddBookToCollectionStepDefinitions(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
            this._bookStorePage = new BookStorePage();
        }

        [When(@"the user selects a book ""(.*)""")]
        public void WhenTheUserSelectsABook(string bookName)
        {
           //TO DO: Need to design and implement SelectBook() method at BookStorePage
        }

        [When(@"the user clicks on Add To Your Collection")]
        public void WhenTheUserClicksOnAddToYourCollection()
        {
            //TO DO: Need to design and implement ClickOnAddButton() method at BookStorePage
        }

        [Then(@"an alert ""(.*)"" is shown")]
        public void ThenAnAlertBookAddedToYourCollection_IsShown(string message)
        {
            //TO DO: Need to design and implement VerifyAlertMessage() method at BookStorePage
        }

        [Then(@"book is shown in your profile")]
        public void ThenBookIsShownInYourProfile()
        {
            //TO DO: Need to design and implement VerifyAddedBookInformation() method at ProfilePage
        }
    }
}
