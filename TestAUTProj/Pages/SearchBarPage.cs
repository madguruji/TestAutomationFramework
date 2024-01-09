using OpenQA.Selenium;
using TestAUTProj.Base;

namespace TestAUTProj.Pages
{
    public class SearchBarPage:BasePage
    {
        private IWebDriver driver;

        public SearchBarPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        private string SEARCHBOX_FIELD = "NAME=>course";
        private string SEARCHBOX_BUTTON= "CSS=>button.find-course";

        public ResultPage course(string courseName)
        {
            sendData(SEARCHBOX_FIELD, courseName, "searchbox field",true);
            elementClick(SEARCHBOX_BUTTON, "searchbox button");
            return new ResultPage(driver);
        }
    }
}