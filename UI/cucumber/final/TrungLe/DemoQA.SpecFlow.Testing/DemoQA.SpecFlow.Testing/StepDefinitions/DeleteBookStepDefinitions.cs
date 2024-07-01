using DemoQA_Selenium.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class DeleteBookStepDefinitions
    {
        private ProfilePage _profilePage;
        private readonly ScenarioContext _scenarioContext;

        public DeleteBookStepDefinitions(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
            _profilePage = new ProfilePage();
        }

        [When(@"the user searchs book with ""(.*)""")]
        public void WhenTheUserSearchsBookWith(string title)
        {
            _profilePage.SearchBook(title);
            this._scenarioContext["title"] = title;
        }

        [When(@"the user clicks on Delete icon")]
        public void WhenTheUserClicksOnDeleteIcon()
        {
            var title = (string) this._scenarioContext["title"];
            _profilePage.ClickOnDeleteIcon(title);
        }

        [When(@"the user clicks on OK button")]
        public void WhenTheUserClicksOnOKButton()
        {
            _profilePage.ClickOnOkButton();
        }

        [When(@"the user clicks on OK button of alert ""(.*)""")]
        public void WhenTheUserClicksOnOKButtonOfAlert(string message)
        {
            _profilePage.CloseAlertMessage(message);
        }

        [When(@"the book is not shown")]
        public void WhenTheBookIsNotShown()
        {
            var title = (string)this._scenarioContext["title"];
            _profilePage.IsBookExist(title).Should().BeFalse();
        }

    }
}
