using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAUTProj.Base;

namespace TestAUTProj.Pages
{
    public  class CategoryFilterPage:BasePage
    {
        private IWebDriver driver;
        public CategoryFilterPage(IWebDriver driver):base(driver)
        {
            this.driver = driver;

        }

        private string CATEGORY_DROPDOWN = "NAME=>categories";
        public ResultPage select(string categoryName)
        {
            IWebElement dropDown=getElement(CATEGORY_DROPDOWN,"category element");
            SelectElement selectElement = new SelectElement(dropDown);
            selectElement.SelectByText(categoryName);
            return new ResultPage(driver);
        }
    }
}