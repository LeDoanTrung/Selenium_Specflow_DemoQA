using DemoQA.SpecFlow.Service.DataObjects;
using DemoQA_Selenium.DataObjects;
using DemoQA_Selenium.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class SearchBookStepDefinitions
    {
        private BookStorePage _bookStorePage;
        private readonly ScenarioContext _scenarioContext;

        public SearchBookStepDefinitions(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
            _bookStorePage = new BookStorePage();
        }

        [Given(@"there are books named with following title")]
        public void GivenThereAreBooksNamedWithFollwingTitle(Table table)
        {
            var books = table.CreateSet<BookDTO>().ToList();
            this._scenarioContext["books"] = books;
        }

        [When(@"the user inputs the keyword ""(.*)""")]
        public void WhenTheUserInputsTheKeyword(string keyword)
        {
            _bookStorePage.EnterSearchKeyword(keyword);
        }

        [Then(@"all books match with ""(.*)"" will be displayed")]
        public void ThenAllBooksMatchWithWillBeDisplayed(string keyword)
        {
            _bookStorePage.VerifySearchResult(keyword).Should().BeTrue();

            //Verify the total search result that matches the total given books
            var books = this._scenarioContext["books"] as List<BookDTO>;
            var totalBooks = books.Count();
            var actualBooks = _bookStorePage.GetSearchResultCount();
            actualBooks.Should().Be(totalBooks);
        }


    }
}
