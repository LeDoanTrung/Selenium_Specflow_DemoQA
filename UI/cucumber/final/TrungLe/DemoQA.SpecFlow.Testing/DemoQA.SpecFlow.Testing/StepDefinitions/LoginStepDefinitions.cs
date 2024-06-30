using DemoQA.SpecFlow.Core.API;
using DemoQA.SpecFlow.Core.Configuration;
using DemoQA.SpecFlow.Core.Extensions;
using DemoQA.SpecFlow.Service.Service;
using SpecFlowProject.StepDefinitions;
using TMS.Core.Util;
using TMS_SpecFlow_Testing.Constants;
using TMS_SpecFlow_Testing.DataObjects;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly Dictionary<string, AccountDTO> _account;
        private UserService _userService;
        private APIClient _apiClient;
        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            this._account = JsonFileUtility.ReadAndParse<Dictionary<string, AccountDTO>>(FileConstant.AccountFilePath.GetAbsolutePath());
            this._apiClient = new APIClient(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:url"));
            this._userService = new UserService(_apiClient);
        }

        [Given(@"the user is logged into the application with a ""(.*)""")]
        public void GivenTheUserIsLoggedIntoTheApplicationWithA(string accountKey)
        {
            AccountDTO account = _account[accountKey];
        }
    }
}
