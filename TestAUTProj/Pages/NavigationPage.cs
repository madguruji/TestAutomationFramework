using OpenQA.Selenium;
using TestAUTProj.Base;
using TestAUTProj.Utilities;

namespace TestAUTProj.Pages
{
    public class NavigationPage : BasePage
    {
        private IWebDriver driver;
        public NavigationPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        private string ALL_COURSES_LINK = "XPATH=>//a[text()='ALL COURSES']";
        private string MY_COURSES_LINK = "CSS=>a[href='/mycourses']";
        private const string URL_ALLCOURSE= "https://www.letskodeit.com/courses";
        private const string ACCOUNT_ICON= "CSS=>img.zl-navbar-rhs-img";
        private const string LOGOUT_LINK= "CSS=>a[href='/logout']";
        private string LOGIN_LINK = "CSS=>a[href='/login'].dynamic-link";

        public LoginPage login()
        {
            elementClick(LOGIN_LINK, "loginLink");
            return new LoginPage(driver);

        }
        public NavigationPage allCourses()
        {
            Thread.Sleep(2000);
            elementClick(ALL_COURSES_LINK, "all courses link");
            return this;
        }
        public NavigationPage myCourses()
        {
            elementClick(MY_COURSES_LINK, "my courses link");
            return this;
        }

        public Boolean isUserLoggedIn()
        {
            try
            {
                waitForElement(ACCOUNT_ICON,3);
                getElement(ACCOUNT_ICON, "account Avatar Image");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
                throw;
            }
        }
        public Boolean verifyHeader()
        {
            string text = getText(ALL_COURSES_LINK, "All course link");
            return Utils.verifyTextContains(text, "All Courses");
        }
        public void Logout()
        {
            elementClick(ACCOUNT_ICON, "account icon");
            elementClick(waitForElement(LOGOUT_LINK, 5), "logout link");
        }
    }
}
