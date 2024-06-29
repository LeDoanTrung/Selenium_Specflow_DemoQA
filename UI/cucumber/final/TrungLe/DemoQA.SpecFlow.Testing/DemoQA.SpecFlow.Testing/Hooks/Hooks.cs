using TMS_Selenium.Library;
using TMS.Core.Configuration;
using TMS.Core.ExtentReport;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;


namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        public static IConfiguration Config;
        const string AppSettingPath = "Configurations\\appsettings.json";
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
            string browser = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:Browser");
            double timeOutSec = double.Parse(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:timeout.webdriver.wait.seconds"));
            string pageLoadTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:timeout.webdriver.pageLoad.seconds");
            string asyncJsTime = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:timeout.webdriver.asyncJavaScript.seconds");

            BrowserFactory.InitDriver(browser);
            BrowserFactory.WebDriver.Manage().Window.Maximize();
            BrowserFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutSec); 
            BrowserFactory.WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(double.Parse(pageLoadTime));
            BrowserFactory.WebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(double.Parse(asyncJsTime));

            ExtentTestManager.CreateScenarioContext(context);

            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:url");

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

    }

}
