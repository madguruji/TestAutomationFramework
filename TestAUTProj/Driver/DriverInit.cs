using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace TestAUTProj.Driver
{
    sealed class DriverInit
    {
        private static DriverInit driverInstance =new DriverInit();

        private DriverInit(){}
        public static DriverInit getInstance()
        {
            return driverInstance;
        }
        private static ThreadLocal<IWebDriver> threadedDriver = new ThreadLocal<IWebDriver>();
        /// <summary>
        /// Enter Browser Name 
        /// </summary>
        /// <param name="browser"></param>
        /// <returns>WEBDRIVERS</returns>
        public IWebDriver getDriver(string browser)
        {
            string browserName = browser.ToLower();
            IWebDriver driver = null;
            if (threadedDriver.Value==null)
            {
                try
                {
                    if (browserName.Equals("chrome"))
                    {
                        ChromeOptions chromeOptions = setChromeOptions();
                        driver = new ChromeDriver(chromeOptions);
                        threadedDriver.Value = driver;
                    }
                    if (browserName.Equals("firefox"))
                    {
                        FirefoxOptions ffoptions = setFirefoxOptions();
                        driver = new FirefoxDriver(ffoptions);
                        threadedDriver.Value = driver;
                    }
                    if (browserName.Equals("edge"))
                    {
                        EdgeOptions edgeOptions = new EdgeOptions();
                        driver = new EdgeDriver(edgeOptions);
                        threadedDriver.Value = driver;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
                threadedDriver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                threadedDriver.Value.Manage().Window.Maximize();

            }
            return threadedDriver.Value;
        }
        public void quitDriver()
        {
            threadedDriver.Value.Quit();
            threadedDriver.Value = null;
        }
        private void setDriver(string browser)
        {

        }
        private ChromeOptions setChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            return options;
        }
        private FirefoxOptions setFirefoxOptions()
        {
            FirefoxOptions options = new FirefoxOptions();
            return options;
        }
        private EdgeOptions setEdgeOptions()
        {
            EdgeOptions options = new EdgeOptions();
            return options;
        }
    }
}
