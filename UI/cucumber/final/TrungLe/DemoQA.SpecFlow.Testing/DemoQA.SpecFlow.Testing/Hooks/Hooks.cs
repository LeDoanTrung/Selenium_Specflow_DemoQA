using TMS_Selenium.Library;
using TMS.Core.Configuration;
using TMS.Core.ExtentReport;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using DemoQA.SpecFlow.Core;
using TMS_SpecFlow_Testing.DataObjects;
using DemoQA.SpecFlow.Core.API;
using TMS.Core.Util;
using TMS_SpecFlow_Testing.Constants;
using DemoQA.SpecFlow.Core.Extensions;


namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        public static IConfiguration Config;
        const string AppSettingPath = "Configuration\\appsetting.json";
        private readonly ScenarioContext _scenerioContext;

        public Hooks(ScenarioContext context)
        {
            this._scenerioContext = context;
            ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.ClassName);         
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Config = ConfigurationHelper.ReadConfiguration(AppSettingPath);

            Console.WriteLine("Test Run Set up");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.GenerateReport();

            Console.WriteLine("Test Run Tear Down");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            ExtentTestManager.CreateParentTest(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {

        }

        [BeforeScenario]
        public void SetUpScenario(ScenarioContext context)
        {
            var config = Hooks.Config;

            var browser = GetConfigValue("application:Browser");
            var timeOutSec = GetConfigValueAsDouble("application:timeout.webdriver.wait.seconds");
            var pageLoadTime = GetConfigValueAsDouble("application:timeout.webdriver.pageLoad.seconds");
            var asyncJsTime = GetConfigValueAsDouble("application:timeout.webdriver.asyncJavaScript.seconds");

            InitializeBrowser(browser, timeOutSec, pageLoadTime, asyncJsTime);
            ExtentTestManager.CreateScenarioContext(context);

            BrowserFactory.WebDriver.Url = GetConfigValue("application:DomainURL");

            Console.WriteLine("Before Scenario");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ExtentTestManager.UpdateScenarioContext();
            BrowserFactory.WebDriver.Quit();
            Console.WriteLine("After Scenario");
        }

        [AfterStep]
        public void AfterStep()
        {
            ExtentTestManager.UpdateStepContext();
            Console.WriteLine("After Step");
        }

        private string GetConfigValue(string key)
        {
            return ConfigurationHelper.GetConfigurationByKey(Hooks.Config, key);
        }

        private double GetConfigValueAsDouble(string key)
        {
            return double.Parse(GetConfigValue(key));
        }

        private void InitializeBrowser(string browser, double implicitWait, double pageLoadTimeout, double asyncJsTimeout)
        {
            BrowserFactory.InitDriver(browser);
            var driver = BrowserFactory.WebDriver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(asyncJsTimeout);
        }

    }

}
