using OpenQA.Selenium;
using TestAUTProj.Base;

namespace TestAUTProj.Pages
{
    public class ResultPage : BasePage
    {
        private IWebDriver driver;

        public ResultPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        private string COURSES_LIST = "CSS=>.zen-course-list";
        public int courseCount()
        {
            waitForElement(COURSES_LIST, 3);
            var allCourses= getElementsLists(COURSES_LIST,"course list");
            return allCourses.Count;
        }
        public Boolean verifySearchResult()
        {
            Boolean result = false;
            if(courseCount()>0)
            {
                result = true;
            }
            return result;
        }
    }
}