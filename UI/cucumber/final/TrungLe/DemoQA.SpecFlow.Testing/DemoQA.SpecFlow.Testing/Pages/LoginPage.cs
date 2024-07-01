using DemoQA_Selenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V123.Profiler;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.DataObjects;
using TMS_SpecFlow_Testing.Pages;

namespace DemoQA.SpecFlow.Testing.Pages
{
    public class LoginPage : BasePage
    {
        //Web Element
        private Element userName = new Element(By.Id("userName"));
        private Element password = new Element(By.Id("password"));
        private Element loginButton = new Element(By.Id("login"));

        //Method
        public void InputUserName(string username)
        {
            userName.ClearText();
            userName.InputText(username);
        }

        public void InputPassword(string password)
        {
            this.password.ClearText();
            this.password.InputText(password);
        }

        public void clickOnLoginBtn()
        {
            loginButton.ClickOnElement();
        }

        public ProfilePage Login(AccountDTO account)
        {
            InputUserName(account.Username);
            InputPassword(account.Password);
            clickOnLoginBtn();

            return new ProfilePage();
        }
    }
}
