using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestAUTProj.Base;
using TestAUTProj.Tests;
using TestAUTProj.Utilities;

namespace TestAUTProj.Helpers
{
    public class CustomDriver
    {
        private ILog log = LogManager.GetLogger(typeof(BaseTest).Name);
        private IWebDriver driver;
        private IJavaScriptExecutor executor;
        public CustomDriver()
        {

        }
        public CustomDriver(IWebDriver driver)
        {
            this.driver = driver;
            executor = (IJavaScriptExecutor)driver;
        }
        /// <summary>
        /// Refresh the page
        /// </summary>
        public void Refresh()
        {
            driver.Navigate().Refresh();
            log.Info("The current browser location was refreshed");
        }
        /// <summary>
        /// Gets the title of the page
        /// </summary>
        /// <returns>Title of the Page</returns>
        public string getTitle()
        {
            string title = driver.Title;
            log.Info("Title of the page is : " + title);
            return title;
        }

        /// <summary>
        /// Finds the type of the locator like XPATH,CSS,ID,NAME,PARTIAL LINK,LINK...
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>returns By types</returns>
        public By getByType(string locator)
        {
            By by = null;
            string locatorType = locator.Split("=>")[0];
            locator = locator.Split("=>")[1];
            try
            {
                if (locatorType.Contains("ID"))
                {
                    by = By.Id(locator);
                }
                else if (locatorType.Contains("XPATH"))
                {
                    by = By.XPath(locator);
                }
                else if (locatorType.Contains("CSS"))
                {
                    by = By.CssSelector(locator);
                }
                else if (locatorType.Contains("NAME"))
                {
                    by = By.Name(locator);
                }
                else if (locatorType.Contains("LINKTEXT"))
                {
                    by = By.LinkText(locator);
                }
                else if (locatorType.Contains("PARTIALLINK"))
                {
                    by = By.PartialLinkText(locator);
                }
                else if (locatorType.Contains("CLASS"))
                {
                    by = By.ClassName(locator);
                }
                else if (locatorType.Contains("TAG"))
                {
                    by = By.TagName(locator);
                }
                else
                {
                    log.Info("Locator type not supported");
                }
            }
            catch (Exception)
            {
                log.Error("By type not found with: " + locator);
                throw;
            }
            return by;
        }

        public IWebElement getElement(string locator, string info)
        {
            IWebElement element = null;
            By byType = getByType(locator);
            try
            {
                element = driver.FindElement(byType);
                log.Info("Element " + info + " found with locator: " + locator);
            }
            catch (Exception e)
            {
                log.Error("Element not found with: " + locator);
                log.Error(e.StackTrace);
                throw;
            }
            return element;
        }

