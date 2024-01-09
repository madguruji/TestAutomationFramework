using NUnit.Framework;
using TestAUTProj.Base;
using TestAUTProj.Utilities;

namespace TestAUTProj.Tests
{
    public class LoginTests:BaseTest
    {
        [Test]
        public void testLogin()
        {
            nav = loginPage.signIn(Constants.DEFAULT_USERNAME, Constants.DEFAULT_PASSWORD);
            Thread.Sleep(2000);
            Boolean result = nav.verifyHeader();
            CheckPoint.mark(Utils.getCurrentTestCaseName(), result, "All Courses header verification");
            CheckPoint.markFinal(Utils.getCurrentTestCaseName(), nav.isUserLoggedIn(), "User not logged in");
        }
        [Test]

        public void TC50450_TestCase_Header_Verify()
        {
            nav = loginPage.signIn(Constants.DEFAULT_USERNAME, Constants.INVALID_PASSWORD);
            Boolean result = nav.verifyHeader();
            CheckPoint.mark(Utils.getCurrentTestCaseName(), result, "All Courses header verification");
            CheckPoint.markFinal(Utils.getCurrentTestCaseName(), nav.isUserLoggedIn(), "user Not logged in");
        }
    }
}
