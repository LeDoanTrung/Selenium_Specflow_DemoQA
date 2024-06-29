using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using TechTalk.SpecFlow;
using TMS_Selenium.Library;

namespace TMS.Core.ExtentReport
{
    public class ExtentTestManager
    {
        private static AsyncLocal<ExtentTest> _parentTest = new AsyncLocal<ExtentTest>();
        private static AsyncLocal<ExtentTest> _childTest = new AsyncLocal<ExtentTest>();

        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest.Value = ExtentReportManager.Instance.CreateTest(testName, description);
            return _parentTest.Value;
        }

        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest.Value = _parentTest.Value.CreateNode(testName, description);
            return _childTest.Value;
        }

        public static ExtentTest GetTest()
        {
            return _childTest.Value;
        }

        public static void CreateScenarioContext(ScenarioContext context)
        {
            var tags = String.Join(" ", context.ScenarioInfo.Tags);
            var testName = tags + "Scenario:" + context.ScenarioInfo.Title;
            
            if(context.ScenarioInfo.Arguments.Count > 0)
            {
                testName += " - [" + context.ScenarioInfo.Arguments[0] + "]";
            }

            ExtentTestManager.CreateTest(testName);

            if(tags.ToLower().Contains("skip") || tags.ToLower().Contains("notsupport"))
            {
                Assert.Inconclusive($@"Ignore test case");
            }

            if (tags.ToLower().Contains("bug"))
            {
                Assert.Inconclusive($"Bug is existed {tags}");
            }
        }

        public static void UpdateStepContext()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            var stepName = ScenarioStepContext.Current.StepInfo.Text;
            var stepLog = stepType + " " + stepName;

            if (ScenarioStepContext.Current.StepInfo.Table !=  null)
            {
                stepLog += "<br>" + ScenarioStepContext.Current.StepInfo.Table.ToString();
                stepLog = stepLog.Replace("\n","<br>");
            }

            if(ScenarioContext.Current.TestError == null)
            {
                ExtentTestManager.GetTest().Log(Status.Pass, stepLog);
            }
            else
            {
                ExtentTestManager.GetTest().Log(Status.Fail, stepLog);
            }
        }

        public static void UpdateScenarioContext()
        {
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            Status logStatus = Status.Pass;

            if (ScenarioContext.Current.TestError != null)
            {
                if (ScenarioContext.Current.TestError.Message.Contains("Ignore"))
                {
                    logStatus = Status.Skip;
                }
                else if (ScenarioContext.Current.TestError.Message.Contains("Bug"))
                {
                    logStatus = Status.Warning;
                }
                else
                {
                    logStatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = "Screenshot_" + time.ToString("ddMMyyyy_h_mm_ss") + ".png";
                    String filePath = string.Empty;

                    try
                    {
                        if (BrowserFactory.WebDriver != null)
                        {
                            Screenshot screenshot = ((ITakesScreenshot)BrowserFactory.WebDriver).GetScreenshot();
                            filePath = "TestResults\\" + fileName;
                            screenshot.SaveAsFile(filePath);
                        }
                        else
                        {
                            filePath = "WebDriver is null, screenshot not taken.";
                        }
                    }
                    catch (Exception ex)
                    {
                        filePath = "Error taking screenshot: " + ex.Message;
                    }

                    ExtentTestManager.GetTest().AddScreenCaptureFromPath(filePath, title: fileName);
                    ExtentTestManager.GetTest().Log(logStatus, "InnerException => " + ScenarioContext.Current.TestError.InnerException);
                    ExtentTestManager.GetTest().Log(logStatus, "StackTrace => " + ScenarioContext.Current.TestError.StackTrace);
                    ExtentTestManager.GetTest().Log(logStatus, "Image => " + filePath);
                }

                ExtentTestManager.GetTest().Log(logStatus, "Message => " + ScenarioContext.Current.TestError.Message);
            }

            ExtentTestManager.GetTest().Log(logStatus, "Test ended with " + logStatus + " - " + stacktrace);
        }

    }
}
