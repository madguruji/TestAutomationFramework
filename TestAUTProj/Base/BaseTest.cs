using OpenQA.Selenium;
using TestAUTProj.Pages;
using NUnit.Framework;
using TestAUTProj.Driver;
using System.Configuration;
using TestAUTProj.Utilities;
using log4net;
using TestAUTProj.Tests;

namespace TestAUTProj.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected string baseUrl;
        protected LoginPage loginPage;
        protected NavigationPage nav;
        protected SearchBarPage searchBarPage;
        protected ResultPage resultPage;
        protected CategoryFilterPage categoryFilterPage;

        private ILog log = LogManager.GetLogger(TestContext.CurrentContext.Test.Name);


        [SetUp]
        public void start()
        {
            Console.WriteLine("************************* STARTED RUNNING SETUP *************************");
            string browser = null;
            try
            {
                browser = ConfigurationManager.AppSettings["browser"];

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
            driver = DriverInit.getInstance().getDriver(browser);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            baseUrl = "https://www.letskodeit.com/";
            driver.Navigate().GoToUrl(baseUrl);
            CheckPoint.clearHashMap();
            nav = new NavigationPage(driver);
            loginPage = nav.login();

            Console.WriteLine("************************* ENDING SETUP *************************");
            string filePath = Utils.getProjectFolderPath("TestData")+ "TestData.xlsx";
            ExcelUtil.setExcelFile(filePath, "TestData");

            Console.WriteLine("\n\n\n************************* START RUNNING TESTS *************************\n\n\n");

        }

        [TearDown]
        public void stop()
        {
            Console.WriteLine("\n\n\n************************* STARTING TEARDOWN EXECUTION *************************");

            if (nav.isUserLoggedIn())
            {
                nav.Logout();
            }
            DriverInit.getInstance().quitDriver();
            Console.WriteLine("************************* ENDING TEARDOWN  *************************");

        }
    }
}
