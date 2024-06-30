using DemoQA.SpecFlow.Core.Configuration;
using DemoQA_Selenium.DataObjects;
using DemoQA_Selenium.Pages;
using SpecFlowProject.StepDefinitions;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TMS.Core.Util;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.Constants;

namespace DemoQA.SpecFlow.Testing.StepDefinitions
{
    [Binding]
    public class RegisterStudentStepDefinitions
    {
        private RegisterStudentPage _registerPage;
        private readonly ScenarioContext _scenarioContext;

        public RegisterStudentStepDefinitions(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
            _registerPage = new RegisterStudentPage();
        }


        [Given(@"the user is on Student Registration Form page")]
        public void GivenTheUserIsOnStudentRegistrationFormPage()
        {
            BrowserFactory.WebDriver.Url = ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "application:RegisterUrl");
        }

        [When(@"the user inputs the following data into Student Registration Form")]
        public void WhenTheUserInputsTheFollowingDataIntoStudentRegistrationForm(Table table)
        {
            var student = table.CreateInstance<StudentDTO>();
            this._scenarioContext[nameof(StudentDTO)] = student;
            _registerPage.RegisterStudent(student);
        }

        [When(@"the user click on Submit button")]
        public void WhenTheUserClickOnSubmitButton()
        {
            _registerPage.ClickOnSubmitButton();
        }

        [Then(@"a successfully message ""(.*)"" is shown")]
        public void ThenASuccessfullyMessageIsShown(string message)
        {
            _registerPage.VerifyMessageAfterRegistration(message);
        }

        [Then(@"all information of student form is shown")]
        public void ThenAllInformationOfStudentFormIsShown()
        {
            var student = this._scenarioContext[nameof(StudentDTO)] as StudentDTO;
            _registerPage.VerifyStudentInformation(student);
        }
    }
}
