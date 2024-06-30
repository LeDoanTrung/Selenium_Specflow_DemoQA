using System;
using TechTalk.SpecFlow;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{


    [Binding]
    public class AddBookToCollectionStepDefinitions
    {

        [Given(@"the user is on Book Store page")]
        public void GivenTheUserIsOnBookStorePage()
        {
            throw new PendingStepException();
        }

        [When(@"the user selects a book ""([^""]*)""")]
        public void WhenTheUserSelectsABook(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks on Add To Your Collection")]
        public void WhenTheUserClicksOnAddToYourCollection()
        {
            throw new PendingStepException();
        }

        [Then(@"an alert ""Book added to your collection\.” is shown")]
        public void ThenAnAlertBookAddedToYourCollection_IsShown()
        {
            throw new PendingStepException();
        }

        [Then(@"book is shown in your profile")]
        public void ThenBookIsShownInYourProfile()
        {
            throw new PendingStepException();
        }
    }
}
