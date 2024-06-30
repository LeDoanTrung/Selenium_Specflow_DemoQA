using OpenQA.Selenium;
using TMS_Selenium.Library;
using TMS_SpecFlow_Testing.DataObjects;
using TMS_SpecFlow_Testing.Pages;

namespace DemoQA_Selenium.Pages
{
    public class LoginPage :BasePage
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

        public void Login(AccountDTO account)
        {
            InputUserName(account.Username);
            InputPassword(account.Password);
            clickOnLoginBtn();
        }


    }
}
