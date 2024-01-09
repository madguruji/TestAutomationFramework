using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAUTProj.Base;

namespace TestAUTProj.Pages
{
    public class LoginPage : BasePage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        private string USERNAME_TXTBOX = "ID=>email";
        private string PASSWORD_TXTBOX = "NAME=>password";
        private string LOGIN_BUTTON = "ID=>login";

        public NavigationPage signIn(string userName, string Password)
        {
            enterUserName(userName).enterPassword(Password).clickLoginButton();
            return new NavigationPage(driver);
        }

        public LoginPage enterUserName(string userName)
        {
            sendData(USERNAME_TXTBOX, userName, "username field", true);
            return this;
        }
        public LoginPage enterPassword(string pass)
        {
            sendData(PASSWORD_TXTBOX, pass, "password field", true);
            return this;
        }
        public LoginPage clickLoginButton()
        {
            elementClick(LOGIN_BUTTON, "login button");
            return this;
        }
        public void searchCourse()
        {

        }
    }
}