        public IList<IWebElement> getElementsLists(string locator, string info)
        {
            IList<IWebElement> elementsList = new List<IWebElement>();
            By byType = getByType(locator);
            try
            {
                elementsList = driver.FindElements(byType);
                log.Info("Elements " + info + " found with locator: " + locator);
            }
            catch (Exception e)
            {
                log.Error("Element not found with: " + locator);
                log.Error(e.StackTrace);
                throw;
            }
            return elementsList;
        }
        public bool isElementPresent(string locator, string info)
        {
            IList<IWebElement> elementList = getElementsLists(locator, info);
            int size = elementList.Count;
            if (size > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void elementClick(IWebElement element, string info, int timeToWait)
        {
            try
            {
                element.Click();
                if (timeToWait == 0)
                {
                    log.Info("Clicked on info :: " + info);
                }
                else
                {
                    Utils.sleep(timeToWait, "Clicled on :: " + info);
                }
            }
            catch (Exception e)
            {
                log.Error("Cannot click on :: " + info);
                //add screenshot
                throw;
            }
        }
        public void elementClick(IWebElement element, string info)
        {
            elementClick(element, info, 0);
        }

        public void elementClick(string locator, string info, int timeToWait)
        {
            IWebElement webElement = getElement(locator, info);
            elementClick(webElement, info, timeToWait);
        }
        public void elementClick(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            elementClick(element, info, 0);
        }
        public void javaScriptClick(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            try
            {
                executor.ExecuteScript("argument[0].click()", element);
                log.Info("Clicked on : " + info);
            }
            catch (Exception e)
            {
                log.Info("Cannot click on info : " + element);
                log.Error(e.StackTrace);
                throw;
            }
        }
        public void clickWhenReady(By locator, int timeout)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                IWebElement webElement = null;
                log.Info("Waiting for max ::" + timeout + " seconds for element to be clickable");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                webElement = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                webElement.Click();
                log.Info("Element clicked on the webpage");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            }
            catch (Exception e)
            {
                log.Info("Element not appeared on the webPage");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                log.Error(e.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// Sends the data to a particular field using WebElements
        /// </summary>
        /// <param name="element"></param>
        /// <param name="data"></param>
        /// <param name="info"></param>
        /// <param name="clear"></param>
        public void sendData(IWebElement element, string data, string info, bool clear)
        {
            try
            {
                if (clear)
                {
                    element.Clear();
                }
                element.SendKeys(data);
                log.Info("Send keys on element " + info + " with data ::" + data);
            }
            catch (Exception e)
            {
                log.Error("Cannot send keys on element " + info + " with data ::" + data);
                log.Error(e.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// first clears the field and then sends the data to a particular field using Locators
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="data"></param>
        /// <param name="info"></param>
        /// <param name="clear"></param>
        public void sendData(string locator, string data, string info, bool clear)
        {
            IWebElement element = getElement(locator, info);
            sendData(element, data, info, clear);
        }
        /// <summary>
        ///  Sends the data to a particular field using locators without first clearing fields
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="data"></param>
        /// <param name="info"></param>
        public void sendData(string locator, string data, string info)
        {
            IWebElement element = getElement(locator, info);
            sendData(element, data, info, true);
        }
        /// <summary>
        /// Returns the text of a particular fields by using WebElements
        /// </summary>
        /// <param name="element"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public string getText(IWebElement element, string info)
        {
            log.Info("Getting text on element ::" + info);
            string text = null;
            text = element.Text;
            if (text.Length == 0)
            {
                text = element.GetAttribute("innerText");
            }
            if (!string.IsNullOrEmpty(text))
            {
                log.Info("The text is :" + text);
            }
            else
            {
                log.Info("Text not found");
            }
            return text.Trim();
        }
        /// <summary>
        /// Returns the text of a particular fields by using Locators
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public string getText(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            return getText(element, info);
        }
        /// <summary>
        /// Checks whether element is enabled or not
        /// Takes IwebElements as arguments
        /// </summary>
        /// <param name="element"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool isEnabled(IWebElement element, string info)
        {
            bool enabled = false;
            if (element != null)
            {
                enabled = element.Enabled;
                if (enabled)
                {
                    log.Info("Element " + info + " is enabled.");
                }
                else
                {
                    log.Info("Element " + info + " is disabled");
                }
            }
            return enabled;
        }
        public bool isEnabled(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            return isEnabled(element, info);
        }
        /// <summary>
        /// Returns true if element is displayed on the page
        /// </summary>
        /// <param name="element"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool isDisplayed(IWebElement element, string info)
        {
            bool displayed = false;
            if (element != null)
            {
                displayed = element.Displayed;
                if (displayed)
                {
                    log.Info("Element :: " + info + " is displayed");
                }
                else
                {
                    log.Info("Element ::" + info + " is not displayed");
                }

            }
            return displayed;
        }
        public bool isDisplayed(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            return isDisplayed(element, info);
        }

        public bool isSelected(IWebElement element, string info)
        {
            bool selected = false;
            if (element != null)
            {
                selected = element.Selected;
                if (selected)
                {
                    log.Info("Element :: " + info + " is selected ");
                }
                else
                {
                    log.Info("Element :: " + info + " is already selected");
                }

            }
            return selected;
        }
        public bool isSelected(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            return isSelected(element, info);
        }
        public void check(IWebElement element, string info)
        {
            if (!isSelected(element, info))
            {
                elementClick(element, info);
                log.Info("Element :: " + info + " is checked");
            }
            else
            {
                log.Info("Element :: " + info + "is not checked");
            }
        }
        public void check(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            check(element, info);
        }
        public void unCheck(IWebElement element, string info)
        {
            if (isSelected(element, info))
            {
                elementClick(element, info);
                log.Info("Element ::" + info + " is unchecked");
            }
            else
            {
                log.Info("Element :: " + info + " not unchecked");
            }
        }

        public bool submit(IWebElement element, string info)
        {
            if (element != null)
            {
                element.Submit();
                log.Info("Element :: " + info + " is submitted");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool submit(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            return submit(element, info);
        }
        public string getElementAttributeValue(string locator, string attribute)
        {
            IWebElement element = getElement(locator, "info");
            return element.GetAttribute(attribute);
        }
        public string getElementAttributeValue(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }

        public IWebElement waitForElement(string locator, int timeout)
        {
            By byType = getByType(locator);
            IWebElement element = null;
            try
            {
                log.Info("Waiting for max ::" + timeout + " seconds for element to be available");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                element = wait.Until(ExpectedConditions.ElementIsVisible(byType));
                log.Info("Element appeared on the webpage");
            }
            catch (Exception e)
            {
                log.Info("Element not appeared on the webpage");
                log.Error(e.StackTrace);
                throw;
            }
            return element;
        }

        public bool waitForLoading(string locator, long timeout)
        {
            By bytype = getByType(locator);
            bool elementInvisible = false;
            try
            {
                log.Info("Waiting for max:: " + timeout + " seconds for element to be available");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                elementInvisible = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(bytype));
                log.Info("Element appeared on the webpage");
            }
            catch (Exception e)
            {
                log.Info("Element not appeared on the webpage");
                log.Error(e.StackTrace);
                throw;
            }
            return elementInvisible;
        }

        public void mouseHover(string locator, string info)
        {
            IWebElement element = getElement(locator, info);
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();
        }
        public void selectOption(IWebElement element, string optionsToSelect)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByText(optionsToSelect);
            log.Info("Selected options:: " + optionsToSelect);
        }
        public void selectOption(string locator, string optionToSelect, string info)
        {
            IWebElement element = getElement(locator, info);
            selectOption(element, optionToSelect);
        }

        public string getSelectedDropDownValue(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            return select.SelectedOption.Text;
        }
        public bool isOptionExist(IWebElement element, string optionsToVerify)
        {
            SelectElement select = new SelectElement(element);
            bool exist = false;
            IList<IWebElement> optionList = select.Options;
            foreach (var option in optionList)
            {
                string text = getText(option, "Option Text");
                if (text.Equals(optionsToVerify))
                {
                    exist = true;
                    break;
                }
                if (exist)
                {
                    log.Info("Selected Option :" + optionsToVerify + " exist");
                }
                else
                {
                    log.Info("Selected Option : " + optionsToVerify + "does not exist");
                }
            }
            return exist;
        }
        //public String takeScreenshot(string methodName, string browserName)
        //{
        //    string fileName=Util.ge
        //}
        public void doubleClick(IWebElement element, string info)
        {
            Actions action = new Actions(driver);
            action.DoubleClick(element).Perform();
            log.Info("Double clicked on:: " + info);
        }
        public void selectItemRightClick(string elementLocator, string itemLocator)
        {
            IWebElement element = getElement(elementLocator, "info");
            Actions action = new Actions(driver);
            action.ContextClick(element).Build().Perform();
            log.Info("Right Clicked ");
            IWebElement itemElement = getElement(itemLocator, "info");
            elementClick(itemElement, "selected Item");
        }

        public string takeScreenShot(string methodName, string browserName)
        {
            string fileName = Utils.getScreenShotName(methodName, browserName);
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string newPath = dir + @"\Test_Output\ScreenShot\";
            string screenShotPath = newPath + fileName;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            try
            {
                ITakesScreenshot takeScreenshot = (ITakesScreenshot)driver;
                Screenshot screenshot = takeScreenshot.GetScreenshot();
                screenshot.SaveAsFile(screenShotPath);
                log.Info("Screenshot stored at path");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return screenShotPath;
        }
    }
}
