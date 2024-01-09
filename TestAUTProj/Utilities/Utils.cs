using NPOI.XSSF.UserModel;
using NUnit.Framework;
using System.Text;
namespace TestAUTProj.Utilities
{
    public class Utils
    {
        public static string getReportName()
        {
            DateTime dateTime = DateTime.Now;
            string dtTime = dateTime.ToString("hhMMss");
            StringBuilder sb = new StringBuilder()
                .Append("Report_")
                .Append(dtTime).Append(".html");
            return sb.ToString();
        }
        public static void sleep(int mSec, string info)
        {
            if(info!=null)
            {
                Console.WriteLine("Wait " + mSec + "for " + info);
            }
            try
            {
                Thread.Sleep(mSec);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
        public static int getRandomNumbers(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static int getRandomNumbers(int number)
        {
            return getRandomNumbers(1,number);
        }

        public static string randomString(int length)
        {
            const string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomArray= new char[length];
            for (int i = 0; i < length; i++)
            {
                randomArray[i] = charSet[getRandomNumbers(1, charSet.Length)];
            }
            return new string(randomArray);
        }
        /// <summary>
        /// Returns the Date and time with specified format
        /// </summary>
        /// <param name="format">"hhMMss"</param>
        /// <format>"ddMMyy/mmDDyy"<format>
        /// <returns>string</returns>
        public static string getSimpleDateFormat(string format)
        {
            DateTime date = DateTime.Now;
            string formatedDate = date.ToString(format);
            Console.WriteLine("Date with format :: "+format+" ::"+ formatedDate);
            return formatedDate;
        }
        /// <summary>
        /// Gets the method Name and Browser Name and returns the screenshot file name
        /// </summary>
        /// <param name="methodName">name of current running test mehtod</param>
        /// <param name="browserName">Name of the browser on which the test is running</param>
        /// <returns>string</returns>
        public static string getScreenShotName(string methodName, string browserName)
        {
            DateTime dateTime = DateTime.Now;
            string dateName = dateTime.ToString("hhMMss");
            StringBuilder sb = new StringBuilder();
            sb.Append(browserName)
                .Append("_")
                .Append(methodName)
                .Append("_")
                .Append(dateName).Append(".png");
            return sb.ToString();
        }
        public static string getCurrentTestCaseName()
        {
            return TestContext.CurrentContext.Test.Name;
        }
        public static Boolean verifyTextContains(string actualText, string expectedText)
        {
            if (actualText.ToLower().Contains(expectedText.ToLower()))
            {
                Console.WriteLine("Actual text from Web Application UI -----> : "+actualText);
                Console.WriteLine("Expected text from Web Application UI -----> : " + expectedText);
                Console.WriteLine("###  ~~~  ~~~  Verification Contains  ~~~  ~~~  !!!");
                return true;
            }
            else
            {
                Console.WriteLine("Actual text from Web Application UI -----> : " + actualText);
                Console.WriteLine("Expected text from Web Application UI -----> : " + expectedText);
                Console.WriteLine("###  ~~~  ~~~  Verification DOES NOT Contains  ~~~  ~~~  !!!");
                return false;
            }
        }

        public static Boolean VerifyTextMatch(string actualText, string expectedText)
        {
            if (actualText.Equals(expectedText))
            {
                Console.WriteLine("Actual text from Web Application UI -----> : " + actualText);
                Console.WriteLine("Expected text from Web Application UI -----> : " + expectedText);
                Console.WriteLine("###  ~~~  ~~~  VERIFICATION MATCH  ~~~  ~~~  !!!");
                return true;
            }
            else
            {
                Console.WriteLine("Actual text from Web Application UI -----> : " + actualText);
                Console.WriteLine("Expected text from Web Application UI -----> : " + expectedText);
                Console.WriteLine("###  ~~~  ~~~  VERIFICATION DOES NOT MATCHED  ~~~  ~~~  !!!");
                return false;
            }
        }
        public static Boolean verifyListEqual(IList<string> actualList,IList<string> expectedList)
        {
            if (areListsEqual(actualList,expectedList))
            {
                Console.WriteLine("Actual list equal to Expected list");
                return true;
            }
            else
            {
                Console.WriteLine("Actual list not equals to Expected list");
                return false;
            }
        }
        /// <summary>
        /// Check if the lists are equal using SequenceEqual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list_First"></param>
        /// <param name="list_sec"></param>
        /// <returns></returns>
        public static Boolean areListsEqual<T>(IList<T> list_First,IList<T> list_sec)
        {
            return list_First.SequenceEqual(list_sec);
        }
        public static Boolean listContainsAllElements<T>(IList<T> list_First,IList<T> list_sec)
        {
            foreach (var item in list_sec)
            {
                if (!list_First.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Returns the path of project folders
        /// </summary>
        /// <param name="FolderName"></param>
        public static string getProjectFolderPath(string folderName)
        {
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + $"\\{folderName}\\";
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Console.WriteLine("Directory not exists :: -----> " + filePath);
                    Directory.CreateDirectory(filePath);
                    Console.WriteLine("Directory created :: -----> " + filePath);
                }
                else
                {
                    Console.WriteLine("Directory exists :: -----> " + filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
            return filePath;
        }

    }
}
