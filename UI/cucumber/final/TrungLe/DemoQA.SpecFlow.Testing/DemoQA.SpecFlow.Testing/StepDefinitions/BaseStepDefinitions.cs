using DemoQA.Core.Extensions;
using DemoQA.Service.Services;
using DemoQA.SpecFlow.Core.API;
using DemoQA.SpecFlow.Core.Configuration;
using DemoQA.SpecFlow.Core.Extensions;
using DemoQA.SpecFlow.Service.DataObjects;
using DemoQA.SpecFlow.Service.DataProvider;
using DemoQA.SpecFlow.Service.Service;
using DemoQA.SpecFlow.Testing.Pages;
using DemoQA_Selenium.Pages;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowProject.StepDefinitions;
using TMS.Core.Util;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.Constants;
using TMS_SpecFlow_Testing.DataObjects;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class BaseStepDefinitions
    {
        private readonly UserDataProvider _accountData;
        private readonly BookDataProvider _bookData;
        private LoginPage _loginPage;
        private UserService _userService;
        private BookService _bookService;
        private APIClient _apiClient;
        private ScenarioContext _scenarioContext;

        public BaseStepDefinitions(ScenarioContext scenarioContext)
        {
            this._accountData = new UserDataProvider(FileConstant.AccountFilePath.GetAbsolutePath());
            this._bookData = new BookDataProvider(FileConstant.BookFilePath.GetAbsolutePath());
            this._apiClient = new APIClient(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:DomainURL"));
            this._userService = new UserService(_apiClient);
            this._bookService = new BookService(_apiClient);
            _loginPage = new LoginPage();
            this._scenarioContext = scenarioContext;
        }

        [Given(@"the user is logged into the application with a ""(.*)""")]
        public async Task GivenTheUserIsLoggedIntoTheApplication(string accountKey)
        {
            AccountDTO account = _accountData.GetAccount(accountKey);
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:LoginUrl");
            ProfilePage profilePage = _loginPage.Login(account);

            var response = await _userService.TryHardGenerateTokenAsync(account.Username, account.Password, 4);
            response.VerifyStatusCodeOK();
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);

            this._scenarioContext["token"] = "Bearer " + result["token"];
            this._scenarioContext[nameof(AccountDTO)] = account;
            this._scenarioContext[nameof(ProfilePage)] = profilePage;
        }

        [Given(@"there is a book ""(.*)"" in the collection")]
        public async Task GivenThereIsABookInTheCollection(string bookKey)
        {
            var account = this._scenarioContext[nameof(AccountDTO)] as AccountDTO;
            var token = this._scenarioContext["token"] as string;
            BookDTO book = _bookData.GetBook(bookKey);

            await _bookService.DeleteBookFromCollectionAsync(account.Id, book.ISBN, token);

            var response = await _bookService.AddBookToCollectionAsync(account.Id, book.ISBN, token);
            response.VerifyStatusCodeCreated();
        }


        [Given(@"the user is on Student Registration Form page")]
        public void GivenTheUserIsOnStudentRegistrationFormPage()
        {
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:RegisterUrl");
        }

        [Given(@"the user is on Book Store page")]
        public void GivenTheUserIsOnBookStorePage()
        {
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:BookStoreURL");
        }

        [Given(@"the user is on Login page")]
        public void GivenTheUserIsOnLoginPage()
        {
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:LoginURL");
        }

        [Given(@"the user is on the Profile page")]
        public void GivenTheUserIsOnTheProfilePage()
        {
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:ProfileURL");
        }

    }
}
