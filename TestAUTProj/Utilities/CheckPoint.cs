using log4net;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAUTProj.Tests;

namespace TestAUTProj.Utilities
{
    public class CheckPoint
    {
        private static ILog log = LogManager.GetLogger(TestContext.CurrentContext.Test.Name);

        public static Dictionary<string, string> resultMap = new Dictionary<string, string>();
        private static string PASS = "PASS";
        private static string FAIL = "FAIL";

        public static void clearHashMap()
        {
            log.Info("Clearing Results from Dictionary");
            resultMap.Clear();
        }
        private static void setStatus(string key,string status)
        {
            resultMap.Add(key, status);
            log.Info(key +" :-> " + resultMap[key]);
        }
        public static void mark(string testName, Boolean result,string resultMessage)
        {
            testName=testName.ToLower();
            var key = testName + " . " + resultMessage;
            try
            {
                if (result)
                {
                    setStatus(key, PASS);
                }
                else
                {
                    setStatus(key, FAIL);
                }
            }
            catch (Exception e)
            {
                log.Error("Exception occured...");
                setStatus(key, FAIL);
                log.Error(e.StackTrace);
                throw;
            }
        }
        public static void markFinal(string testName, Boolean result,string resultMessage)
        {
            testName=testName.ToLower();
            var key = testName + " . " + resultMessage;
            try
            {
                if (result)
                {
                    setStatus(key, PASS);
                }
                else
                {
                    setStatus(key, FAIL);
                }
            }
            catch (Exception e)
            {
                log.Error("Exception occured...");
                setStatus(key, FAIL);
                log.Error(e.StackTrace);
                throw;
            }
            List<string> resultsList = new List<string>();
            foreach (string item in resultMap.Keys)
            {
                resultsList.Add(resultMap[item]);
            }
            foreach (var resultValues in resultsList)
            {
                if (resultsList.Contains(FAIL))
                {
                    log.Error("Test method Fail");
                    Assert.True(false);
                }
                else
                {
                    log.Info("Test method Successful");
                    Assert.True(true);
                }
            }
        }
    }
}
