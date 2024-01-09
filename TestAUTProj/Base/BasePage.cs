using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAUTProj.Helpers;

namespace TestAUTProj.Base
{
    public class BasePage:CustomDriver
    {
        IWebDriver driver;
        public BasePage(IWebDriver driver):base(driver)
        {
            this.driver = driver;
        }
        public Boolean verifyTitle(string expectedTitle)
        {
            return driver.Title.Equals(expectedTitle);
        }

    }
}
