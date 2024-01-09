using NUnit.Framework.Interfaces;
using TestAUTProj.Base;
using TestAUTProj.Utilities;

namespace TestAUTProj.Tests
{
    internal class TestListners : BaseTest, ITestListener
    {
        //private static ExtentManager extentManager = ExtentManager.getInstance();
        public void SendMessage(TestMessage message)
        {
            throw new NotImplementedException();
        }

        public void TestFinished(ITestResult result)
        {
            throw new NotImplementedException();
        }

        public void TestOutput(TestOutput output)
        {
            throw new NotImplementedException();
        }

        public void TestStarted(ITest test)
        {
            throw new NotImplementedException();
        }
    }
}
